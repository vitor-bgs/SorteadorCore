using SorteadorCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorCore.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        bool Login(Usuario usuario);
    }
}
