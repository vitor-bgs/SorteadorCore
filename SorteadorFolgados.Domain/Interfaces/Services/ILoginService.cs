using SorteadorFolgados.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        bool Login(Usuario usuario);
    }
}
