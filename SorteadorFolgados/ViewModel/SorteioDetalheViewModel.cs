using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorteadorFolgados.ViewModel
{
    public class SorteioDetalheViewModel
    {
        public int SorteioDetalheId { get; set; }
        public int SorteioId { get; set; }
        public virtual ParticipanteViewModel Participante { get; set; }
        public string EnderecoIP { get; set; }
        public int Pontos { get; set; }
        public bool ParticipacaoValida { get; set; }
        public DateTime DataParticipacao { get; set; }
    }
}
