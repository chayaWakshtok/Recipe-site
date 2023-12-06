using DAL.Models.DB;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(UserDTO user, string password);
        Task<ServiceResponse<UserDTO>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
