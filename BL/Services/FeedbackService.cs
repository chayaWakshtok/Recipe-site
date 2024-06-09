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
    public class FeedbackService : IFeedbackService
    {
        private readonly IMapper _mapper;
        private readonly RecipesSiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FeedbackService(IMapper mapper, RecipesSiteContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<ServiceResponse<List<FeedbackDTO>>> Add(FeedbackDTO newcategory)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<FeedbackDTO>>> Delete(int workId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<FeedbackDTO>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<FeedbackDTO>>> GetByRecipe(int recipe)
        {
            var serviceResponse = new ServiceResponse<List<FeedbackDTO>>();
            var likes = await _context.Feedbacks.Where(p => p.RecipeId == recipe).Include(p=>p.User)
                .ToListAsync();

            serviceResponse.Data = likes.Select(c => _mapper.Map<FeedbackDTO>(c)).ToList();
            return serviceResponse;
        }

        public Task<ServiceResponse<FeedbackDTO>> GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<FeedbackDTO>> Update(FeedbackDTO categoryUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
