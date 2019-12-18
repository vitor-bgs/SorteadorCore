using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Domain.Entities
{
    public class Sorteio
    {
        public Sorteio()
        {
            Participantes = new List<Participante>();
        }
        public int SorteioId { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public List<Participante> Participantes { get; set; }
    }
}
