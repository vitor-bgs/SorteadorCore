using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorteadorCore.Web.ViewModel
{
    public class SalaViewModel
    {
        [Key]
        [Display(AutoGenerateField = false)]
        public int SalaId { get; set; }

        [Required(ErrorMessage = "Preencha o campo")]
        [MaxLength(150, ErrorMessage = "Máximo 150 Caracteres")]
        public string Nome { get; set; }

        [DisplayName("Vencedores (Maiores pontos)")]
        [Required(ErrorMessage = "Preencha o campo Vencedores (Maiores pontos)")]
        [Range(0, int.MaxValue, ErrorMessage = "Preencha com um valor maior ou igual a zero")]
        public int QuantidadeVencedoresMaioresPontos { get; set; }

        [DisplayName("Vencedores (Menores Pontos)")]
        [Required(ErrorMessage = "Preencha o campo Vencedores (Menores pontos)")]
        [Range(0, int.MaxValue, ErrorMessage = "Preencha com um valor maior ou igual a zero")]
        public int QuantidadeVencedoresMenoresPontos { get; set; }

        public bool Ativo { get; set; }

        public bool EstaNoSorteioAtual { get; set; }
    }
}
