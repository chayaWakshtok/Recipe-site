using DAL.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IList<Category>> GetAll();
        Task<Category> GetOne(int workId);
        Task Update(Category work);
        Task Add(Category work);
        Task Delete(int workId);
    }
}
