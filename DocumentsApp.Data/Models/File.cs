using System;

namespace DocumentsApp.Data.Models
{
    public class File
    {
        public File() { CreatedAt = DateTime.Now; ModifiedAt = DateTime.Now; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }

    }
}
