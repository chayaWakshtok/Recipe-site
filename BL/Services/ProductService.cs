using AutoMapper;
using BL.Interfaces;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly RecipesSiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IMapper mapper, RecipesSiteContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<ProductDTO>>> Add(ProductDTO newProduct)
        {
            var serviceResponse = new ServiceResponse<List<ProductDTO>>();

            try
            {
                var cat = _mapper.Map<Product>(newProduct);
                _context.Products.Add(cat);
                await _context.SaveChangesAsync();

                serviceResponse.Data =
                   await _context.Products
                       .Select(c => _mapper.Map<ProductDTO>(c)).ToListAsync();

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ProductDTO>>> Delete(int workId)
        {
            var serviceResponse = new ServiceResponse<List<ProductDTO>>();
            try
            {
                var cat = await _context.Products
                    .FirstOrDefaultAsync(c => c.Id == workId);
                if (cat is null)
                    throw new Exception($"Product with Id '{workId}' not found.");

                _context.Products.Remove(cat);

                await _context.SaveChangesAsync();


                var dbCats = await _context.Products
                        .Select(c => _mapper.Map<ProductDTO>(c)).ToListAsync();

                serviceResponse.Data = dbCats;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ProductDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<ProductDTO>>();
            var dbCharacters = await _context.Products
                .ToListAsync();

            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<ProductDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ProductDTO>>> Search(string term)
        {
            var serviceResponse = new ServiceResponse<List<ProductDTO>>();
            var dbCharacters = await _context.Products.Where(p => p.Name.ToLower().Contains(term.ToLower()))
                .ToListAsync();

            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<ProductDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<ProductDTO>> GetOne(int id)
        {
            var serviceResponse = new ServiceResponse<ProductDTO>();
            try
            {
                var dbCharacter = await _context.Products
                              .FirstOrDefaultAsync(c => c.Id == id);

                if (dbCharacter is null)
                    throw new Exception($"Product with Id '{id}' not found.");

                serviceResponse.Data = _mapper.Map<ProductDTO>(dbCharacter);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ProductDTO>> Update(ProductDTO productUpdate)
        {
            var serviceResponse = new ServiceResponse<ProductDTO>();

            try
            {
                var cat =
                    await _context.Products
                        .FirstOrDefaultAsync(c => c.Id == productUpdate.Id);
                if (cat is null)
                    throw new Exception($"Product with Id '{productUpdate.Id}' not found.");

                cat.Name = productUpdate.Name;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<ProductDTO>(cat);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
