using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SorteadorFolgados.Domain.Entities
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
            if(this.Participacoes.Count == 0)
            {
                return vencedores;
            }
            vencedores.AddRange(this.Participacoes.OrderByDescending(p => p.Pontos).ToList().GetRange(0, this.Sala.QuantidadeVencedoresMaioresPontos));
            vencedores.AddRange(this.Participacoes.OrderBy(p => p.Pontos).ToList().GetRange(0, this.Sala.QuantidadeVencedoresMenoresPontos));
            return vencedores;
        }

        public Sorteio()
        {
            this.Participacoes = new List<SorteioDetalhe>();
        }
    }
}
