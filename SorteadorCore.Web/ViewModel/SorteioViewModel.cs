using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorteadorCore.Web.ViewModel
{
    public class SorteioViewModel
    {
        [Key]
        public int SorteioId { get; set; }
        [DisplayName("Data de Início")]
        public DateTime DataInicio { get; set; }
        public bool Ativo { get; set; }
        public virtual SalaViewModel Sala { get; set; }
        public virtual List<SorteioDetalheViewModel> Participacoes { get; set; }
        public virtual ParticipanteViewModel ParticipanteAtual { get; set; }
    }
}
