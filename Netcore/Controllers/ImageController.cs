using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Netcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        [HttpPost("UploadImage")]
        public async Task<ActionResult> UploadImage()
        {
            bool Result = false;
            var Files = Request.Form.Files;
            foreach (IFormFile source in Files)
            {
                string FileName = source.FileName;
                string imagepath = GetActualpath(FileName);
                try
                {

                    if (System.IO.File.Exists(imagepath))
                        System.IO.File.Delete(imagepath);

                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                        Result = true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return Ok(Result);

        }
        [HttpPost("RemoveImage/{imageName}")]
        public ActionResult RemoveImage(string imageName)
        {
            string Result = string.Empty;
            string FileName = imageName;
            string imagepath = GetActualpath(FileName);
            try
            {
                if (System.IO.File.Exists(imagepath))
                    System.IO.File.Delete(imagepath);

                Result = "pass";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(Result);

        }

        [NonAction]
        public string GetActualpath(string FileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), @"Images") + FileName;
        }

        //[NonAction]
        //private string GetImagebycode(int Code)
        //{
        //    string hosturl = "https://localhost:44308";
        //    string mainimagepath = GetActualpath(Code.ToString());
        //    string Filepath = mainimagepath + "\\1.png";

        //    if (System.IO.File.Exists(Filepath))
        //        return hosturl + "/Uploads/Product/" + Code + "/1.png";
        //    else
        //        return hosturl + "/Uploads/Common/noimage.png";
        //}

        [HttpPost]
        [Route("Upload")]
        public IActionResult Upload()
        {
            try
            {
                var files = Request.Form.Files;
                List<string> list = new List<string>();
                var folderName =  "Images";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (files.Any(f => f.Length == 0))
                {
                    return BadRequest();
                }
                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName); //you can add this path to a list and then return all dbPaths to the client if require
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    list.Add(dbPath);
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
