using System;
using System.Collections.Generic;
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

        public Sorteio()
        {
        }
    }
}
