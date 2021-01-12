using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Common
{
    //Save file and get infor of File
    public interface IStrorageService
    {
        string GetUrl(string fileName);
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
