using AutoMapper;
using BL.Services;
using DAL.Interfaces.Services;
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

        [HttpGet]
        [Route("GetAllWithPicture")]
        public async Task<IList<UserDTO>> Get()
        {
            var users = await _userService.GetAll();
            string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";

            users.ToList().ForEach(p =>
            {
                if (!string.IsNullOrEmpty(p.Picture))
                    p.Picture = myHostUrl + p.Picture;
            });
            return _mapper.Map<IList<UserDTO>>(users);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<bool> Add([FromBody] UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                await _userService.Add(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<bool> Update([FromBody] UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                await _userService.Update(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                await _userService.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
