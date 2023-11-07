using DAL.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces.Services
{
    public interface IUserService
    {
        Task<IList<User>> GetAll();
        Task<User> GetOne(int id);
        Task Update(User user);
        Task Add(User user);
        Task Delete(int id);
        Task<User> GetByUserNameAndPassword(string username, string password);
    }
}
