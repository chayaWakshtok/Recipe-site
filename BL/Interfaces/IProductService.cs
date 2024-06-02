using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<List<ProductDTO>>> GetAll();
        Task<ServiceResponse<List<ProductDTO>>> Add(ProductDTO newProduct);
        Task<ServiceResponse<List<ProductDTO>>> Delete(int workId);
        Task<ServiceResponse<ProductDTO>> GetOne(int id);
        Task<ServiceResponse<ProductDTO>> Update(ProductDTO productUpdate);
        Task<ServiceResponse<List<ProductDTO>>> Search(string term);
    }
}
