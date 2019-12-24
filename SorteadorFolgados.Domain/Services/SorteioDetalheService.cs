using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Domain.Interfaces.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SorteadorFolgados.Domain.Services
{
    public class SorteioDetalheService : ServiceBase<SorteioDetalhe>, ISorteioDetalheService
    {
        private readonly ISorteioDetalheRepository _sorteioDetalhesRepository;

        public SorteioDetalheService(ISorteioDetalheRepository sorteioDetalhesRepository)
            : base(sorteioDetalhesRepository)
        {
            _sorteioDetalhesRepository = sorteioDetalhesRepository;
        }

        public void Sortear(Sorteio sorteioAtual, Participante participante, string EnderecoIP)
        {
            if(sorteioAtual.Participacoes.Any(p => p.Participante == participante))
            {
                throw new Exception("Você já está participando do sorteio.");
            }

            var sorteioDetalhe = new SorteioDetalhe();
            sorteioDetalhe.Participante = participante;
            sorteioDetalhe.Sorteio = sorteioAtual;
            sorteioDetalhe.EnderecoIP = EnderecoIP;
            sorteioDetalhe.Pontos = new Random().Next(0, 1000);
            sorteioDetalhe.DataParticipacao = DateTime.Now;
            base.Add(sorteioDetalhe);
        }

        public List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId)
        {
            return _sorteioDetalhesRepository.GetSorteioDetalhes(sorteioId);
        }
    }
}
