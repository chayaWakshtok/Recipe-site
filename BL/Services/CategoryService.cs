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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(Category category)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<Category>();
                await workRepos.InsertAsync(category);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(int workId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<Category>();
                var work = await workRepos.FindAsync(workId);
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

        public async Task<IList<Category>> GetAll()
        {
            return await _unitOfWork.Repository<Category>().GetAllAsync();
        }

        public async Task<Category> GetOne(int workId)
        {
            return await _unitOfWork.Repository<Category>().FindAsync(workId);
        }

        public async Task Update(Category category)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var workRepos = _unitOfWork.Repository<Category>();
                var work = await workRepos.FindAsync(category.Id);
                if (work == null)
                    throw new KeyNotFoundException();

                work.Name = category.Name;

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
