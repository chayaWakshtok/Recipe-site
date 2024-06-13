using Entities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BL.Services.AuthRepository;
using System.Net.Mail;
using System.Net;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MimeKit.Utils;

namespace BL.Services
{
    public static class GlobalService
    {
        public static bool SendEmail(string toAddress, string userName,
            string subject, string message)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("RealFood", "realfood@email.com"));
                email.To.Add(new MailboxAddress(userName, toAddress));

                email.Subject = subject;

                var bodyBuilder = new BodyBuilder();

                bodyBuilder.HtmlBody = message;

                email.Body = bodyBuilder.ToMessageBody();
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect("sandbox.smtp.mailtrap.io", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("af70e5c9e58483", "a7c80bdccd2723");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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

        public static string SaveImage(string imageSrc, string folder)
        {
            string FileName = folder + "_" + Guid.NewGuid().ToString() + ".jpg";
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
