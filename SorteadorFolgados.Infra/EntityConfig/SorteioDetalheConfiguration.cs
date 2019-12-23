using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SorteadorFolgados.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Infra.EntityConfig
{
    public class SorteioDetalheConfiguration : IEntityTypeConfiguration<SorteioDetalhe>
    {
        public void Configure(EntityTypeBuilder<SorteioDetalhe> builder)
        {
            builder.ToTable("SorteioDetalhe");
            builder.HasKey(s => s.SorteioDetalheId);
            builder.HasOne<Sorteio>(s => s.Sorteio);
            builder.HasOne<Participante>(s => s.Participante);
            builder.Property(s => s.Pontos)
                .IsRequired();
            builder.Property(s => s.EnderecoIP)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(s => s.DataParticipacao)
                .IsRequired();
        }
    }
}
