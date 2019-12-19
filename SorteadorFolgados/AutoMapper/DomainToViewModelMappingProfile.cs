using AutoMapper;
using SorteadorFolgados.Domain.Entities;
using SorteadorFolgados.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorteadorFolgados.AutoMapper
{
    public class DomainToViewProfile : Profile
    {
        public DomainToViewProfile()
        {
            CreateMap<Participante, ParticipanteViewModel>();
            CreateMap<Sorteio, SorteioViewModel>();
            CreateMap<SorteioDetalhe, SorteioDetalheViewModel>();
            CreateMap<Sala, SalaViewModel>();
        }
    }
}
