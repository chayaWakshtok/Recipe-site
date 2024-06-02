using AutoMapper;
using BL.Interfaces;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class FollowService : IFollowService
    {
        private readonly IMapper _mapper;
        private readonly RecipesSiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FollowService(IMapper mapper, RecipesSiteContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
         .FindFirst(ClaimTypes.NameIdentifier).Value!.ToString());

        public async Task<ServiceResponse<List<FollowDTO>>> Add(FollowDTO newcategory)
        {
            var serviceResponse = new ServiceResponse<List<FollowDTO>>();

            try
            {
                Follow cat = _mapper.Map<Follow>(newcategory);
                cat.FromUserNavigation = _context.Users.FirstOrDefault(p => p.Id == newcategory.FromUser);
                cat.ToUserNavigation = _context.Users.FirstOrDefault(p => p.Id == newcategory.ToUser);

                _context.Follows.Add(cat);
                await _context.SaveChangesAsync();

                serviceResponse.Data =
                   await _context.Follows.Where(p => p.FromUser == GetUserId() || p.ToUser == GetUserId())
                       .Select(c => _mapper.Map<FollowDTO>(c)).ToListAsync();

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FollowDTO>>> Delete(int workId)
        {
            var serviceResponse = new ServiceResponse<List<FollowDTO>>();
            try
            {
                var cat = await _context.Follows
                    .FirstOrDefaultAsync(c => c.Id == workId);
                if (cat is null)
                    throw new Exception($"Follow with Id '{workId}' not found.");

                _context.Follows.Remove(cat);

                await _context.SaveChangesAsync();


                var dbCats = await _context.Follows.Where(p => p.FromUser == GetUserId() || p.ToUser == GetUserId())
                        .Select(c => _mapper.Map<FollowDTO>(c)).ToListAsync();

                serviceResponse.Data = dbCats;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FollowDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<FollowDTO>>();
            var dbCharacters = await _context.Follows.Include(p => p.ToUserNavigation).Include(p=>p.FromUserNavigation)
                .ToListAsync();


            serviceResponse.Data = await _context.Follows.Where(p => p.FromUser == GetUserId() || p.ToUser == GetUserId())
                        .Select(c => _mapper.Map<FollowDTO>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FollowDTO>>> GetToUser()
        {
            var serviceResponse = new ServiceResponse<List<FollowDTO>>();
            var dbCharacters = await _context.Follows.Include(p => p.ToUserNavigation).Include(p => p.FromUserNavigation)
                .ToListAsync();


            serviceResponse.Data = await _context.Follows.Where(p => p.ToUser == GetUserId())
                        .Select(c => _mapper.Map<FollowDTO>(c)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<FollowDTO>>> GetFromUser()
        {
            var serviceResponse = new ServiceResponse<List<FollowDTO>>();
            var dbCharacters = await _context.Follows.Include(p => p.ToUserNavigation).Include(p => p.FromUserNavigation)
                .ToListAsync();


            serviceResponse.Data = await _context.Follows.Where(p => p.FromUser == GetUserId())
                        .Select(c => _mapper.Map<FollowDTO>(c)).ToListAsync();

            return serviceResponse;
        }

        public Task<ServiceResponse<FollowDTO>> GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<FollowDTO>> Update(FollowDTO categoryUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
