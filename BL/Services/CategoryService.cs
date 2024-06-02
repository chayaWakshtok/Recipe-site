using AutoMapper;
using DAL.Models.DB;
using System;
using System.Collections.Generic;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Entities;
using BL.Interfaces.Services;

namespace BL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly RecipesSiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryService(IMapper mapper, RecipesSiteContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
         .FindFirst(ClaimTypes.NameIdentifier)!.ToString());

        public async Task<ServiceResponse<List<CategoryDTO>>> Add(CategoryDTO newcategory)
        {

            var serviceResponse = new ServiceResponse<List<CategoryDTO>>();

            try
            {
                var cat = _mapper.Map<Category>(newcategory);
                cat.Image = GlobalService.SaveImage(cat.Image, "category");
                _context.Categories.Add(cat);
                await _context.SaveChangesAsync();

                serviceResponse.Data =
                   await _context.Categories
                       .Select(c => _mapper.Map<CategoryDTO>(c)).ToListAsync();

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CategoryDTO>>> Delete(int workId)
        {
            var serviceResponse = new ServiceResponse<List<CategoryDTO>>();
            try
            {
                var cat = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == workId);
                if (cat is null)
                    throw new Exception($"Category with Id '{workId}' not found.");

                if (string.IsNullOrEmpty(cat.Image))
                    GlobalService.RemoveImage(cat.Image);

                _context.Categories.Remove(cat);

                await _context.SaveChangesAsync();

                string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";

                var dbCats = await _context.Categories
                        .Select(c => _mapper.Map<CategoryDTO>(c)).ToListAsync();

                dbCats.ToList().ForEach(cat =>
                {
                    if (!string.IsNullOrEmpty(cat.Image))
                        cat.Image = myHostUrl + cat.Image;
                });

                serviceResponse.Data = dbCats;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<CategoryDTO>>> GetAll()
        {

            var serviceResponse = new ServiceResponse<List<CategoryDTO>>();
            var dbCharacters = await _context.Categories.Include(p=>p.Recipes)
                .ToListAsync();

            string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";

            dbCharacters.ToList().ForEach(cat =>
            {
                
                if (!string.IsNullOrEmpty(cat.Image))
                    cat.Image = myHostUrl + cat.Image;
            });

            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<CategoryDTO>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<CategoryDTO>> GetOne(int id)
        {

            var serviceResponse = new ServiceResponse<CategoryDTO>();
            try
            {
                var dbCharacter = await _context.Categories
                              .FirstOrDefaultAsync(c => c.Id == id);

                if (dbCharacter is null)
                    throw new Exception($"Category with Id '{id}' not found.");

                string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";
                if (!string.IsNullOrEmpty(dbCharacter.Image))
                    dbCharacter.Image = myHostUrl + dbCharacter.Image;

                serviceResponse.Data = _mapper.Map<CategoryDTO>(dbCharacter);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<CategoryDTO>> Update(CategoryDTO categoryUpdate)
        {
            var serviceResponse = new ServiceResponse<CategoryDTO>();

            try
            {
                var cat =
                    await _context.Categories
                        .FirstOrDefaultAsync(c => c.Id == categoryUpdate.Id);
                if (cat is null)
                    throw new Exception($"Category with Id '{categoryUpdate.Id}' not found.");

                if (categoryUpdate.Image.Contains(";base64"))
                {
                    if (!string.IsNullOrEmpty(cat.Image))
                        GlobalService.RemoveImage(cat.Image);
                    cat.Image = GlobalService.SaveImage(categoryUpdate.Image, "category");

                }

                cat.UpdateAt = DateTime.Now;
                cat.Description = categoryUpdate.Description;
                cat.Status = categoryUpdate.Status;
                cat.Name = categoryUpdate.Name;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<CategoryDTO>(cat);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
