using SorteadorCore.Domain.Entities;
using SorteadorCore.Domain.Interfaces.Repository;
using SorteadorCore.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SorteadorCore.Infra.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario, BancoContexto>, IUsuarioRepository
    {
        public UsuarioRepository(BancoContexto db) : base(db)
        {

        }

        public override Usuario Add(Usuario obj)
        {
            obj.Password = Convert.ToBase64String(Encoding.Unicode.GetBytes(obj.Password));
            return base.Add(obj);
        }

        public Usuario ObterUsuario(string username, string password)
        {
            string hash = Convert.ToBase64String(Encoding.Unicode.GetBytes(password));
            return Db.Usuarios.FirstOrDefault(u => u.Username == username && u.Password == hash);
        }
    }
}
