using AutoMapper;
using DesafioDev.Operacoes.Applications.ViewModel;
using DesafioDev.Operacoes.Domain.Entities;

namespace DesafioDev.Operacoes.Applications.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<OperacaoViewModel, Operacao>();
            CreateMap<TipoTransacaoViewModel, TipoTransacao>();
            CreateMap<LojaViewModel, Loja>();
        }
    }
}
