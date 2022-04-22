using DocumentsApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentsApp.Data.Repositories
{
    public interface IFileRepository : IRepository<File>
    {
        Task<List<File>> GetFiles();
        Task<File> GetFileById(int id);
        Task<File> AddFile(File File);
        Task<File> DeleteFile(int id);
        Task<File> UpdateFile(File file);
    }
}
