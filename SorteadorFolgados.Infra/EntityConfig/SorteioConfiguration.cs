using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SorteadorFolgados.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Infra.EntityConfig
{
    public class SorteioConfiguration : IEntityTypeConfiguration<Sorteio>
    {
        public void Configure(EntityTypeBuilder<Sorteio> builder)
        {
            builder.ToTable("Sorteio");
            builder.HasKey(s => s.SorteioId);
            builder.HasOne<Sala>(s => s.Sala);
            builder.HasMany<SorteioDetalhe>(s => s.Participacoes);
            builder.Property(s => s.DataInicio)
                .IsRequired();
            builder.Property(s => s.DataEncerramento);
            builder.Property(s => s.Ativo)
                .IsRequired();
        }
    }
}
