using AutoMapper;
using BL.Services;
using DAL.Interfaces.Services;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Netcore.Controllers
{
    public class AuthenticateController : BaseController
    {
        #region Property
        /// <summary>
        /// Property Declaration
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IConfiguration _config;
        IUserService _userService;
        private readonly IMapper _mapper;
        #endregion

        #region Contructor Injector
        /// <summary>
        /// Constructor Injection to access all methods or simply DI(Dependency Injection)
        /// </summary>
        public AuthenticateController(IConfiguration config, IUserService userService, IMapper mapper)
        {
            _config = config;
            _userService = userService;
            _mapper = mapper;
        }
        #endregion

        #region GenerateJWT
        /// <summary>
        /// Generate Json Web Token Method
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string GenerateJSONWebToken(UserDTO userDTO)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            IEnumerable<Claim> claims = new List<Claim>() { new Claim("Id",userDTO.Id.ToString()),
                new Claim("RoleId",userDTO.RoleId.ToString()),
                new Claim("Username",userDTO.Username),
                new Claim("Email",userDTO.Email),};

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddHours(24),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                await _userService.Add(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        #region Login Validation
        /// <summary>
        /// Login Authenticaton using JWT Token Authentication
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel data)
        {
            IActionResult response = Unauthorized();
            var user = await _userService.GetByUserNameAndPassword(data.UserName, data.Password);
            if (user != null)
            {
                var userDto = _mapper.Map<UserDTO>(user);
                var tokenString = GenerateJSONWebToken(userDto);
                response = Ok(new { Token = tokenString, Message = "Success" });
            }
            return response;
        }
        #endregion
        //[HttpGet(nameof(Get))]
        //public async Task<IEnumerable<string>> Get()
        //{
        //    var accessToken = await HttpContext.GetTokenAsync("access_token");

        //    return new string[] { accessToken };
        //}

    }

    #region JsonProperties
    /// <summary>
    /// Json Properties
    /// </summary>
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
    #endregion
}
