using DAL.Models.DB;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<List<UserDTO>>> GetAll();
        Task<ServiceResponse<List<UserDTO>>> Add(UserDTO userDTO);
        Task<ServiceResponse<List<UserDTO>>> Delete(int id);
        Task<ServiceResponse<UserDTO>> GetOne(int id);
        Task<ServiceResponse<UserDTO>> Update(UserDTO userDTO);
    }
}
