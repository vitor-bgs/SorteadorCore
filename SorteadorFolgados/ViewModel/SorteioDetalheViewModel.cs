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
        [Key]
        public int SorteioDetalheId { get; set; }

        public int SorteioId { get; set; }

        public virtual ParticipanteViewModel Participante { get; set; }

        [DisplayName("Endereço IP")]
        public string EnderecoIP { get; set; }
        public int Pontos { get; set; }

        [DisplayName("Data")]
        public DateTime DataParticipacao { get; set; }
    }
}
