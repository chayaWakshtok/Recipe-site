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
    public class FeedbackController : BaseController
    {
        IFeedbackService _feedbackService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public FeedbackController(IFeedbackService feedService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _feedbackService = feedService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetByRecipe/{recipeId}")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<FeedbackDTO>>>> GetByRecipe(int recipeId)
        {
            return Ok(await _feedbackService.GetByRecipe(recipeId));
        }

        [HttpPost]
        [Route("Add")]

        public async Task<ActionResult<ServiceResponse<List<FeedbackDTO>>>> Add(FeedbackDTO newObj)
        {
            return Ok(await _feedbackService.Add(newObj));
        }
    }
}
