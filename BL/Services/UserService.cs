using DAL.Interfaces;
using DAL.Interfaces.Services;
using DAL.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<User>();
                user.CreateAt = DateTime.Now;
                user.UpdateAt = DateTime.Now;
                await workRepos.InsertAsync(user);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<User>();
                var work = await workRepos.FindAsync(id);
                if (work == null)
                    throw new KeyNotFoundException();

                await workRepos.DeleteAsync(work);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IList<User>> GetAll()
        {
            return await _unitOfWork.Repository<User>().GetAllAsync();

        }

        public async Task<User> GetByUserNameAndPassword(string username, string password)
        {
            return await _unitOfWork.Repository<User>().FindOneAsync(p => p.Password == password && p.Username == username);
        }

        public async Task<User> GetOne(int id)
        {
            return await _unitOfWork.Repository<User>().FindAsync(id);

        }

        public async Task Update(User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<User>();
                var work = await workRepos.FindAsync(user.Id);
                if (work == null)
                    throw new KeyNotFoundException();

                work.Username = user.Username;
                work.Email = user.Email;
                work.Status = user.Status;
                work.Picture = user.Picture;
                work.UpdateAt = DateTime.Now;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
