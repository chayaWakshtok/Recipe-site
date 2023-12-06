
using AutoMapper;
using BL.Interfaces.Services;
using DAL.Models.DB;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly RecipesSiteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IMapper mapper, RecipesSiteContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        //    public async Task Delete(int id)
        //    {
        //        try
        //        {
        //            await _unitOfWork.BeginTransaction();

        //            var workRepos = _unitOfWork.Repository<User>();
        //            var work = await workRepos.FindAsync(id);
        //            if (work == null)
        //                throw new KeyNotFoundException();

        //            await workRepos.DeleteAsync(work);

        //            await _unitOfWork.CommitTransaction();
        //        }
        //        catch (Exception e)
        //        {
        //            await _unitOfWork.RollbackTransaction();
        //            throw;
        //        }
        //    }

        //    public async Task<IList<User>> GetAll()
        //    {
        //        var users = await _unitOfWork.Repository<User>().GetAllAsync();
        //        users.ToList().ForEach(p =>
        //        {
        //            p.Password = "";
        //        });
        //        return users;

        //    }

        //    public async Task<User> GetByUserNameAndPassword(string username, string password)
        //    {
        //        return await _unitOfWork.Repository<User>().FindOneAsync(p => p.Password == password && p.Username == username);
        //    }

        //    public async Task<User> GetOne(int id)
        //    {
        //        return await _unitOfWork.Repository<User>().FindAsync(id);

        //    }

        //    public async Task Update(User user)
        //    {
        //        try
        //        {
        //            await _unitOfWork.BeginTransaction();

        //            var workRepos = _unitOfWork.Repository<User>();
        //            var work = await workRepos.FindAsync(user.Id);
        //            if (work == null)
        //                throw new KeyNotFoundException();

        //            work.Username = user.Username;
        //            work.FirstName = user.FirstName;
        //            work.Email = user.Email;
        //            work.Status = user.Status;
        //            work.Picture = user.Picture;
        //            work.UpdateAt = DateTime.Now;

        //            await _unitOfWork.CommitTransaction();
        //        }
        //        catch (Exception e)
        //        {
        //            await _unitOfWork.RollbackTransaction();
        //            throw;
        //        }
        //    }
        //}

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<List<UserDTO>>> Add(UserDTO userDTO)
        {
            var response = new ServiceResponse<List<UserDTO>>();
            if (await UserExists(userDTO.Username))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }

            GlobalService.SetPictureByGender(userDTO);
            var userDB = _mapper.Map<User>(userDTO);
            userDB.CreateAt = DateTime.Now;
            userDB.UpdateAt = DateTime.Now;
            if(!string.IsNullOrEmpty(userDTO.Password))
            {
                GlobalService.CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userDB.PasswordHash = passwordHash;
                userDB.PasswordSalt = passwordSalt;
            }
        
            userDB.Role = _context.Roles.FirstOrDefault(p => p.Id == userDB.RoleId);

            _context.Users.Add(userDB);
            await _context.SaveChangesAsync();
            response.Data =
                   await _context.Users
                       .Select(c => _mapper.Map<UserDTO>(c)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<UserDTO>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<UserDTO>>();
            try
            {
                var cat = await _context.Users
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (cat is null)
                    throw new Exception($"User with Id '{id}' not found.");
                _context.Users.Remove(cat);

                await _context.SaveChangesAsync();
                serviceResponse.Data =
                    await _context.Users
                        .Select(c => _mapper.Map<UserDTO>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UserDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<UserDTO>>();
            var dbUsers = await _context.Users
                .ToListAsync();

            string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";

            dbUsers.ToList().ForEach(cat =>
            {
                if (!string.IsNullOrEmpty(cat.Picture))
                    cat.Picture = myHostUrl + cat.Picture;
            });

            serviceResponse.Data = dbUsers.Select(c => _mapper.Map<UserDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDTO>> GetOne(int id)
        {
            var serviceResponse = new ServiceResponse<UserDTO>();
            try
            {
                var dbUsers = await _context.Users
                              .FirstOrDefaultAsync(c => c.Id == id);

                if (dbUsers is null)
                    throw new Exception($"User with Id '{id}' not found.");

                string myHostUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/Images/";
                if (!string.IsNullOrEmpty(dbUsers.Picture))
                    dbUsers.Picture = myHostUrl + dbUsers.Picture;

                serviceResponse.Data = _mapper.Map<UserDTO>(dbUsers);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDTO>> Update(UserDTO userDTO)
        {
            var serviceResponse = new ServiceResponse<UserDTO>();

            try
            {
                var cat =
                    await _context.Users
                        .FirstOrDefaultAsync(c => c.Id == userDTO.Id);
                if (cat is null)
                    throw new Exception($"User with Id '{userDTO.Id}' not found.");

                cat.UpdateAt = DateTime.Now;
                cat.FirstName = userDTO.FirstName;
                cat.Username = userDTO.Username;
                cat.Status = userDTO.Status;
                cat.Picture = userDTO.Picture;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<UserDTO>(cat);
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
