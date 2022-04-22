using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using DocumentsApp.Services.Services;
using System.IO;
using File = DocumentsApp.Data.Models.File;
using Microsoft.AspNetCore.Authorization;

namespace DocumentsApp.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FilesController(IFileService fileService, IWebHostEnvironment hostEnvironment)
        {
            _fileService = fileService;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: api/Files
        [HttpGet]
        public async Task<ActionResult<List<File>>> GetAll()
        {
            return await _fileService.GetFiles();
        }

        //// GET: api/Files/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var file = await _fileService.GetFileByIdAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        ////// POST: api/Files
        [HttpPost]
        public IActionResult PostFile(File file)
        {
            //Get Identity Name from Azure AD
            file.CreatedBy = User.Identity.Name;
            file.ModifiedBy = User.Identity.Name;
            _fileService.AddFileAsync(file);
            return Ok(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFile(int id, File file)
        {
            if (id != file.Id)
            {
                return BadRequest();
            }
            file.ModifiedAt = DateTime.Now;
            await _fileService.UpdateFileAsync(file);
            return NoContent();
        }

        public void DeleteFileUpload(string fileName)
        {
            var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Storages/Uploads", fileName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

        public async Task<string> SaveFile(IFormFile uploadFile)
        {
            string fileName = new String(Path.GetFileNameWithoutExtension(uploadFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            fileName = fileName + DateTime.Now.ToString("-yymmssfff") + Path.GetExtension(uploadFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Storages/Uploads", fileName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await uploadFile.CopyToAsync(fileStream);
            }
            return fileName;
        }

        ////// DELETE: api/Files/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var file = await _fileService.GetFileByIdAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            DeleteFileUpload(file.Name);
            await _fileService.DeleteFileAsync(id);
            return NoContent();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Upload(List<IFormFile> uploadFile)
        {
            foreach (var formFile in uploadFile)
            {
                if (formFile.Length > 0)
                {
                    // Add new File
                    File newFile = new File();
                    newFile.Name = await SaveFile(formFile);
                    newFile.Extension = Path.GetExtension(newFile.Name);
                    newFile.CreatedBy = User.Identity.Name;
                    newFile.ModifiedBy = User.Identity.Name;
                    await _fileService.AddFileAsync(newFile);
                }
            }
            return StatusCode(201);
        }

        [HttpGet("IsFileUpload/{id}")]
        public async Task<Boolean> IsFileUpload(int id)
        {
            var currentFile = await _fileService.GetFileByIdAsync(id);
            var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Storages/Uploads", currentFile.Name);
            if (System.IO.File.Exists(filePath))
            {
                return true;
            }
            return false;
        }


        [HttpPost("UpdateFile/{id}")]
        public async Task<IActionResult> UpdateFile(int id, IFormFile uploadFile)
        {
            if (uploadFile.Length > 0 && uploadFile != null)
            {
                // update File
                var file = await _fileService.GetFileByIdAsync(id);
                DeleteFileUpload(file.Name);
                file.Name = await SaveFile(uploadFile);
                file.Extension = Path.GetExtension(file.Name);
                file.ModifiedAt = DateTime.Now;
                file.ModifiedBy = User.Identity.Name;
                await _fileService.UpdateFileAsync(file);
            }
            return StatusCode(201);
        }
    }
}
