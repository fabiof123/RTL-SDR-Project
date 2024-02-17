using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtlSdrServer.Lettura.Application
{
    public class LetturaConfiguration : IEntityTypeConfiguration<Lettura>
    {
        public void Configure(EntityTypeBuilder<Lettura> builder)
        {
            builder.ToTable("Letture");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Data).IsRequired();
            builder.Property(e => e.FrequenzaIniziale).HasPrecision(20, 10).IsRequired();
            builder.Property(e => e.FrequenzaFinale).HasPrecision(20, 10).IsRequired();
            builder.OwnsOne(e => e.Posizione)
                .Property(x => x.Latitudine).HasPrecision(20, 10).IsRequired().HasColumnName("PosizioneLatitudine");
            builder.OwnsOne(e => e.Posizione)
                .Property(x => x.Longitudine).HasPrecision(20, 10).IsRequired().HasColumnName("PosizioneLongitudine");
            builder.HasMany(x => x.PotenzaSegnali).WithOne()
                .HasForeignKey(x => x.LetturaId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(x => x.PotenzaSegnali).AutoInclude();
        }
    }
}
