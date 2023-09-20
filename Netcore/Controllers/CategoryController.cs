using AutoMapper;
using DAL.Interfaces.Services;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService,IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IList<CategoryDTO>> Get()
        {
            return _mapper.Map<IList<CategoryDTO>>( await _categoryService.GetAll());
        }

        [HttpPost]
        public async Task< bool> Add([FromBody]CategoryDTO categoryDTO)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDTO);
                category.Image = UploadedFile(categoryDTO);
                await _categoryService.Add(category);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private string UploadedFile(CategoryDTO model)
        {
            string uniqueFileName = null;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(@"C:\Users\User\Documents\GitHub\Recipe-site\Netcore", "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
