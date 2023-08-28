using AutoMapper;
using DesafioDev.Core.Logs;
using DesafioDev.Operacoes.Applications.ViewModel;
using DesafioDev.Operacoes.Domain.Entities;
using DesafioDev.Operacoes.Domain.Repositories;
using System.Text.Json;

namespace DesafioDev.Operacoes.Applications.Services.Impl
{
    public class OperacaoAppService : IOperacaoAppService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOperacaoRepository _repository;
        public OperacaoAppService(ILogger logger, IMapper mapper, IOperacaoRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<LojaViewModel>> GetListLojasAsync(int page = 0, int limit = 0)
        {
            try
            {
                _logger.Info<OperacaoAppService>($"[GetListLojasAsync] get all 'lojas'.");
                return _mapper.Map<List<LojaViewModel>>(await _repository.GetListLojasAsync(page, limit));
            }
            catch (Exception ex)
            {
                _logger.Error<OperacaoAppService>($"[GetListLojasAsync] Error: {ex.Message}");
                Dispose();
                throw;
            }
        }

        public async Task<OperacaoByLojaViewModel> GetListOperacoesByLojaAsync(Guid id, int page = 0, int limit = 0)
        {
            try
            {
                _logger.Info<OperacaoAppService>($"[GetListOperacoesByLojaAsync] Get operation by 'Lojas'.");

                var viewModel = _mapper.Map<List<OperacaoViewModel>>(await _repository.GetListOperacoesByLojaAsync(id, page, limit));

                var viewModelSaldo = _mapper.Map<List<OperacaoViewModel>>(await _repository.GetListOperacoesByLojaNoPaginationAsync(id));

                OperacaoByLojaViewModel model = new OperacaoByLojaViewModel();
                model.Operacoes = viewModel;

                var modelSaldoEntrada = viewModelSaldo.Where(x => x.TipoTransacao.Natureza == "Entrada").Sum(x => x.Valor);

                var modelSaldoSaida = viewModelSaldo.Where(x => x.TipoTransacao.Natureza == "Saída").Sum(x => x.Valor);

                model.TotalSaldo = modelSaldoEntrada - modelSaldoSaida;

                return model;

            }
            catch (Exception ex)
            {
                _logger.Error<OperacaoAppService>($"[GetListOperacoesByLojaAsync] Error: {ex.Message}");
                _repository.Dispose();
                throw;
            }
        }

        public async Task SaveOperacoes(List<OperacaoViewModel> lista)
        {

            try
            {
                var json = JsonSerializer.Serialize(lista);
                _logger.Info<OperacaoAppService>($"[SaveOperacoes] {json}");

                await _repository.UnitOfWork.BeginTransactionAsync();

                foreach (var operacao in lista)
                {
                    var tipoTransacao = await _repository.GetTipoTransacaoById(operacao.TipoTransacaoId);

                    if (tipoTransacao == null)
                    {
                        json = JsonSerializer.Serialize(operacao);
                        _logger.Error<OperacaoAppService>($"[SaveOperacoes] Tipo de Transacao não existe. {json}");
                        break;
                    }

                    // Validate Loja
                    var loja = await _repository.GetListLojaByNameAsync(operacao.Loja.NomeLoja);

                    if (loja == null) loja = await SaveLoja(operacao.Loja);

                    operacao.LojaId = loja.LojaId;
                    operacao.Loja = null;

                    operacao.TipoTransacaoId = tipoTransacao.Tipo;
                    operacao.TipoTransacao = null;

                    await _repository.AddOperacaoAsync(_mapper.Map<Operacao>(operacao));
                }

                await _repository.UnitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.Error<OperacaoAppService>($"[SaveOperacoes] Error: {ex.Message}");
                _repository.UnitOfWork.Rollback();
                throw;
            }
        }

        private async Task<Loja?> SaveLoja(LojaViewModel viewModel)
        {
            var loja = await _repository.AddLojaAsync(_mapper.Map<Loja>(viewModel));
            return loja.Entity;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
