using AutoMapper;
using Azure;
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

            try
            {
                var recipeDB = _mapper.Map<Recipe>(newrecipe);
                recipeDB.CreateAt = DateTime.Now;
                recipeDB.UpdateAt = DateTime.Now;
                recipeDB.ImageUrl = GlobalService.SaveImage(newrecipe.ImageUrl, "recipe");

                recipeDB.Category = null;
                recipeDB.Difficulty = null;
                recipeDB.User = null;

                if (recipeDB.Instructions != null && recipeDB.Instructions.Count() > 0)
                {
                    for (int i = 0; i < recipeDB.Instructions.Count(); i++)
                    {
                        recipeDB.Instructions.ToArray()[i].Step = i + 1;
                        //var instruction = _mapper.Map<Instruction>(newrecipe.Instructions.ToArray()[i]);
                        //instruction.RecipeId = recipeDB.Id;
                        //_context.Instructions.Add(instruction);
                        //_context.SaveChanges();
                    }
                }



                if (recipeDB.Ingredients != null && recipeDB.Ingredients.Count() > 0)
                {

                    foreach (var item in recipeDB.Ingredients)
                    {
                        if (item.Product.Id > 0)
                        {
                            item.ProductId = item.Product.Id;
                            item.Product = null;
                            //item.Product = _context.Products.FirstOrDefault(p => p.Id == item.Product.Id);

                        }
                        else
                        {
                            var newProduct = new Product() { Name = item.Product.Name };
                            _context.Products.Add(newProduct);
                            _context.SaveChanges();
                            item.ProductId = _context.Products.FirstOrDefault(p => p.Id == newProduct.Id).Id;
                            item.Product = null;
                            //item.Product = _context.Products.FirstOrDefault(p => p.Id == newProduct.Id);
                        }

                        //var prodDB = _mapper.Map<Ingredient>(item);
                        //prodDB.RecipeId = recipeDB.Id;
                        //prodDB.Recipe = null;
                        //_context.Ingredients.Add(prodDB);
                        //_context.SaveChanges();
                    }
                }


                _context.Recipes.Add(recipeDB);
                await _context.SaveChangesAsync();

                var follows= _context.Follows.Include(p=>p.FromUserNavigation).Where(p => p.ToUser == newrecipe.UserId).ToList();
                foreach (var item in follows)
                {
                    GlobalService.SendEmail(item.FromUserNavigation.Email, item.FromUserNavigation.Username, "Add new Recipe", $"<div><h2>Your follow add new recipe - {recipeDB.Title}</h2>\r\n<a href=\"http://192.168.17.1:4200/recipe/{recipeDB.Id}\"></a></div>");

                }

                response.Data =
                    await _context.Recipes
                        .Select(c => _mapper.Map<RecipeDTO>(c)).ToListAsync();
                return response;
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }

            return response;


        }

        public async Task<ServiceResponse<List<RecipeDTO>>> Delete(int workId)
        {
            var serviceResponse = new ServiceResponse<List<RecipeDTO>>();
            try
            {
                var cat = await _context.Recipes.Include(p => p.Ingredients).Include(p => p.Instructions).Include(p => p.Likes)
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

        public async Task<ServiceResponse<List<RecipeDTO>>> GetLaster()
        {
            var serviceResponse = new ServiceResponse<List<RecipeDTO>>();
            var recipes = await _context.Recipes.Include(p => p.Ingredients).ThenInclude(p => p.Product).Include(p => p.Instructions).Include(p => p.Likes)
                .Include(p => p.User).Include(p=>p.Category)
                    .Include(p => p.Feedbacks).OrderByDescending(p=>p.CreateAt).Take(9)
                .ToListAsync();

            //string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";

            //recipes.ToList().ForEach(cat =>
            //{
            //    if (!string.IsNullOrEmpty(cat.ImageUrl))
            //        cat.ImageUrl = myHostUrl + cat.ImageUrl;
            //});

            serviceResponse.Data = recipes.Select(c => _mapper.Map<RecipeDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<RecipeDTO>>> GetByCategory(int category)
        {
            var serviceResponse = new ServiceResponse<List<RecipeDTO>>();
            var recipes = await _context.Recipes.Include(p => p.Likes)
                .Include(p => p.User).Include(p => p.Category).Where(p=>p.CategoryId==category)
                    .OrderByDescending(p => p.CreateAt)
                .ToListAsync();


            serviceResponse.Data = recipes.Select(c => _mapper.Map<RecipeDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<RecipeDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<RecipeDTO>>();
            var recipes = await _context.Recipes.Include(p => p.Ingredients).ThenInclude(p=>p.Product).Include(p => p.Instructions).Include(p => p.Likes)
                .Include(p=>p.User)
                    .Include(p => p.Feedbacks)
                    .Include(p => p.Difficulty).Include(p => p.Category)
                .ToListAsync();

           

            serviceResponse.Data = recipes.Select(c => _mapper.Map<RecipeDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<RecipeDTO>>> GetAllByUser(int userId)
        {
            var serviceResponse = new ServiceResponse<List<RecipeDTO>>();
            var recipes = await _context.Recipes.Include(p => p.Ingredients).ThenInclude(p => p.Product).Include(p => p.Instructions).Include(p => p.Likes)
                .Include(p => p.User)
                    .Include(p => p.Feedbacks).Where(p=>p.UserId==userId)
                .ToListAsync();

           

            serviceResponse.Data = recipes.Select(c => _mapper.Map<RecipeDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<RecipeDTO>> GetOne(int id)
        {
            var serviceResponse = new ServiceResponse<RecipeDTO>();
            try
            {
                var dbRecipe = await _context.Recipes.Include(p => p.Ingredients)
                    .ThenInclude(p => p.Product).Include(p => p.Instructions).Include(p => p.Likes)
                .Include(p => p.User).Include(p=>p.Difficulty).Include(p=>p.Category)
                    .Include(p => p.Feedbacks)
                              .FirstOrDefaultAsync(c => c.Id == id);

                if (dbRecipe is null)
                    throw new Exception($"Recipe with Id '{id}' not found.");

                //string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";
                //if (!string.IsNullOrEmpty(dbRecipe.ImageUrl))
                //    dbRecipe.ImageUrl = myHostUrl + dbRecipe.ImageUrl;

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
                    await _context.Recipes.Include(p => p.Ingredients).ThenInclude(p => p.Product).Include(p => p.Instructions)
                .Include(p => p.User)
                    .Include(p => p.Feedbacks)
                        .FirstOrDefaultAsync(c => c.Id == recUpdate.Id);
                if (cat is null)
                    throw new Exception($"Recipe with Id '{recUpdate.Id}' not found.");

                cat.UpdateAt = DateTime.Now;
                cat.ImageUrl = recUpdate.ImageUrl;
                if (recUpdate.ImageUrl.Contains(";base64"))
                {
                    if (!string.IsNullOrEmpty(cat.ImageUrl))
                        GlobalService.RemoveImage(cat.ImageUrl);
                    cat.ImageUrl = GlobalService.SaveImage(recUpdate.ImageUrl, "category");

                }
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


                var addInIngredients = recUpdate.Ingredients?.Where(p => p.Id == 0).ToList();

                var intDBIngredients = cat.Ingredients.ToList();

                cat.Ingredients = cat.Ingredients.Where(p => recUpdate.Ingredients?.Any(pp => pp.Id == p.Id) == true).ToList();


                for (int i = 0; i < cat.Ingredients.Count(); i++)
                {
                    var up = recUpdate.Ingredients?.First(p => p.Id == intDB[i].Id);
                    //intDBIngredients[i].Product.Name = up.Product.Name;
                    intDBIngredients[i].Count = up.Count;
                    intDBIngredients[i].TypeCount = up.TypeCount;
                }


                if (addInIngredients?.Count() > 0)
                {
                    addInIngredients.ForEach(p =>
                    {
                        cat.Ingredients.Add(new Ingredient() { Count = p.Count, Product = _context.Products.First(pp => pp.Id == p.ProductId), TypeCount = p.TypeCount });
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

        public async Task<ServiceResponse<List<RecipeDTO>>> GetAllByUserAndLikes(int userId)
        {
            var serviceResponse = new ServiceResponse<List<RecipeDTO>>();
            var recipes = await _context.Recipes.Include(p => p.Ingredients).ThenInclude(p => p.Product).Include(p => p.Instructions)
                .Include(p => p.User).Include(p=>p.Likes)
                    .Include(p => p.Feedbacks).Where(p => p.UserId == userId)
                .ToListAsync();

            //string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";

            //recipes.ToList().ForEach(cat =>
            //{
            //    if (!string.IsNullOrEmpty(cat.ImageUrl))
            //        cat.ImageUrl = myHostUrl + cat.ImageUrl;
            //});

            serviceResponse.Data = recipes.Select(c => _mapper.Map<RecipeDTO>(c)).ToList();
            return serviceResponse;
        }


        public async Task<ServiceResponse<int>> CountRecipe()
        {
            var serviceResponse = new ServiceResponse<int>();
            try
            {
                var count = _context.Recipes.Where(p => p.Status == 1).Count();
                serviceResponse.Data = count;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<List<RecipeDTO>>> MostLiked()
        {
            var serviceResponse = new ServiceResponse<List<RecipeDTO>>();
            var recipes = await _context.Recipes.Include(p => p.Ingredients).ThenInclude(p => p.Product).Include(p => p.Instructions).Include(p => p.Likes)
                .Include(p => p.User).Include(p => p.Category)
                    .Include(p => p.Feedbacks).OrderByDescending(p => p.Likes.Count).Take(6)
                .ToListAsync();

            //string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";

            //recipes.ToList().ForEach(cat =>
            //{
            //    if (!string.IsNullOrEmpty(cat.ImageUrl))
            //        cat.ImageUrl = myHostUrl + cat.ImageUrl;
            //});

            serviceResponse.Data = recipes.Select(c => _mapper.Map<RecipeDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<RecipeDTO>>> GetRecipesLikes(int userId)
        {
            var serviceResponse = new ServiceResponse<List<RecipeDTO>>();
            var recipes = await _context.Likes.Where(p=>p.UserId==userId).Include(p=>p.Recipe).Select(p=>p.Recipe)
                .ToListAsync();

            serviceResponse.Data = recipes.Select(c => _mapper.Map<RecipeDTO>(c)).ToList();
            return serviceResponse;
        }
    }
}
