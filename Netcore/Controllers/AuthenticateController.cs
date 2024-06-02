using AutoMapper;
using BL;
using BL.Interfaces;
using BL.Interfaces.Services;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <returns></returns>
        private readonly IAuthRepository _authRepo;

        public AuthenticateController(IAuthRepository authRepo, IHttpContextAccessor httpContextAccessor)
        {
            _authRepo = authRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<int>>> GetUser()
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = int.Parse(userId);
            //get user from db
            return Ok(userId);
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
     .FindFirst(ClaimTypes.NameIdentifier)!.Value.ToString());


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserDTO request)
        {
            var response = await _authRepo.Register(
                request, request.Password
            );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<UserDTO>>> Login(UserDTO request)
        {
            var response = await _authRepo.Login(request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
