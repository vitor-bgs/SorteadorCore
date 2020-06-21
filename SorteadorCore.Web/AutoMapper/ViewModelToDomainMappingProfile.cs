using AutoMapper;
using SorteadorCore.Domain.Entities;
using SorteadorCore.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorteadorCore.Web.AutoMapper
{
    public class ViewToDomainProfile : Profile
    {
        public ViewToDomainProfile()
        {
            CreateMap<ParticipanteViewModel, Participante>();
            CreateMap<SorteioViewModel, Sorteio>();
            CreateMap<SorteioDetalheViewModel, SorteioDetalhe>();
            CreateMap<SalaViewModel, Sala>();
            CreateMap<UsuarioViewModel, Usuario>();
        }
    }
}
