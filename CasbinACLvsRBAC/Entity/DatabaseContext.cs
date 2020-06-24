using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasbinACLvsRBAC.Entity
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<CasbinRule> CasbinRule { get; set; }

        private readonly IEntityTypeConfiguration<CasbinRule> _casbinModelConfig;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IEntityTypeConfiguration<CasbinRule> casbinModelConfig) : base(options)
        {
            _casbinModelConfig = casbinModelConfig;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (_casbinModelConfig != null)
            {
                modelBuilder.ApplyConfiguration(_casbinModelConfig);
            }
        }
    }
}
