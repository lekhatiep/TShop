using TShopSolution.Utilities.Constant;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace TShopSolution.Application.Common
{
    public class FileStorageService : IStrorageService
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT__FOLDER_NAME = SystemConstant.ProductSettings.USER_CONTENT_FOLDER_NAME;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT__FOLDER_NAME);
        }

        public string GetUrl(string fileName)
        {
            return $"/{USER_CONTENT__FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(fileName))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}