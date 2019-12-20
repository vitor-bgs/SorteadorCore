using SorteadorFolgados.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorteadorFolgados.Infra.Context
{
    public static class BancoDadosFake<TEntity>
    {
        private static List<TEntity> _Lista { get; set; }

        public static List<TEntity> Lista {
            get
            {
                if(_Lista == null)
                {
                    _Lista = new List<TEntity>();
                }
                return _Lista;
            }
        }
    }
}
