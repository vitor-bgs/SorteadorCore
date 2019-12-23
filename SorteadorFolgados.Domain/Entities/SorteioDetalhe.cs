using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Domain.Entities
{
    public class SorteioDetalhe
    {
        public int SorteioDetalheId { get; set; }
        public int SorteioId { get; set; }
        public int ParticipanteId { get; set; }
        public string EnderecoIP { get; set; }
        public int Pontos { get; set; }
        public DateTime DataParticipacao { get; set; }
        public Participante Participante { get; set; }
        public Sorteio Sorteio { get; set; }
    }
}
