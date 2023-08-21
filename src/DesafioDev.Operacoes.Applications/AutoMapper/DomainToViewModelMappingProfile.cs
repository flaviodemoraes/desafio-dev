using AutoMapper;
using DesafioDev.Operacoes.Applications.ViewModel;
using DesafioDev.Operacoes.Domain.Entities;

namespace DesafioDev.Operacoes.Applications.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Operacao, OperacaoViewModel>();
            CreateMap<TipoTransacao, TipoTransacaoViewModel>();
            CreateMap<Loja, LojaViewModel>();
        }
    }
}
