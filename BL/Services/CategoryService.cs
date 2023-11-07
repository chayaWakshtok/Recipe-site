using AutoMapper;
using DAL.Interfaces;
using DAL.Interfaces.Services;
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

        private int GetUserId()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return int.Parse(_httpContextAccessor.HttpContext.User
                                   .FindFirst(ClaimTypes.NameIdentifier).ToString());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public async Task Add(Category category)
        {

            //var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var newCategory = _mapper.Map<Category>(category);
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }
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

                _context.Categories.Remove(cat);

                await _context.SaveChangesAsync();

                serviceResponse.Data =
                    await _context.Categories
                        .Select(c => _mapper.Map<CategoryDTO>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;



            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<Category>();
                var work = await workRepos.FindAsync(workId);
                if (work == null)
                    throw new KeyNotFoundException();

                await workRepos.DeleteAsync(work);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IList<Category>> GetAll()
        {
            return await _unitOfWork.Repository<Category>().GetAllAsync();
        }

        public async Task<Category> GetOne(int workId)
        {
            return await _unitOfWork.Repository<Category>().FindAsync(workId);
        }

        public async Task Update(Category category)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<Category>();
                var work = await workRepos.FindAsync(category.Id);
                if (work == null)
                    throw new KeyNotFoundException();

                work.Name = category.Name;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
