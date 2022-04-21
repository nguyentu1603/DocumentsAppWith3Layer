using DocumentsApp.Data.Models;
using DocumentsApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsApp.Services.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<File> AddFileAsync(File newFile)
        {
            return await _fileRepository.AddFile(newFile);
        }

        public async Task<File> DeleteFileAsync(int id)
        {
            return await _fileRepository.DeleteFile(id);
        }

        public async Task<File> GetFileByIdAsync(int id)
        {
            return await _fileRepository.GetFileById(id);
        }

        public async Task<List<File>> GetFiles()
        {
            return await _fileRepository.GetFiles();
        }
    }
}
