using Google.Apis.Auth.OAuth2;
using Google.Apis.Docs.v1;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Microsoft.Extensions.Configuration;
using SparkleAir.Infa.EFModel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Google.Apis.Drive.v3.Data.File;

namespace SparkleAir.Infa.Utility.Helper.GoogleDriveApi
{
    public class GoogleTest
    {
        DriveService _driveService;
        string _sparkleStaticFolderId = "1EYAiOBxhmMDl9754WhbgxsVHOI9xdn9-";

        public GoogleTest(IConfiguration configuration)
        {
            _driveService = CreateDriveService(configuration);
        }

        public void test()
        {
            //方法2 從JSON字串讀取
            string _credentialJson = string.Empty;
            using (FileStream fs = new FileStream("credential.json", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    _credentialJson = sr.ReadToEnd();
                }
            }

            // 获取特定节点的配置
            //IConfigurationSection apiKeysSection = _configuration.GetSection("SendEmailSetting");

            //// 将节点的配置转换为 JSON 字符串
            //string sendEmailJson = "{" + string.Join(",", apiKeysSection.GetChildren().Select(child => $"\"{child.Key}\": \"{child.Value}\"")) + "}";


            GoogleCredential credential = GoogleCredential.FromJson(_credentialJson).CreateScoped(new[] { DriveService.Scope.Drive });

            DriveService _driveService = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "_applicationName"
            });
        }

        /// <summary>
        /// 建立DriveService
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private DriveService CreateDriveService(IConfiguration configuration)
        {
            // 得到指定section
            IConfigurationSection apiKeysSection = configuration.GetSection("GoogleDriveSetting");

            // 將section 轉成JSON字串
            string sendEmailJson = "{" + string.Join(",", apiKeysSection.GetChildren().Select(child => $"\"{child.Key}\": \"{child.Value}\"")) + "}";

            // 取得 credential
            GoogleCredential credential = GoogleCredential.FromJson(sendEmailJson).CreateScoped(new[] { DriveService.Scope.Drive });

            // 建立 service
            DriveService driveService = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "_applicationName"
            });
            return driveService;
        }


        public void ListFiles()
        {
            DriveService service = _driveService;

            string parentFolderId = "";

            if (parentFolderId == "")
            {
                parentFolderId = "file";
            }

            // 列出您的應用程式建立的檔案
            IList<File> folders = ListFiles(service, "root");   // 列出根目錄的資料
            IList<File> files = ListFiles(service, parentFolderId);     // 列出子目錄或非根目錄的資料

            // 將根目錄資料列在 ListBox 上
            if (folders != null && folders.Count > 0)
            {
                foreach (var file in folders)
                {
                    Console.WriteLine($"1{file.Name} ({file.Id})");
                }
            }
            else
            {
                Console.WriteLine("1No files found.");
            }

            // 將非根目錄資料列在 ListBox 上
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine($"2{file.Name} ({file.Id})");
                }
            }
            else
            {
                Console.WriteLine("2No files found.");
            }
        }



        private static IList<File> ListFiles(DriveService service, string root_or_file)
        {
            string request = $"'{root_or_file}'";
            if (root_or_file == "file")
            {
                request = "not 'root'";
            }

            try
            {
                FilesResource.ListRequest listRequest = service.Files.List();
                // 查詢根目錄中的檔案
                // listRequest.Q = "" 列出全部目錄檔案
                // 'root' in parents 會列出根目錄檔案
                // not 'root' in parents 會列出所有非根目錄檔案
                // '{id}' in parents 則會列出指定目錄下的檔案
                // name contains 'CBETA' and trashed = true 會列出檔名包含 CBETA 且在垃圾桶中

                listRequest.Q = request + " in parents";

                // nextPageToken 應該是要傳回下一頁用的，此處沒處理。
                listRequest.Fields = "nextPageToken, files(id, name)";
                FileList files = listRequest.Execute();
                return files.Files;
            }
            catch (Exception e)
            {
                Console.WriteLine("No files found.");
                return null;
            }

        }

        private void ShowInfo()
        {
            // DriveService service = GetDriveService();
            string fileId = "";

            if (fileId == "") { return; }

            // 取得各種屬性
            FilesResource.GetRequest request = _driveService.Files.Get(fileId);
            request.Fields = "name, parents, size, createdTime, modifiedTime";
            File file = request.Execute();
            var edParentFolderId = file.Parents[0].ToString(); // 取得目錄名
            var edFileName = file.Name;                        // 取得檔名

            string size = file.Size.ToString();                 // 取得檔案大小
            string createdTime = file.CreatedTime.ToString();   // 取得建檔日期
            string modifiedTime = file.ModifiedTime.ToString(); // 取得修改日期

            var edFileInfo = $"【建立時間】{createdTime} 【修改時間】{modifiedTime} 【檔案大小】{size}";

        }

        // 建立目錄
        public void CreateFolder()
        {
            // 要創建的目錄的名稱
            string folderName = "123";

            // 父目錄 ID，若是最上層則空白即可，或是用 root
            string parentFolderId = "1EYAiOBxhmMDl9754WhbgxsVHOI9xdn9-";

            // 創建目錄
            CreateFolderInDrive(folderName, parentFolderId);
        }

        private void CreateFolderInDrive(string folderName, string parentFolderId)
        {
            DriveService service = _driveService;

            // 如果沒有指定父目錄，則用 root 為根目錄
            if (parentFolderId == "")
            {
                parentFolderId = "root";
            }

            // 創建 File 實例
            File fileMetadata = new File
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string> { parentFolderId }
            };

            // 建立目錄
            var request = service.Files.Create(fileMetadata);
            // request.Fields = "id";          // 僅取回 id 屬性，亦可忽略不寫
            var folder = request.Execute();

            // 打印新子目錄的ID
            if (folder != null)
            {
                Console.WriteLine($"建立目錄 ID：{folder.Id}");
            }
            else
            {
                Console.WriteLine("建立目錄失敗。");
            }
        }

        // 上傳檔案
        public void UploadFile(string fileName)
        {
            //string fileName = "";
            string parentFolderId = _sparkleStaticFolderId;

            UploadFileToDrive(fileName, parentFolderId);
        }

        private void UploadFileToDrive(string fileName, string parentFolderId)
        {
            DriveService service = _driveService;

            // 如果沒有指定父目錄，則用 root 為根目錄
            if (parentFolderId == "")
            {
                parentFolderId = "root";
            }

            // 創建 File 實例
            var fileMetadata = new File()
            {
                Name = Path.GetFileName(fileName),
                Parents = new List<string> { parentFolderId }
            };

            FilesResource.CreateMediaUpload request;

            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, GetMimeType(fileName));
                request.Fields = "id";      // 返回文件的ID
                request.Upload();
            }

            var file = request.ResponseBody;

            Console.WriteLine($"檔案 ID：{file.Id}");
        }

        // 抄來的，取得 MineType
        private string GetMimeType(string filePath)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(filePath).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }

            return mimeType;
        }

        // 上傳檔案
        public void UploadFileTest(byte[] imageData, string fileName)
        {
            //string fileName = "";
            string parentFolderId = _sparkleStaticFolderId;

            DriveService service = _driveService;

            // 如果沒有指定父目錄，則用 root 為根目錄
            if (parentFolderId == "")
            {
                parentFolderId = "root";
            }

            // 創建 File 實例
            var fileMetadata = new File()
            {
                Name = fileName,
                Parents = new List<string> { parentFolderId }
            };

            FilesResource.CreateMediaUpload request;

            MemoryStream stream = new MemoryStream(imageData);

     
            request = service.Files.Create(fileMetadata, stream, GetMimeType(fileName));
            request.Fields = "id";      // 返回文件的ID
            request.Upload();
          

            var file = request.ResponseBody;

            Console.WriteLine($"檔案 ID：{file.Id}");
        }

    }
}
