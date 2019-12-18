
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.Domain.Interfaces;

namespace SorteadorFolgados.Infra
{
    public class SorteioRepository : ISorteioRepository
    {
        private static Sorteio _Sorteio { get; set; }
        public Sorteio AdicionarSorteio(Sorteio sorteio)
        {
            _Sorteio = sorteio;
            return _Sorteio;
        }

        public Sorteio ObterSorteioAtual()
        {
            if(_Sorteio == null)
            {
                _Sorteio = new Sorteio();
            }
            return _Sorteio;
        }

        public Participante SortearParticipante(Participante participante, int SorteioId)
        {
            if(_Sorteio == null)
            {
                _Sorteio = new Sorteio() { Nome = "" };
            }
            participante.ParticipanteId = _Sorteio.Participantes.Count + 1;
            _Sorteio.Participantes.Add(participante);

            return participante;
        }
    }
}
