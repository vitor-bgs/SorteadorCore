using System.Collections.Generic;
using System.Linq;

namespace SorteadorFolgados.Domain.Entities
{
    public class Sala
    {
        public int SalaId { get; set; }
        public string Nome { get; set; }
        public int QuantidadeVencedoresMaioresPontos { get; set; }
        public int QuantidadeVencedoresMenoresPontos { get; set; }

        public List<Participante> DeterminarVencedores(List<Participante> participantes)
        {
            List<Participante> vencedores = new List<Participante>();
            vencedores.AddRange(participantes.Take(QuantidadeVencedoresMaioresPontos));
            vencedores.AddRange(participantes.Take(QuantidadeVencedoresMenoresPontos));
            return vencedores;
        }
    }
}
