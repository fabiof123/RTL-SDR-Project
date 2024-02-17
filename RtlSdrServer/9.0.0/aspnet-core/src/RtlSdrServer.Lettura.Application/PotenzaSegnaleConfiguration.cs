using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application
{
    public class PotenzaSegnaleConfiguration : IEntityTypeConfiguration<PotenzaSegnale>
    {
        public void Configure(EntityTypeBuilder<PotenzaSegnale> builder)
        {
            builder.ToTable("PotenzaSegnali");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Frequenza).HasPrecision(20, 10).IsRequired();
            builder.Property(x => x.Valore).HasPrecision(20, 10).IsRequired();/*decimal precision custom 20 cifre di cui 10 decimali*/
        }
    }
}
