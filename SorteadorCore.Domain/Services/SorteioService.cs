using SorteadorCore.Domain.Entities;
using SorteadorCore.Domain.Interfaces.Repository;
using SorteadorCore.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SorteadorCore.Domain.Services
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

        public List<Sorteio> ObterVencedores(DateTime dataInicial, DateTime dataFinal)
        {
            return _sorteioRepository.ObterSorteiosPorData(dataInicial, dataFinal)
                .Where(s => s.Participacoes.Count > 0)
                .Select(s => { 
                    s.Participacoes = s.ObterVencedores();
                    return s; 
                })
                .OrderBy(s => s.DataEncerramento)
                .ToList();
        }

    }
}
