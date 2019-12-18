using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Domain.Entities
{
    public class Participante
    {
        public int ParticipanteId { get; set; }
        public string Nome { get; set; }
        public int Pontos { get; set; }
        public string EnderecoIP { get; set; }
        public DateTime Data { get; set; }
    }
}
