using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IRecipeService
    {
        Task<ServiceResponse<List<RecipeDTO>>> GetAll();
        Task<ServiceResponse<List<RecipeDTO>>> Add(RecipeDTO newcategory);
        Task<ServiceResponse<List<RecipeDTO>>> Delete(int workId);
        Task<ServiceResponse<RecipeDTO>> GetOne(int id);
        Task<ServiceResponse<RecipeDTO>> Update(RecipeDTO categoryUpdate);
        Task<ServiceResponse<List<RecipeDTO>>> GetLaster();
        Task<ServiceResponse<List<RecipeDTO>>> MostLiked();
        Task<ServiceResponse<List<RecipeDTO>>> GetAllByUser(int userId);
        Task<ServiceResponse<List<RecipeDTO>>> GetAllByUserAndLikes(int userId);
        Task<ServiceResponse<List<RecipeDTO>>> GetRecipesLikes(int userId);
    }
}
