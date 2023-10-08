using AutoMapper;
using DAL.Interfaces.Services;
using DAL.Models.DB;
using Entities;
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
                await _categoryService.Add(category);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //[HttpPost, DisableRequestSizeLimit]
        //public IActionResult Upload()
        //{
        //    try
        //    {
        //        var file = Request.Form.Files[0];
        //        var folderName = "Images";
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);
        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }
        //            return Ok(new { dbPath });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}

        //private string UploadedFile(CategoryDTO model)
        //{
        //    string uniqueFileName = null;

        //    if (model.ImageFile != null)
        //    {
        //        string uploadsFolder = Path.Combine(@"C:\Users\User\Documents\GitHub\Recipe-site\Netcore", "Images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.ImageFile.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}
    }
}
