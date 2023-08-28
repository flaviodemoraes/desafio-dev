using DesafioDev.Core.Logs;
using DesafioDev.Operacoes.Domain.Entities;
using DesafioDev.Operacoes.Infra;
using DesafioDev.Operacoes.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DesafioDev.Tests.Operacoes.Infra.Repositories
{
    [TestFixture]
    public class OperacaoRepositoryTest
    {
        Mock<ILogger> _loggerMock;
        OperacoesDbContext _dbContext;
        OperacaoRepository _repository;
        List<Loja> _lojas;
        List<TipoTransacao> _tiposTransacoes;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger>();

            var options = new DbContextOptionsBuilder<OperacoesDbContext>()
                .UseInMemoryDatabase(databaseName: "db_desafio")
                .Options;

            _dbContext = new OperacoesDbContext(options);
            _repository = new OperacaoRepository(_dbContext, _loggerMock.Object);
        }

        private async Task PopulateData()
        {
            (_lojas, _tiposTransacoes) = await SaveSecondaryData();
        }

        [Test]
        public async Task SaveOperacaoAsync_WithSuccess()
        {
            // Arrange
            await PopulateData();

            var model = new Operacao(1, _lojas[0].LojaId, DateTime.UtcNow, 10000, "67665850561", "4753****3153", "090000");

            // Act
            await _repository.AddOperacaoAsync(model);

            // Assert
            var savedOperacao = _dbContext.Set<Operacao>().FirstOrDefault();
            Assert.NotNull(savedOperacao);

        }

        [Test]
        public async Task SaveLojaAsync_WithSuccess()
        {
            // Arrange
            var model = new Loja("Lojas 1001", "Tavares Silva");

            // Act
            await _repository.AddLojaAsync(model);

            // Assert
            var loja = await _dbContext.Set<Loja>().Where(x => x.NomeLoja == model.NomeLoja).FirstOrDefaultAsync();

            Assert.NotNull(loja);
            Assert.That(loja.NomeLoja, Is.EqualTo(model.NomeLoja));
            Assert.That(loja.NomeProprietario, Is.EqualTo(model.NomeProprietario));
        }

        [Test]
        public async Task GetListLojasAsync_WithSuccess()
        {
            // Arrange
            await PopulateData();

            //Act
            var list = await _repository.GetListLojasAsync(1, 10);

            // Assert
            Assert.That(list.Count(), Is.EqualTo(_lojas.Count()));
        }

        [Test]
        public async Task GetListOperacoesByLojaAsync_WithSuccess()
        {
            // Arrange
            await PopulateData();

            var listModel = new List<Operacao>()
                {
                    new Operacao(1, _lojas[0].LojaId, DateTime.UtcNow, 5000, "89553352707", "99xxx999", "10:00:00"),
                    new Operacao(1, _lojas[0].LojaId, DateTime.UtcNow, 7500, "11583877258", "99xxx999", "17:15:00"),
                };

            foreach (var model in listModel)
                await _dbContext.Set<Operacao>().AddAsync(model);

            var listSaveChange = await _dbContext.SaveChanges();

            // Act
            var list = await _repository.GetListOperacoesByLojaAsync(_lojas[0].LojaId, 1, 10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(listSaveChange));
        }

        [Test]
        public async Task GetListOperacoesByLojaNoPaginationAsync_WithSuccess()
        {
            // Arrange
            await PopulateData();

            // Act
            var list = await _repository.GetListOperacoesByLojaNoPaginationAsync(_lojas[0].LojaId);

            // Assert
            Assert.That(list.Count, Is.EqualTo(_lojas.Count));

        }

        [Test]
        public async Task GetTipoTransacaoById_WithSuccess()
        {
            // Arrange
            await PopulateData();

            // Act
            var list = await _repository.GetTipoTransacaoById(_tiposTransacoes[0].Tipo);

            // Assert
            Assert.That(list.Descricao, Is.EqualTo(_tiposTransacoes[0].Descricao));
            Assert.That(list.Operacao, Is.EqualTo(_tiposTransacoes[0].Operacao));
            Assert.That(list.Natureza, Is.EqualTo(_tiposTransacoes[0].Natureza));
            Assert.That(list.Sinal, Is.EqualTo(_tiposTransacoes[0].Sinal));
        }

        [Test]
        public async Task GetListLojaByNameAsync_WithSuccess()
        {
            // Arrange
            await PopulateData();

            // Act
            var list = await _repository.GetListLojaByNameAsync(_lojas[0].NomeLoja);

            // Assert
            Assert.That(list.NomeLoja, Is.EqualTo(_lojas[0].NomeLoja));
            Assert.That(list.NomeProprietario, Is.EqualTo(_lojas[0].NomeProprietario));

            _repository.Dispose();
        }

        private async Task<(List<Loja>, List<TipoTransacao>)> SaveSecondaryData()
        {
            var listTiposTransacoes = await _dbContext.Set<TipoTransacao>().AsNoTracking().ToListAsync();
            var listLojas = await _dbContext.Set<Loja>().AsNoTracking().ToListAsync();

            if(listTiposTransacoes.Count() == 0)
            {
                var tipoTransacao = new TipoTransacao(1, "Débito", "Entrada", "+");
                await _dbContext.Set<TipoTransacao>().AddAsync(tipoTransacao);
                await _dbContext.SaveChanges();

                listTiposTransacoes = await _dbContext.Set<TipoTransacao>().AsNoTracking().ToListAsync();
            }

            if(listLojas.Count() == 0)
            {
                var list = new List<Loja>()
                {
                    new Loja("Lojas 1000", "Amarildo Passos"),
                    new Loja("Construções Pa", "Alvaro Furtado"),
                };

                foreach (var loja in list)
                    await _dbContext.Set<Loja>().AddAsync(loja);

                await _dbContext.SaveChanges();

                listLojas = await _dbContext.Set<Loja>().AsNoTracking().ToListAsync();
            }

            return (listLojas, listTiposTransacoes);
        }
    }
}
