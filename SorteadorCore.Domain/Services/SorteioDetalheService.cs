using SorteadorCore.Domain.Entities;
using SorteadorCore.Domain.Interfaces.Repository;
using SorteadorCore.Domain.Interfaces.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SorteadorCore.Domain.Services
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
            sorteioDetalhe.ParticipacaoValida = true;
            base.Add(sorteioDetalhe);
        }

        public List<SorteioDetalhe> GetSorteioDetalhes(int sorteioId)
        {
            return _sorteioDetalhesRepository.GetSorteioDetalhes(sorteioId);
        }

        public void MarcarParticipacaoComoInvalida(SorteioDetalhe participacao)
        {
            participacao.ParticipacaoValida = false;
            _sorteioDetalhesRepository.Update(participacao);
        }

        public void MarcarParticipacaoComoValida(SorteioDetalhe participacao)
        {
            participacao.ParticipacaoValida = true;
            _sorteioDetalhesRepository.Update(participacao);
        }
    }
}
