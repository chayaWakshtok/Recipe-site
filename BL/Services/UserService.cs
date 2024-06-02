
using AutoMapper;
using BL.Interfaces.Services;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly RecipesSiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IMapper mapper, RecipesSiteContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<List<UserDTO>>> Add(UserDTO userDTO)
        {
            var response = new ServiceResponse<List<UserDTO>>();
            if (await UserExists(userDTO.Username))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            GlobalService.SetPictureByGender(userDTO);
            var userDB = _mapper.Map<User>(userDTO);
            userDB.CreateAt = DateTime.Now;
            userDB.UpdateAt = DateTime.Now;
            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                GlobalService.CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userDB.PasswordHash = passwordHash;
                userDB.PasswordSalt = passwordSalt;
            }

            userDB.Role = _context.Roles.FirstOrDefault(p => p.Id == userDB.RoleId);

            _context.Users.Add(userDB);
            await _context.SaveChangesAsync();
            response.Data =
                   await _context.Users
                       .Select(c => _mapper.Map<UserDTO>(c)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<UserDTO>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<UserDTO>>();
            try
            {
                var cat = await _context.Users
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (cat is null)
                    throw new Exception($"User with Id '{id}' not found.");
                _context.Users.Remove(cat);

                await _context.SaveChangesAsync();
                serviceResponse.Data =
                    await _context.Users
                        .Select(c => _mapper.Map<UserDTO>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UserDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<UserDTO>>();
            var dbUsers = await _context.Users.Include(p => p.Recipes).Include(p => p.Likes)
                .ToListAsync();

            //string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";

            //dbUsers.ToList().ForEach(cat =>
            //{
            //    if (!string.IsNullOrEmpty(cat.Picture))
            //        cat.Picture = myHostUrl + cat.Picture;
            //});

            serviceResponse.Data = dbUsers.Select(c => _mapper.Map<UserDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDTO>> GetUserDetails(int id)
        {
            var serviceResponse = new ServiceResponse<UserDTO>();
            try
            {
                var dbUsers = await _context.Users.Include(p => p.Recipes).Include(p => p.Likes).Include(p => p.FollowFromUserNavigations)
                    .Include(p => p.FollowToUserNavigations)
                              .FirstOrDefaultAsync(c => c.Id == id);

                if (dbUsers is null)
                    throw new Exception($"User with Id '{id}' not found.");

                //string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";
                //if (!string.IsNullOrEmpty(dbUsers.Picture))
                //    dbUsers.Picture = myHostUrl + dbUsers.Picture;

                serviceResponse.Data = _mapper.Map<UserDTO>(dbUsers);
                serviceResponse.Data.CountFollowToUser=dbUsers.FollowToUserNavigations.Count;
                serviceResponse.Data.CountFollowFromUser=dbUsers.FollowFromUserNavigations.Count;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDTO>> GetOne(int id)
        {
            var serviceResponse = new ServiceResponse<UserDTO>();
            try
            {
                var dbUsers = await _context.Users.Include(p => p.Recipes).Include(p => p.Likes)
                              .FirstOrDefaultAsync(c => c.Id == id);

                if (dbUsers is null)
                    throw new Exception($"User with Id '{id}' not found.");

                //string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";
                //if (!string.IsNullOrEmpty(dbUsers.Picture))
                //    dbUsers.Picture = myHostUrl + dbUsers.Picture;

                serviceResponse.Data = _mapper.Map<UserDTO>(dbUsers);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDTO>> Update(UserDTO userDTO)
        {
            var serviceResponse = new ServiceResponse<UserDTO>();

            try
            {
                var cat =
                    await _context.Users
                        .FirstOrDefaultAsync(c => c.Id == userDTO.Id);
                if (cat is null)
                    throw new Exception($"User with Id '{userDTO.Id}' not found.");

                if (userDTO.Picture.Contains(";base64"))
                {
                    cat.Picture = GlobalService.SaveImage(userDTO.Picture, "");

                }

                cat.UpdateAt = DateTime.Now;
                cat.FirstName = userDTO.FirstName;
                cat.Username = userDTO.Username;
                cat.Status = userDTO.Status;
                cat.AboutMe = userDTO.AboutMe;
                if(!string.IsNullOrEmpty( userDTO.Password))
                {
                    GlobalService.CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    cat.PasswordHash = passwordHash;
                    cat.PasswordSalt = passwordSalt;
                }

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<UserDTO>(cat);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
