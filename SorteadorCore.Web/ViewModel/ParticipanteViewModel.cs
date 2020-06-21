using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorteadorCore.Web.ViewModel
{
    public class ParticipanteViewModel
    {
        [Key]
        public int ParticipanteId { get; set; }

        [Required(ErrorMessage = "Preencha o campo")]
        [MaxLength(150, ErrorMessage = "Máximo 150 Caracteres")]
        public string Nome { get; set; }
    }
}
