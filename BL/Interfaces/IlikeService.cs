using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IlikeService
    {
        Task<ServiceResponse<bool>> GetIfExist(int userId,int recipeId);
        Task<ServiceResponse<List<LikesDTO>>> GetLikesByUser(int userId);
        Task<ServiceResponse<List<LikesDTO>>> Add(LikesDTO newlike);
        Task<ServiceResponse<bool>> Delete(int workId);
    }
}
