
using DAL.Models.DB;
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
    public class UserService
    {
        //    private readonly IUnitOfWork _unitOfWork;
        //    public UserService(IUnitOfWork unitOfWork)
        //    {
        //        _unitOfWork = unitOfWork;
        //    }

        //    public async Task Add(User user)
        //    {
        //        try
        //        {
        //            await _unitOfWork.BeginTransaction();

        //            var workRepos = _unitOfWork.Repository<User>();
        //            user.CreateAt = DateTime.Now;
        //            user.UpdateAt = DateTime.Now;
        //            if (string.IsNullOrEmpty(user.Picture))
        //            {
        //                using (HttpClient client = new HttpClient())
        //                {
        //                    try
        //                    {
        //                        HttpResponseMessage response = client.GetAsync("https://api.genderize.io/?name=" + user.FirstName).Result;

        //                        string responseBody = response.Content.ReadAsStringAsync().Result;
        //                        var nameGender = JsonConvert.DeserializeObject<NameGender>(responseBody, new JsonSerializerSettings
        //                        {
        //                            ContractResolver = new CamelCasePropertyNamesContractResolver()
        //                        });
        //                        if (nameGender != null)
        //                        {
        //                            if (nameGender.Gender == Gender.Female)
        //                                user.Picture = "female_defualt.png";
        //                            else user.Picture = "male_default.jpg";
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                    }
        //                }
        //                //if()
        //            }
        //            user.Role = _unitOfWork.Repository<Role>().Find(user.RoleId);
        //            await workRepos.InsertAsync(user);

        //            await _unitOfWork.CommitTransaction();
        //        }
        //        catch (Exception e)
        //        {
        //            await _unitOfWork.RollbackTransaction();
        //            throw;
        //        }
        //    }

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

        //public class NameGender
        //{
        //    public string Name { get; set; }
        //    public Gender? Gender { get; set; }
        //    public float Probability { get; set; }
        //    public int Count { get; set; }
        //}
        //public enum Gender
        //{
        //    Male,
        //    Female
        //}
    }
}
