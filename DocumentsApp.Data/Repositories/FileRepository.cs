using DocumentsApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DocumentsApp.Data.Repositories
{
    public class FileRepository : Repository<File>, IFileRepository
    {
        public FileRepository(DocumentsDb _context) : base(_context)
        {

        }
        public async Task<List<File>> GetFiles()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<File> GetFileById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<File> AddFile(File file)
        {
            return await AddAsync(file);
        }

        public async Task<File> DeleteFile(int id)
        {
            return await DeleteAsync(id);
        }
    }
}
