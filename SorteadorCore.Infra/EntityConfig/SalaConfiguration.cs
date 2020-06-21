using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SorteadorCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorCore.Infra.EntityConfig
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.ToTable("Sala");
            builder.HasKey(s => s.SalaId);

            builder.Property(s => s.Nome)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("VARCHAR");

            builder.Property(s => s.QuantidadeVencedoresMaioresPontos)
                .IsRequired();

            builder.Property(s => s.QuantidadeVencedoresMenoresPontos)
                .IsRequired();
        }
    }
}
