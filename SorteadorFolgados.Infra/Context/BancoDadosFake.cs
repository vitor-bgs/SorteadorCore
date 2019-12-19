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

        private static List<Sorteio> _ListaSorteio { get; set; }
        private static List<Participante> _ListaParticipante { get; set; }
        private static List<SorteioDetalhe> _ListaSorteioDetalhes { get; set; }
        private static List<Sala> _ListaSala { get; set; }

        public static List<Sorteio> ListaSorteio {
            get
            {
                if(_ListaSorteio == null)
                {
                    _ListaSorteio = new List<Sorteio>();
                }
                return _ListaSorteio;
            }
        }
        public static List<Participante> ListaParticipante
        {
            get
            {
                if (_ListaParticipante == null)
                {
                    _ListaParticipante = new List<Participante>();
                }
                return _ListaParticipante;
            }
        }
        public static List<SorteioDetalhe> ListaSorteioDetalhes
        {
            get
            {
                if (_ListaSorteioDetalhes == null)
                {
                    _ListaSorteioDetalhes = new List<SorteioDetalhe>();
                }
                return _ListaSorteioDetalhes;
            }
        }
        public static List<Sala> ListaSala
        {
            get
            {
                if (_ListaSala == null)
                {
                    _ListaSala = new List<Sala>();
                }
                return _ListaSala;
            }
        }
    }
}
