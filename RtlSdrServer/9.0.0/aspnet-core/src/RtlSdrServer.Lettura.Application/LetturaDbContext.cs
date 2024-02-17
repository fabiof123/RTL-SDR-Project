using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application
{
    public class LetturaDbContext : AbpDbContext
    {
        public DbSet<Lettura> Letture { get; set; }

        public LetturaDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LetturaConfiguration());
            modelBuilder.ApplyConfiguration(new PotenzaSegnaleConfiguration());
            /*modelBuilder.ApplyConfigurationsFromAssembly()*//*per non fare il collegamento tutte le volte posso dargli l'assembly !!*/
        }
    }
}
