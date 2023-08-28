using AutoMapper;
using DesafioDev.Core.Logs;
using DesafioDev.Operacoes.Applications.Services.Impl;
using DesafioDev.Operacoes.Applications.ViewModel;
using DesafioDev.Operacoes.Domain.Entities;
using DesafioDev.Operacoes.Domain.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace DesafioDev.Tests.Operacoes.Applications
{
    [TestFixture]
    internal class OperacaoAppServiceTest
    {

        Mock<ILogger> _logger;
        Mock<IMapper> _mapper;
        Mock<IOperacaoRepository> _repository;

        OperacaoAppService _service;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<IOperacaoRepository>();

            _service = new OperacaoAppService(_logger.Object, _mapper.Object, _repository.Object);
        }

        [Test]
        public async Task GetListLojasAsync_WithSuccess()
        {
            // Arrange
            var list = new List<Loja>()
                {
                    new Loja("Lojas 1000", "Amarildo Passos"),
                    new Loja("Construções Pa", "Alvaro Furtado"),
                };

            var listVM = new List<LojaViewModel>()
                {
                    new LojaViewModel(){ NomeLoja = "Lojas 1000", NomeProprietario = "Amarildo Passos" },
                    new LojaViewModel(){ NomeLoja = "Construções Pa", NomeProprietario = "Alvaro Furtado" },
                };
            
            _mapper.Setup(x => x.Map<List<LojaViewModel>>(list)).Returns(listVM);
            _repository.Setup(x => x.GetListLojasAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(list));

            // Act
            var result = await _service.GetListLojasAsync(1, 10);

            // Assert
            Assert.That(result.Count, Is.EqualTo(listVM.Count));

            _mapper.Verify(x => x.Map<List<LojaViewModel>>(list));
            _repository.Verify(x => x.GetListLojasAsync(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public async Task GetListOperacoesByLojaAsync_WithSuccess()
        {
            // Arrange
            var tipoTransacaoEntrada = new TipoTransacaoViewModel() { Tipo = 1, Descricao = "Débito", Natureza = "Entrada", Sinal = "+" };
            var tipoTransacaoSaida = new TipoTransacaoViewModel() { Tipo = 2, Descricao = "Boleto", Natureza = "Saída", Sinal = "-" };

            var lojas = new List<Loja>()
                {
                    new Loja("Lojas 1000", "Amarildo Passos"),
                    new Loja("Construções Pa", "Alvaro Furtado"),
                };

            var operacoes = new List<Operacao>()
                {
                    new Operacao(1, lojas[0].LojaId, DateTime.UtcNow, 5000, "89553352707", "99xxx999", "10:00:00"),
                    new Operacao(1, lojas[0].LojaId, DateTime.UtcNow, 7500, "11583877258", "99xxx999", "17:15:00"),
                };

            var operacoesVM = new List<OperacaoViewModel>()
                {
                    new OperacaoViewModel()
                    { 
                        TipoTransacaoId = 1, 
                        LojaId = lojas[0].LojaId, 
                        DataOcorrencia = DateTime.UtcNow, 
                        Valor = 15000, 
                        Cpf = "89553352707", 
                        CartaoTransacao = "99xxx999", 
                        HoraOcorrencia = "10:00:00",
                        TipoTransacao = tipoTransacaoEntrada
                    },
                    new OperacaoViewModel() 
                    { 
                        TipoTransacaoId = 2, 
                        LojaId = lojas[0].LojaId, 
                        DataOcorrencia = DateTime.UtcNow, 
                        Valor = 7500, 
                        Cpf = "11583877258", 
                        CartaoTransacao = "99xxx999", 
                        HoraOcorrencia = "17:15:00",
                        TipoTransacao = tipoTransacaoSaida
                    },
                };

            _mapper.Setup(x => x.Map<List<OperacaoViewModel>>(operacoes)).Returns(operacoesVM);

            _repository.Setup(x => x.GetListOperacoesByLojaAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(operacoes));

            _repository.Setup(x => x.GetListOperacoesByLojaNoPaginationAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(operacoes));

            // Act
            var result = await _service.GetListOperacoesByLojaAsync(lojas[0].LojaId, 1, 10);
            var saldo = operacoesVM[0].Valor - operacoesVM[1].Valor;

            // Assert
            Assert.NotNull(result);
            Assert.That(saldo, Is.EqualTo(result.TotalSaldo));
            _repository.Verify(x => x.GetListOperacoesByLojaAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()));
            _repository.Verify(x => x.GetListOperacoesByLojaNoPaginationAsync(It.IsAny<Guid>()));
        }

        [Test]
        public async Task SaveOperacoes_WithSuccess()
        {
            // Arrange
            var tipoTransacaoEntrada = new TipoTransacaoViewModel() { Tipo = 1, Descricao = "Débito", Natureza = "Entrada", Sinal = "+" };
            var tipoTransacaoSaida = new TipoTransacaoViewModel() { Tipo = 2, Descricao = "Boleto", Natureza = "Saída", Sinal = "-" };

            TipoTransacao tipoTransacao = new TipoTransacao(1, "Débito", "Entrada", "+");

            var lojas = new List<Loja>()
                {
                    new Loja("Lojas 1000", "Amarildo Passos"),
                    new Loja("Construções Pa", "Alvaro Furtado"),
                };

            var lojasVM = new List<LojaViewModel>()
                {
                    new LojaViewModel(){ NomeLoja = "Lojas 1000", NomeProprietario = "Amarildo Passos" },
                    new LojaViewModel(){ NomeLoja = "Construções Pa", NomeProprietario = "Alvaro Furtado" },
                };


            var operacoes = new List<Operacao>()
                {
                    new Operacao(1, lojas[0].LojaId, DateTime.UtcNow, 5000, "89553352707", "99xxx999", "10:00:00"),
                    new Operacao(1, lojas[0].LojaId, DateTime.UtcNow, 7500, "11583877258", "99xxx999", "17:15:00"),
                };

            var operacoesVM = new List<OperacaoViewModel>()
                {
                    new OperacaoViewModel()
                    {
                        TipoTransacaoId = 1,
                        LojaId = lojas[0].LojaId,
                        DataOcorrencia = DateTime.UtcNow,
                        Valor = 15000,
                        Cpf = "89553352707",
                        CartaoTransacao = "99xxx999",
                        HoraOcorrencia = "10:00:00",
                        TipoTransacao = tipoTransacaoEntrada,
                        Loja = lojasVM[0]
                    },
                    new OperacaoViewModel()
                    {
                        TipoTransacaoId = 2,
                        LojaId = lojas[0].LojaId,
                        DataOcorrencia = DateTime.UtcNow,
                        Valor = 7500,
                        Cpf = "11583877258",
                        CartaoTransacao = "99xxx999",
                        HoraOcorrencia = "17:15:00",
                        TipoTransacao = tipoTransacaoSaida,
                        Loja = lojasVM[1]
                    },
                };

            _repository.Setup(x => x.UnitOfWork.BeginTransactionAsync());
            _repository.Setup(x => x.UnitOfWork.CommitAsync());
            _repository.Setup(x => x.GetTipoTransacaoById(operacoesVM[0].TipoTransacaoId)).Returns(Task.FromResult(tipoTransacao));
            _repository.Setup(x => x.GetListLojaByNameAsync(lojasVM[0].NomeLoja)).Returns(Task.FromResult(lojas?[0]));

            // Act
            await _service.SaveOperacoes(operacoesVM);

            // Assert
            _repository.Verify(x => x.UnitOfWork.BeginTransactionAsync());
            _repository.Verify(x => x.UnitOfWork.CommitAsync());
            _repository.Verify(x => x.GetTipoTransacaoById(operacoesVM[0].TipoTransacaoId));
            _repository.Verify(x => x.GetListLojaByNameAsync(lojasVM[0].NomeLoja));
        }
    }
}
