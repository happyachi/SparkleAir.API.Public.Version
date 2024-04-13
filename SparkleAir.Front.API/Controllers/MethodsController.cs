using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SparkleAir.Infa.Dto.Methods;
using SparkleAir.Infa.Utility.Helper.GoogleDriveApi;
using SparkleAir.Infa.Utility.Helper.Jwts;
using SparkleAir.Infa.Utility.Helper.Notices;

namespace SparkleAir.Front.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MethodsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MethodsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("getAllFilesId")]
        public async Task<string> GetAllFilesId()
        {
            var googleDriveHelper = new GoogleDriveHelper(_configuration);
            var result = googleDriveHelper.ListFiles();
            return result;
        }

        [HttpPost("uploadFileToGoogleDrive")]
        public async Task<string> UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        // 轉成byte
                        file.CopyTo(memoryStream);
                        byte[] imageData = memoryStream.ToArray();

                        // 呼叫方法上傳檔案， 得到Id
                        var googleDriveHelper = new GoogleDriveHelper(_configuration);
                        var googleFileId = googleDriveHelper.UploadFile(imageData, file.FileName);

                        // 得到圖片位置
                        var googleFileUrl = googleDriveHelper.GetImageSrc(googleFileId);

                        return $"檔案Id:{googleFileId},  圖片位置:{googleFileUrl}";
                    }
                }
                catch (Exception ex)
                {
                    return $"發生錯誤: {ex.Message}";
                }
            }
            else
            {
                return "請上傳有效的圖片檔案";
            }
        }

        [HttpPost("uploadFileToGoogleDriveFromBase64")]
        public async Task<string> UploadFileToGoogleDriveFromBase64(GetImageDto dto)
        {

            try
            {
                byte[] imageData = Convert.FromBase64String(dto.Base64);
                var fileName = Guid.NewGuid().ToString("N");
                // 呼叫方法上傳檔案， 得到Id
                var googleDriveHelper = new GoogleDriveHelper(_configuration);
                var googleFileId = googleDriveHelper.UploadFile(imageData, fileName);

                // 得到圖片位置
                //var googleFileUrl = googleDriveHelper.GetImageSrc(googleFileId);

                return googleFileId;
                    
            }
            catch (Exception ex)
            {
                throw new Exception($"發生錯誤: {ex.Message}");

            }
           
        }


        [HttpPost("SendEmail")]
        public async Task tesSendEmail([FromBody]Email email)
        {
            //發送Email測試
            var emailSetting = _configuration.GetSection("SendEmailSetting").Get<Dictionary<string, string>>();

            //SendEmailHelper.SendEmail(
            //    emailSetting["FromEmail"],
            //    emailSetting["FromPassword"],
            //    "sparkle.airline@gmail.com",
            //    "test2",
            //    "test1234");

            SendEmailHelper.SendEmail(
                emailSetting["FromEmail"],
                emailSetting["FromPassword"],
                email.ToEmail,
                email.Subject,
                email.Body);
        }

        /// <summary>
        /// HangFire的使用測試
        /// </summary>
        [HttpGet("hangFireTest")]
        public void HangFireTest()
        {
            //單次立即執行
            BackgroundJob.Enqueue(() => Console.WriteLine("單次!"));
            //單次10秒後執行
            BackgroundJob.Schedule(() => Console.WriteLine("10秒後執行!"), TimeSpan.FromSeconds(10));
            //重複執行，預設為每天00:00啟動
            RecurringJob.AddOrUpdate(() => Console.WriteLine("重複執行！"), Cron.Daily);
        }

        
    }
}
