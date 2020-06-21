using AutoMapper;

using SorteadorCore.Domain.Entities;
using SorteadorCore.Web.ViewModel;

namespace SorteadorCore.Web.AutoMapper
{
    public class DomainToViewProfile : Profile
    {
        public DomainToViewProfile()
        {
            CreateMap<Participante, ParticipanteViewModel>();
            CreateMap<Sorteio, SorteioViewModel>();
            CreateMap<SorteioDetalhe, SorteioDetalheViewModel>();
            CreateMap<Sala, SalaViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}
