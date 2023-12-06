using Entities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.Services.AuthRepository;

namespace BL.Services
{
    public static class GlobalService
    {
        public static UserDTO SetPictureByGender(UserDTO user)
        {
            if (string.IsNullOrEmpty(user.Picture))
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = client.GetAsync("https://api.genderize.io/?name=" + user.FirstName).Result;

                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        var nameGender = JsonConvert.DeserializeObject<NameGender>(responseBody, new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
                        if (nameGender != null)
                        {
                            if (nameGender.Gender == Gender.Female)
                                user.Picture = "female_defualt.png";
                            else user.Picture = "male_default.jpg";
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return user;
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static string GetActualpath(string FileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), @"Images\") + FileName;
        }

        public static string SaveImage(string imageSrc)
        {
            string FileName = "category_" + Guid.NewGuid().ToString()+".jpg";
            string imagepath = GetActualpath(FileName);
            try
            {

                if (System.IO.File.Exists(imagepath))
                    System.IO.File.Delete(imagepath);

                string[] words = imageSrc.Split(";base64,");

                File.WriteAllBytes(imagepath, Convert.FromBase64String(words[1]));
                return FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool RemoveImage(string imageName)
        {
            string FileName = imageName;
            string imagepath = GetActualpath(FileName);
            try
            {
                if (System.IO.File.Exists(imagepath))
                    System.IO.File.Delete(imagepath);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
