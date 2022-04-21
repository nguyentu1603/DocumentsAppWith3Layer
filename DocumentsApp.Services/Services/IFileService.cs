using DocumentsApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsApp.Services.Services
{
    public interface IFileService
    {
        Task<List<File>> GetFiles();
        Task<File> GetFileByIdAsync(int id);
        Task<File> AddFileAsync(File newFile);
        Task<File> DeleteFileAsync(int id);
    }
}
