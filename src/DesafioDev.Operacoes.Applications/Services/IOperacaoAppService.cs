using DesafioDev.Operacoes.Applications.ViewModel;

namespace DesafioDev.Operacoes.Applications.Services
{
    public interface IOperacaoAppService : IDisposable
    {
        Task SaveOperacoes(List<OperacaoViewModel> lista);
        Task<List<LojaViewModel>> GetListLojasAsync(int page = 0, int limit = 0);
        Task<OperacaoByLojaViewModel> GetListOperacoesByLojaAsync(Guid id, int page = 0, int limit = 0);
    }
}
