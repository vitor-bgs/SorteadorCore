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
        public List<SorteioDetalhe> Participacoes { get; set; }

        public Sorteio(Sala sala)
        {
            SalaId = sala.SalaId;
            Sala = sala;
            DataInicio = DateTime.Now;
            Participacoes = new List<SorteioDetalhe>();
        }
    }
}
