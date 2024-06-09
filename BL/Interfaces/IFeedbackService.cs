using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IFeedbackService
    {
        Task<ServiceResponse<List<FeedbackDTO>>> GetAll();
        Task<ServiceResponse<List<FeedbackDTO>>> Add(FeedbackDTO newcategory);
        Task<ServiceResponse<List<FeedbackDTO>>> Delete(int workId);
        Task<ServiceResponse<FeedbackDTO>> GetOne(int id);
        Task<ServiceResponse<FeedbackDTO>> Update(FeedbackDTO categoryUpdate);
        Task<ServiceResponse<List<FeedbackDTO>>> GetByRecipe(int recipe);
    }
}
