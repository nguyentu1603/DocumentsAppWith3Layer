using Microsoft.EntityFrameworkCore;

namespace DocumentsApp.Data.Models
{
    public class DocumentsDb : DbContext
    {
        public DocumentsDb(DbContextOptions<DocumentsDb> options) : base(options)
        {

        }
        public virtual DbSet<File> Files { get; set; }

    }
}
