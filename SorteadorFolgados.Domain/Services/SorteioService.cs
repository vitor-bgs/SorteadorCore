using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces;
using System;

namespace SorteadorFolgados.Domain.Services
{
    public class SorteioService
    {
        ISorteioRepository _SorteioRepository;
        public SorteioService(ISorteioRepository _sorteioRepository)
        {
            _SorteioRepository = _sorteioRepository;
        }

        public Sorteio AdicionarSorteio(string nomeSorteio)
        {
            var sorteio = new Sorteio() { Nome = nomeSorteio, Data = DateTime.Now };
            return _SorteioRepository.AdicionarSorteio(sorteio);
        }

        public Participante SortearParticipante(string nomeParticipante, string enderecoIP, int sorteioId)
        {
            int numeroSorteado = new Random().Next(0, 1000);
            var participante = new Participante { Nome = nomeParticipante, Pontos = numeroSorteado, Data = DateTime.Now, EnderecoIP = enderecoIP };
            return _SorteioRepository.SortearParticipante(participante, sorteioId);
        }

        public Sorteio ObterSorteioAtual()
        {
            return _SorteioRepository.ObterSorteioAtual();
        }
    }
}
