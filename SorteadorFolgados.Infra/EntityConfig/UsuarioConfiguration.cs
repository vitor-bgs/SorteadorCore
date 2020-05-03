using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SorteadorFolgados.Domain.Entities;

namespace SorteadorFolgados.Infra.EntityConfig
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(u => u.UsuarioId );
            builder.Property(s => s.Username)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(s => s.Password)
                .HasColumnType("VARCHAR")
                .HasMaxLength(300)
                .IsRequired();
        }
    }
}
