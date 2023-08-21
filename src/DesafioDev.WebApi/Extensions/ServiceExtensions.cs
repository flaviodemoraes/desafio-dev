using DesafioDev.Operacoes.Applications.AutoMapper;
using DesafioDev.Operacoes.Applications.Services.Impl;
using DesafioDev.Operacoes.Applications.Services;
using DesafioDev.Operacoes.Domain.Repositories;
using DesafioDev.Operacoes.Infra.Logs;
using DesafioDev.Operacoes.Infra.Repositories;
using DesafioDev.Operacoes.Infra;

namespace DesafioDev.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static WebApplicationBuilder AddDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

            builder.Services.AddSingleton<Core.Logs.ILogger, ConsoleLogger>();

            builder.Services.AddTransient<IOperacaoRepository, OperacaoRepository>();

            builder.Services.AddTransient<IOperacaoAppService, OperacaoAppService>();

            builder.Services.AddTransient<ITranslateFileDataAppService, TranslateFileDataAppService>();

            builder.Services.AddDbContext<OperacoesDbContext>();

            return builder;
        }
    }
}
