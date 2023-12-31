using AutoMapper;
using BL.Interfaces;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class RecipesService : IRecipeService
    {
        private readonly IMapper _mapper;
        private readonly RecipesSiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipesService(IMapper mapper, RecipesSiteContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<List<RecipeDTO>>> Add(RecipeDTO newrecipe)
        {
            var response = new ServiceResponse<List<RecipeDTO>>();
            var recipeDB = _mapper.Map<Recipe>(newrecipe);
            recipeDB.CreateAt = DateTime.Now;
            recipeDB.UpdateAt = DateTime.Now;
            recipeDB.Category = _context.Categories.FirstOrDefault(p => p.Id == recipeDB.CategoryId);
            recipeDB.Difficulty = _context.Difficulties.FirstOrDefault(p => p.Id == recipeDB.DifficultyId);
            recipeDB.User = _context.Users.FirstOrDefault(p => p.Id == recipeDB.UserId);
            if (recipeDB.Instructions != null && recipeDB.Instructions.Count() > 0)
            {
                for (int i = 0; i < recipeDB.Instructions.Count(); i++)
                {
                    recipeDB.Instructions.ToArray()[i].Step = i + 1;
                }
            }

            _context.Recipes.Add(recipeDB);
            await _context.SaveChangesAsync();

            response.Data =
                await _context.Recipes
                    .Select(c => _mapper.Map<RecipeDTO>(c)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<RecipeDTO>>> Delete(int workId)
        {
            var serviceResponse = new ServiceResponse<List<RecipeDTO>>();
            try
            {
                var cat = await _context.Recipes.Include(p => p.Ingredients).Include(p => p.Instructions)
                    .Include(p => p.Feedbacks)
                    .FirstOrDefaultAsync(c => c.Id == workId);
                if (cat is null)
                    throw new Exception($"Recipe with Id '{workId}' not found.");

                _context.Recipes.Remove(cat);

                await _context.SaveChangesAsync();
                serviceResponse.Data =
                    await _context.Recipes
                        .Select(c => _mapper.Map<RecipeDTO>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<RecipeDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<RecipeDTO>>();
            var recipes = await _context.Recipes
                .ToListAsync();

            string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";

            recipes.ToList().ForEach(cat =>
            {
                if (!string.IsNullOrEmpty(cat.ImageUrl))
                    cat.ImageUrl = myHostUrl + cat.ImageUrl;
            });

            serviceResponse.Data = recipes.Select(c => _mapper.Map<RecipeDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<RecipeDTO>> GetOne(int id)
        {
            var serviceResponse = new ServiceResponse<RecipeDTO>();
            try
            {
                var dbRecipe = await _context.Recipes
                              .FirstOrDefaultAsync(c => c.Id == id);

                if (dbRecipe is null)
                    throw new Exception($"Recipe with Id '{id}' not found.");

                string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";
                if (!string.IsNullOrEmpty(dbRecipe.ImageUrl))
                    dbRecipe.ImageUrl = myHostUrl + dbRecipe.ImageUrl;

                serviceResponse.Data = _mapper.Map<RecipeDTO>(dbRecipe);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<RecipeDTO>> Update(RecipeDTO recUpdate)
        {
            var serviceResponse = new ServiceResponse<RecipeDTO>();

            try
            {
                var cat =
                    await _context.Recipes
                        .FirstOrDefaultAsync(c => c.Id == recUpdate.Id);
                if (cat is null)
                    throw new Exception($"Recipe with Id '{recUpdate.Id}' not found.");

                cat.UpdateAt = DateTime.Now;
                cat.ImageUrl = recUpdate.ImageUrl;
                cat.Status = recUpdate.Status;
                cat.Carbs = recUpdate.Carbs;
                cat.VideoUrl = recUpdate.VideoUrl;
                cat.Fat = recUpdate.Fat;
                cat.Calories = recUpdate.Calories;
                cat.Category = _context.Categories.FirstOrDefault(p => p.Id == recUpdate.CategoryId);
                cat.Servings = recUpdate.Servings;
                cat.Description = recUpdate.Description;
                cat.Difficulty = _context.Difficulties.FirstOrDefault(p => p.Id == recUpdate.DifficultyId);
                cat.PrepTime = recUpdate.PrepTime;
                cat.Protein = recUpdate.Protein;
                cat.Title = recUpdate.Title;

                if (recUpdate.Instructions != null && recUpdate.Instructions.Count() > 0)
                {
                    for (int i = 0; i < recUpdate.Instructions.Count(); i++)
                    {
                        recUpdate.Instructions.ToArray()[i].Step = i + 1;
                    }
                }

                var addIn = recUpdate.Instructions?.Where(p => p.Id == 0).ToList();

                var intDB = cat.Instructions.ToList();

                cat.Instructions = cat.Instructions.Where(p => recUpdate.Instructions?.Any(pp => pp.Id == p.Id) == true).ToList();


                for (int i = 0; i < cat.Instructions.Count(); i++)
                {
                    var up = recUpdate.Instructions?.First(p => p.Id == intDB[i].Id);
                    intDB[i].Step = up.Step;
                    intDB[i].Description = up.Description;
                }


                if (addIn?.Count() > 0)
                {
                    addIn.ForEach(p =>
                    {
                        cat.Instructions.Add(new Instruction() { Description = p.Description, Step = p.Step });
                    });
                }


                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<RecipeDTO>(cat);
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
