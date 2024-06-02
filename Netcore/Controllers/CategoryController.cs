using AutoMapper;
using BL;
using BL.Interfaces.Services;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryController(ICategoryService categoryService,IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<List<CategoryDTO>>>> Get()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<CategoryDTO>>> GetSingle(int id)
        {
            return Ok(await _categoryService.GetOne(id));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<ServiceResponse<List<CategoryDTO>>>> Add(CategoryDTO newObj)
        {
            return Ok(await _categoryService.Add(newObj));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<ServiceResponse<List<CategoryDTO>>>> Update(CategoryDTO updatedObj)
        {
            var response = await _categoryService.Update(updatedObj);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<CategoryDTO>>>> Delete(int id)
        {
            var response = await _categoryService.Delete(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
