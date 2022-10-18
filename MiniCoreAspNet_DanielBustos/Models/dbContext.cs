using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCoreAspNet_DanielBustos.Models
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options)
        : base(options)
        {
        }
        public DbSet<Pase> Pases { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
