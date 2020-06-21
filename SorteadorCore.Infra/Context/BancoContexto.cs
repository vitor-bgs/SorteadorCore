using Microsoft.EntityFrameworkCore;
using SorteadorCore.Domain.Entities;
using SorteadorCore.Infra.EntityConfig;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorCore.Infra.Context
{
    public class BancoContexto : DbContext
    {
        public BancoContexto(DbContextOptions<BancoContexto> options) : base(options) { }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Sorteio> Sorteios { get; set; }
        public DbSet<SorteioDetalhe> SorteioDetalhes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<Participante>(new ParticipanteConfiguration());
            builder.ApplyConfiguration<Sala>(new SalaConfiguration());
            builder.ApplyConfiguration<Sorteio>(new SorteioConfiguration());
            builder.ApplyConfiguration<SorteioDetalhe>(new SorteioDetalheConfiguration());
            builder.ApplyConfiguration<Usuario>(new UsuarioConfiguration());

        }
    }
}
