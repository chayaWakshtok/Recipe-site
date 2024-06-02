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
    public class LikeService : IlikeService
    {
        private readonly IMapper _mapper;
        private readonly RecipesSiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LikeService(IMapper mapper, RecipesSiteContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<LikesDTO>>> Add(LikesDTO newlike)
        {
            var serviceResponse = new ServiceResponse<List<LikesDTO>>();

            try
            {
                var cat = _mapper.Map<Likes>(newlike);
                _context.Likes.Add(cat);
                await _context.SaveChangesAsync();

                serviceResponse.Data =
                   await _context.Likes
                       .Select(c => _mapper.Map<LikesDTO>(c)).ToListAsync();

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> Delete(int workId)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var cat = await _context.Likes
                    .FirstOrDefaultAsync(c => c.Id == workId);
                if (cat is null)
                    throw new Exception($"Like with Id '{workId}' not found.");

                _context.Likes.Remove(cat);

                await _context.SaveChangesAsync();

                serviceResponse.Data = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<bool>> GetIfExist(int userId, int recipeId)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var cat = await _context.Likes
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.RecipeId == recipeId);
                if (cat is null)
                {
                    serviceResponse.Data = true;
                    return serviceResponse;
                }
                serviceResponse.Data = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<LikesDTO>>> GetLikesByUser(int userId)
        {
            var serviceResponse = new ServiceResponse<List<LikesDTO>>();
            var likes = await _context.Likes.Where(p => p.UserId == userId).Include(p => p.Recipe)
                .ToListAsync();

            serviceResponse.Data = likes.Select(c => _mapper.Map<LikesDTO>(c)).ToList();
            return serviceResponse;
        }
    }
}
