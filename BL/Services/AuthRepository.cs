using AutoMapper;
using BL.Interfaces;
using DAL.Models.DB;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly RecipesSiteContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthRepository(RecipesSiteContext context, IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<UserDTO>> Login(string username, string password)
        {
            var response = new ServiceResponse<UserDTO>();
            var user = await _context.Users.Include(p => p.Role)
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if (user is null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                var userdto = _mapper.Map<UserDTO>(user);
                userdto.Token = CreateToken(user);
                response.Data = userdto;
            }

            return response;
        }


        public class NameGender
        {
            public string Name { get; set; }
            public Gender? Gender { get; set; }
            public float Probability { get; set; }
            public int Count { get; set; }
        }


        public async Task<ServiceResponse<int>> Register(UserDTO user, string password)
        {
            var response = new ServiceResponse<int>();
            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            GlobalService.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            GlobalService.SetPictureByGender(user);
            var userDB = _mapper.Map<User>(user);
            userDB.CreateAt = DateTime.Now;
            userDB.UpdateAt = DateTime.Now;
            userDB.PasswordHash = passwordHash;
            userDB.PasswordSalt = passwordSalt;
            userDB.Role=_context.Roles.FirstOrDefault(p=>p.Id==user.RoleId);

            _context.Users.Add(userDB);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

       

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,user.RoleId.ToString())

            };

            var issuer = _configuration.GetSection("Jwt:Issuer").Value;
            var audience = _configuration.GetSection("Jwt:Audience").Value;
            var key = Encoding.ASCII.GetBytes
            (_configuration.GetSection("Jwt:Key").Value);

            //var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            //if (appSettingsToken is null)
            //    throw new Exception("AppSettings Token is null!");

            //SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            //    .GetBytes(appSettingsToken));

            //SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
