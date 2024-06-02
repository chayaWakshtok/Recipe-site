using AutoMapper;
using BL;
using BL.Interfaces;
using BL.Interfaces.Services;
using BL.Services;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : BaseController
    {
        IlikeService _likeService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LikeController(IlikeService likeService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _likeService = likeService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetLikesByUser")]
        public async Task<ActionResult<ServiceResponse<List<LikesDTO>>>> GetRecipesLikes()
        {
            int userId = int.Parse(User.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _likeService.GetLikesByUser(userId));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> Delete(int id)
        {
            var response = await _likeService.Delete(id);
            if (response.Data is false)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
