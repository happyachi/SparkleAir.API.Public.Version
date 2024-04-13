using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleAir.Infa.Utility.Helper
{
    public class UploadImgHelper
    {
        /// <summary>
        /// 將上傳檔案存檔
        /// </summary>
        /// <param name="path">要存放的實體路徑</param>
        /// <param name="file"></param>
        /// <exception cref="UploadFileNullException">沒有上傳檔案,或檔案大小為 0 byte 時, 丟出本例外</exception>
        /// <returns></returns>
        //public string SaveAs(string path, HttpPostedFileBase file)
        //{
        //    if (file == null || string.IsNullOrEmpty(file.FileName) || file.ContentLength == 0)
        //    {
        //        throw new UploadFileNullException();
        //    }

        //    string ext = System.IO.Path.GetExtension(file.FileName); // 副檔名, 例如 .jpg
        //    string newFileName = Guid.NewGuid().ToString("N") + ext;
        //    string fullPath = System.IO.Path.Combine(path, newFileName);

        //    file.SaveAs(fullPath);

        //    return newFileName;

        //}

        public class UploadFileNullException : ApplicationException
        {
        }
    }
}
