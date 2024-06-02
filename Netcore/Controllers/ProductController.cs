using AutoMapper;
using BL;
using BL.Interfaces;
using BL.Interfaces.Services;
using BL.Services;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ProductController(IProductService productService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Search/{term}")]
        public async Task<ActionResult<ServiceResponse<List<ProductDTO>>>> Search(string term)
        {
            return Ok(await _productService.Search(term));
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<ProductDTO>>>> Get()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ProductDTO>>> GetSingle(int id)
        {
            return Ok(await _productService.GetOne(id));
        }

        [HttpPost]
        [Route("Add")]

        public async Task<ActionResult<ServiceResponse<List<ProductDTO>>>> Add(ProductDTO newObj)
        {
            return Ok(await _productService.Add(newObj));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<ServiceResponse<List<ProductDTO>>>> Update(ProductDTO updatedObj)
        {
            var response = await _productService.Update(updatedObj);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<ProductDTO>>>> Delete(int id)
        {
            var response = await _productService.Delete(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
