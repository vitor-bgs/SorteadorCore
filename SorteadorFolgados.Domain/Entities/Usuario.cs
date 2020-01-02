using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
