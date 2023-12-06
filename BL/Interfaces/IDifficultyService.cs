using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IDifficultyService
    {
        Task<ServiceResponse<List<DifficultyDTO>>> GetAll();
        Task<ServiceResponse<List<DifficultyDTO>>> Add(DifficultyDTO newcategory);
        Task<ServiceResponse<List<DifficultyDTO>>> Delete(int workId);
        Task<ServiceResponse<DifficultyDTO>> GetOne(int id);
        Task<ServiceResponse<DifficultyDTO>> Update(DifficultyDTO categoryUpdate);
    }
}
