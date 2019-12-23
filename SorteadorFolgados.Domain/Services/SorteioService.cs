using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Domain.Interfaces.Services;
using System;

namespace SorteadorFolgados.Domain.Services
{
    public class SorteioService : ServiceBase<Sorteio>, ISorteioService
    {
        ISorteioRepository _sorteioRepository;
        public SorteioService(ISorteioRepository sorteioRepository)
            :base(sorteioRepository)
        {
            _sorteioRepository = sorteioRepository;
        }

        public void EncerrarSorteioAtual()
        {
            var sorteioAtual = _sorteioRepository.ObterSorteioAtual();
            if (sorteioAtual != null)
            {
                sorteioAtual.Ativo = false;
                sorteioAtual.DataEncerramento = DateTime.Now;
                _sorteioRepository.Update(sorteioAtual);
            }
        }

        public void IniciarNovoSorteio(Sala sala)
        {
            EncerrarSorteioAtual();
            var novoSorteio = new Sorteio() { Sala = sala, DataInicio = DateTime.Now, Ativo = true, SalaId = sala.SalaId };
            _sorteioRepository.Add(novoSorteio);
        }

        public Sorteio ObterSorteioAtual()
        {
            return _sorteioRepository.ObterSorteioAtual();
        }

    }
}
