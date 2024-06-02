using AutoMapper;
using BL.Interfaces.Services;
using BL;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL.Interfaces;
using BL.Services;
using System.Security.Claims;

namespace Netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        IFollowService _followService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FollowController(IFollowService followService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _followService = followService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
.FindFirst(ClaimTypes.NameIdentifier)!.Value.ToString());

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<FollowDTO>>>> Get()
        {
            return Ok(await _followService.GetAll());
        }

        [HttpGet("GetFromUser")]
        public async Task<ActionResult<ServiceResponse<List<FollowDTO>>>> GetFromUser()
        {
            //var res=GetUserId();
           var res= User.Claims;
            return Ok(await _followService.GetFromUser());
        }

        [HttpGet("GetToUser")]
        public async Task<ActionResult<ServiceResponse<List<FollowDTO>>>> GetToUser()
        {
            return Ok(await _followService.GetToUser());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<FollowDTO>>> GetSingle(int id)
        {
            return Ok(await _followService.GetOne(id));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<ServiceResponse<List<FollowDTO>>>> Add(FollowDTO newObj)
        {
            return Ok(await _followService.Add(newObj));
        }
    }
}
