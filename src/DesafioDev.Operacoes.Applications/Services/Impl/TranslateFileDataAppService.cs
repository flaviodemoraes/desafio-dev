using AutoMapper;
using DesafioDev.Core.Logs;
using DesafioDev.Operacoes.Applications.ViewModel;
using DesafioDev.Operacoes.Domain.Entities;

namespace DesafioDev.Operacoes.Applications.Services.Impl
{
    public class TranslateFileDataAppService : ITranslateFileDataAppService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOperacaoAppService _service;


        public TranslateFileDataAppService(
            ILogger logger,
            IMapper mapper,
            IOperacaoAppService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        public async Task TranslateFile(StreamReader file)
        {
            try
            {
                _logger.Info<OperacaoAppService>($"[HandleSaveFileData] Get date file.");

                string line;

                var listViewModel = new List<OperacaoViewModel>();

                while ((line = file.ReadLine()) != null)
                {
                    var loja = new LojaViewModel()
                    {
                        NomeProprietario = line.Substring(48, 14).ToString().Trim(),
                        NomeLoja = line.Substring(62, 18).ToString().Trim(),
                    };

                    var operacao = new OperacaoViewModel()
                    {
                        TipoTransacaoId = Convert.ToInt32(line.Substring(0, 1)),
                        Loja = loja,
                        DataOcorrencia = ConvertDate(line.Substring(1, 8).ToString()),
                        Valor = Convert.ToDecimal(line.Substring(9, 10).ToString()),
                        Cpf = line.Substring(19, 11).ToString(),
                        CartaoTransacao = line.Substring(30, 12).ToString(),
                        HoraOcorrencia = ConvertHora(line.Substring(42, 6).ToString())
                    };

                    var modelOperacao = _mapper.Map<Operacao>(operacao);

                    listViewModel.Add(operacao);
                }

                _logger.Info<OperacaoAppService>($"[HandleSaveFileData] Send file to save Cnab.");

                await _service.SaveOperacoes(listViewModel);

            }
            catch (Exception ex)
            {
                _logger.Error<OperacaoAppService>($"[HandleSaveFileData] Error in model handler Cnab. Error: {ex.Message}");
                throw;
            }
        }

        private static DateTime ConvertDate(string date)
        {
            var year = date.Substring(0, 4);
            var month = date.Substring(4, 2);
            var day = date.Substring(6, 2);

            return Convert.ToDateTime($"{year}-{month}-{day}");
        }

        private static string ConvertHora(string hora)
        {
            var hour = hora.Substring(0, 2);
            var minute = hora.Substring(2, 2);
            var second = hora.Substring(4, 2);

            return $"{hour}:{minute}:{second}";
        }
    }
}
