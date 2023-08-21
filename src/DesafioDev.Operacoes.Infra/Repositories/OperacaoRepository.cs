using DesafioDev.Core.Data;
using DesafioDev.Core.Logs;
using DesafioDev.Operacoes.Domain.Entities;
using DesafioDev.Operacoes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DesafioDev.Operacoes.Infra.Repositories
{
    public class OperacaoRepository : IOperacaoRepository
    {
        private readonly OperacoesDbContext _context;
        private readonly ILogger _logger;

        public OperacaoRepository(OperacoesDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<EntityEntry<Loja>> AddLojaAsync(Loja entity)
        {
            _logger.Info<OperacaoRepository>("Add model Loja.");

            var model = await _context.Lojas.AddAsync(entity);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<EntityEntry<Operacao>> AddOperacaoAsync(Operacao entity)
        {
            _logger.Info<OperacaoRepository>("Add model Operacao.");

            var model = await _context.Operacoes.AddAsync(entity);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<List<Loja>> GetListLojasAsync(int page = 0, int limit = 0)
        {
            _logger.Info<OperacaoRepository>("Get all list 'Lojas'.");

            var values = GetPagination(page, limit);

            return await _context.Lojas.AsNoTracking()
                .Skip(values.Item2)
                .Take(values.Item1)
                .ToListAsync();
        }

        public async Task<List<Operacao>> GetListOperacoesByLojaAsync(Guid lojaId, int page = 0, int limit = 0)
        {
            _logger.Info<OperacaoRepository>("Get all list 'Lojas'.");

            var values = GetPagination(page, limit);

            return await _context.Operacoes
                .Include(x => x.TipoTransacao)
                .Include(x => x.Loja)
                .Where(x => x.LojaId == lojaId)
                .Skip(values.Item2)
                .Take(values.Item1)
                .ToListAsync();
        }

        public async Task<List<Operacao>> GetListOperacoesByLojaNoPaginationAsync(Guid lojaId)
        {
            _logger.Info<OperacaoRepository>("Get all list 'Lojas' no pagination .");

            return await _context.Operacoes
                .Include(x => x.TipoTransacao)
                .Include(x => x.Loja)
                .Where(x => x.LojaId == lojaId)
                .ToListAsync();
        }

        public async Task<TipoTransacao?> GetTipoTransacaoById(int id)
        {
            _logger.Info<OperacaoRepository>("Get 'TipoTransacao' by id.");

            var model = await _context.TiposTransacoes.FindAsync(id);

            return model;
        }

        private (int, int) GetPagination(int page, int limit)
        {
            int take = 10;
            int skip = 0;

            if (limit != 0 && page != 0)
            {
                take = limit;
                skip = (page - 1) * take;
            }

            return (take, skip);
        }
        public async Task<Loja?> GetListLojaByNameAsync(string nomeLoja)
        {
            _logger.Info<OperacaoRepository>("Get 'Loja' by NomeLoja.");

            return await _context.Lojas.AsNoTracking().Where(x => x.NomeLoja == nomeLoja).FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            _logger.Info<OperacaoRepository>("Dispose.");
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
