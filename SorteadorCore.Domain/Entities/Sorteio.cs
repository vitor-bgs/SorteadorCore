using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SorteadorCore.Domain.Entities
{
    public class Sorteio
    {
        public int SorteioId { get; set; }
        public int SalaId { get; set; }
        public Sala Sala { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataEncerramento { get; set; }
        public bool Ativo { get; set; }
        public List<SorteioDetalhe> Participacoes { get; set; }

        public List<SorteioDetalhe> ObterVencedores()
        {
            var vencedores = new List<SorteioDetalhe>();
            if(this.Participacoes.Count == 0 || this.Participacoes.Where(p => p.ParticipacaoValida).Count() == 0)
            {
                return vencedores;
            }
            int quantidadeVencedoresMaioresPontos = 
                (this.Sala.QuantidadeVencedoresMaioresPontos <= this.Participacoes.Count) ? 
                this.Sala.QuantidadeVencedoresMaioresPontos : this.Participacoes.Count;

            int quantidadeVencedoresMenoresPontos =
                (quantidadeVencedoresMaioresPontos + this.Sala.QuantidadeVencedoresMenoresPontos) <= this.Participacoes.Count ?
                this.Sala.QuantidadeVencedoresMenoresPontos : this.Participacoes.Count - quantidadeVencedoresMaioresPontos;


            if(quantidadeVencedoresMaioresPontos >= 1)
            {
                vencedores.AddRange(
                this.Participacoes
                .Where(p => p.ParticipacaoValida)
                .OrderByDescending(p => p.Pontos)
                .ToList()
                .GetRange(0, quantidadeVencedoresMaioresPontos)
                );
            }


            if(quantidadeVencedoresMenoresPontos >= 1)
            {
                vencedores.AddRange(
                    this.Participacoes
                    .Where(p => p.ParticipacaoValida)
                    .OrderBy(p => p.Pontos)
                    .ToList()
                    .GetRange(0, quantidadeVencedoresMenoresPontos));
            }
            
            return vencedores;
        }

        public Sorteio()
        {
            this.Participacoes = new List<SorteioDetalhe>();
        }
    }
}
