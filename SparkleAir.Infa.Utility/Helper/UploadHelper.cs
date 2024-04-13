using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SparkleAir.Infa.Utility.Helper
{
    public class UploadHelper
    {
        //private readonly IWebHostEnvironment _host;

        //public string UploadImageFile(HttpPostedFileBase file, string path)
        //{
        //    //判斷有無上傳檔案，沒有則丟出例外
        //    //if (file == null || file.ContentLength == 0) throw new ArgumentNullException("file");
        //    //取得檔名並判斷是否為允許的檔案類型

        //    string[] allowExts = { ".jpg", ".jpeg", ".png" };
        //    string ext = Path.GetExtension(file.FileName).ToLower();
        //    if (!allowExts.Contains(ext)) throw new ArgumentException($"不允許上傳此類檔案({ext})");
        //    //為避免不同時間上傳相同檔名，每次要取唯一檔名
        //    string fileName = Path.GetRandomFileName();
        //    //與副檔名合併成正常檔名
        //    string newFileName = fileName + ext;
        //    string fullName = Path.Combine(path, newFileName);
        //    //存放上傳檔案並取得檔名
        //    file.SaveAs(fullName);

        //    return newFileName;
        //}

        //public void UploadToByteHelper(AirMealDto dto, HttpPostedFileBase file)
        //{
        //    string fileName = "empty.jpg";
        //    if (file != null)
        //    {
        //        fileName = file.FileName;
        //    }

        //    //檔案上傳路徑
        //    string uploadPath = Path.Combine(_host.WebRootPath, "uploads", fileName);
        //    //檔案上傳
        //    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
        //    {
        //        file?.CopyTo(fileStream);
        //    }

        //    //轉成二進
        //    byte[]? imgByte = null;
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        file?.CopyTo(memoryStream);
        //        imgByte = memoryStream.ToArray();
        //    }
        //    member.FileName = fileName;
        //    member.FileData = imgByte;

        //    //加進資料庫
        //    _dbContext.Members.Add(member);
        //    _dbContext.SaveChanges();
        //}
    }
}
