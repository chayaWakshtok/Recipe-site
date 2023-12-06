using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IFollowService
    {
        Task<ServiceResponse<List<FollowDTO>>> GetAll();
        Task<ServiceResponse<List<FollowDTO>>> Add(FollowDTO newcategory);
        Task<ServiceResponse<List<FollowDTO>>> Delete(int workId);
        Task<ServiceResponse<FollowDTO>> GetOne(int id);
        Task<ServiceResponse<FollowDTO>> Update(FollowDTO categoryUpdate);
    }
}
