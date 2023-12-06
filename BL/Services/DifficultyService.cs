using AutoMapper;
using BL.Interfaces;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PluralizeService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class DifficultyService : IDifficultyService
    {
        private readonly IMapper _mapper;
        private readonly RecipesSiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DifficultyService(IMapper mapper, RecipesSiteContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<List<DifficultyDTO>>> Add(DifficultyDTO newcategory)
        {
            var serviceResponse = new ServiceResponse<List<DifficultyDTO>>();

            try
            {
                var cat = _mapper.Map<Difficulty>(newcategory);
                _context.Difficulties.Add(cat);
                await _context.SaveChangesAsync();

                serviceResponse.Data =
                   await _context.Difficulties
                       .Select(c => _mapper.Map<DifficultyDTO>(c)).ToListAsync();

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<DifficultyDTO>>> Delete(int workId)
        {
            var serviceResponse = new ServiceResponse<List<DifficultyDTO>>();
            try
            {
                var cat = await _context.Difficulties
                    .FirstOrDefaultAsync(c => c.Id == workId);
                if (cat is null)
                    throw new Exception($"Difficulty with Id '{workId}' not found.");

                _context.Difficulties.Remove(cat);

                await _context.SaveChangesAsync();

                serviceResponse.Data =
                    await _context.Difficulties
                        .Select(c => _mapper.Map<DifficultyDTO>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<DifficultyDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<DifficultyDTO>>();
            var dbCharacters = await _context.Difficulties
                .ToListAsync();

            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<DifficultyDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<DifficultyDTO>> GetOne(int id)
        {
            var serviceResponse = new ServiceResponse<DifficultyDTO>();
            try
            {
                var dbCharacter = await _context.Difficulties
                              .FirstOrDefaultAsync(c => c.Id == id);

                if (dbCharacter is null)
                    throw new Exception($"Difficulty with Id '{id}' not found.");

                serviceResponse.Data = _mapper.Map<DifficultyDTO>(dbCharacter);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<DifficultyDTO>> Update(DifficultyDTO categoryUpdate)
        {
            var serviceResponse = new ServiceResponse<DifficultyDTO>();

            try
            {
                var cat =
                    await _context.Difficulties
                        .FirstOrDefaultAsync(c => c.Id == categoryUpdate.Id);
                if (cat is null)
                    throw new Exception($"Difficulty with Id '{categoryUpdate.Id}' not found.");

                cat.Status = categoryUpdate.Status;
                cat.Name = categoryUpdate.Name;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<DifficultyDTO>(cat);
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
