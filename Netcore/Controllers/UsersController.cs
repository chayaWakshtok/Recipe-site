using AutoMapper;
using BL;
using BL.Interfaces.Services;
using BL.Services;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet("GetAllWithPicture")]
        public async Task<ActionResult<ServiceResponse<List<UserDTO>>>> Get()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<ServiceResponse<UserDTO>>> GetUserById(int id)
        {
            return Ok(await _userService.GetOne(id));
        }

        [HttpPost]
        [Route("Add")]

        public async Task<ActionResult<ServiceResponse<List<UserDTO>>>> Add(UserDTO newObj)
        {
            return Ok(await _userService.Add(newObj));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<ServiceResponse<List<UserDTO>>>> Update(UserDTO updatedObj)
        {
            var response = await _userService.Update(updatedObj);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<UserDTO>>>> Delete(int id)
        {
            var response = await _userService.Delete(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
