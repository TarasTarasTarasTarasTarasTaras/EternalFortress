using EternalFortress.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace EternalFortress.Data.EF.Context
{
    public class EternalFortressContext: DbContext
    {
        public EternalFortressContext()
        {

        }

        public EternalFortressContext(DbContextOptions<EternalFortressContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Folder> Folder { get; set; }

        public DbSet<Entities.FileInfo> FileInfo { get; set; }
    }
}
