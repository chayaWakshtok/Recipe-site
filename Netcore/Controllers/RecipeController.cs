using AutoMapper;
using BL;
using BL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : BaseController
    {
        IRecipeService _recipeService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipeController(IRecipeService recipeService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _recipeService = recipeService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpGet("GetLaster")]
        public async Task<ActionResult<ServiceResponse<List<RecipeDTO>>>> GetLaster()
        {
            return Ok(await _recipeService.GetLaster());
        }

        [AllowAnonymous]
        [HttpGet("MostLike")]
        public async Task<ActionResult<ServiceResponse<List<RecipeDTO>>>> MostLike()
        {
            return Ok(await _recipeService.MostLiked());
        }

        [HttpGet("GetCount")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCount()
        {
            return Ok(await _recipeService.CountRecipe());
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<RecipeDTO>>>> Get()
        {
            return Ok(await _recipeService.GetAll());
        }

        [HttpGet("GetRecipesLikes")]
        public async Task<ActionResult<ServiceResponse<List<RecipeDTO>>>> GetRecipesLikes()
        {
            int userId = int.Parse(User.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _recipeService.GetRecipesLikes(userId));
        }

        [HttpGet("GetMyRecipes")]
        public async Task<ActionResult<ServiceResponse<List<RecipeDTO>>>> GetMyRecipes()
        {
            int userId = int.Parse(User.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _recipeService.GetAllByUserAndLikes(userId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<RecipeDTO>>> GetSingle(int id)
        {
            return Ok(await _recipeService.GetOne(id));
        }


        [HttpPost]
        [Route("Add")]

        public async Task<ActionResult<ServiceResponse<List<RecipeDTO>>>> Add(RecipeDTO newObj)
        {
            return Ok(await _recipeService.Add(newObj));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<ServiceResponse<List<RecipeDTO>>>> Update(RecipeDTO updatedObj)
        {
            var response = await _recipeService.Update(updatedObj);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult<ServiceResponse<List<RecipeDTO>>>> UpdateUser(RecipeDTO updatedObj)
        {
            if (updatedObj.UserId.ToString() != User.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).Value)
            {
                return BadRequest("Only create user can update");
            }
            var response = await _recipeService.Update(updatedObj);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<ProductDTO>>>> Delete(int id)
        {
            var response = await _recipeService.Delete(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllByUser")]
        public async Task<ActionResult<ServiceResponse<List<RecipeDTO>>>> GetAllByUser()
        {
            int userId = int.Parse(User.Claims.First(p => p.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _recipeService.GetAllByUser(userId));
        }

    }
}
