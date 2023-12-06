using AutoMapper;
using BL.Interfaces.Services;
using BL;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL.Interfaces;

namespace Netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : BaseController
    {
        IDifficultyService _difficultyService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public DifficultyController(IDifficultyService difficultyService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _difficultyService = difficultyService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<DifficultyDTO>>>> Get()
        {
            return Ok(await _difficultyService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<DifficultyDTO>>> GetSingle(int id)
        {
            return Ok(await _difficultyService.GetOne(id));
        }


        [HttpPost]
        [Route("Add")]

        public async Task<ActionResult<ServiceResponse<List<DifficultyDTO>>>> Add(DifficultyDTO newObj)
        {
            return Ok(await _difficultyService.Add(newObj));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<ServiceResponse<List<DifficultyDTO>>>> Update(DifficultyDTO updatedObj)
        {
            var response = await _difficultyService.Update(updatedObj);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<DifficultyDTO>>>> Delete(int id)
        {
            var response = await _difficultyService.Delete(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}

