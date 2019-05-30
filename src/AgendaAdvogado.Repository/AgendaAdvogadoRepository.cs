using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaAdvogado.Domain;
using Microsoft.EntityFrameworkCore;

namespace AgendaAdvogado.Repository
{
    public class AgendaAdvogadoRepository : IAgendaAdvogadoRepository
    {
        private readonly AgendaAdvogadoContext _context;

        public AgendaAdvogadoRepository(AgendaAdvogadoContext context)
        {
            _context = context;

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region All
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(List<T> entity) where T : class
        {
            _context.RemoveRange(entity);
        }
        public async Task<bool> SaveChangesAssync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        #endregion

        #region Clientes
        public async Task<Cliente[]> GetAllClienteAsync()
        {
            IQueryable<Cliente> query = _context.Clientes
                 .Include(c => c.Processos);


            query = query.OrderBy(c => c.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Cliente> GetAllClienteAsyncById(int ClienteId)
        {
            IQueryable<Cliente> query = _context.Clientes
                .Include(c => c.Processos);

            query = query.OrderBy(c => c.Nome)
                .Where(c => c.Id == ClienteId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<long> GetQtdClientesAsync()
        {
            IQueryable<Cliente> query = _context.Clientes;

            var clientes = await query.ToArrayAsync();

            return clientes.Count();
        }
        #endregion

        #region Processos
        public async Task<Processo[]> GetAllProcessoAsync()
        {
            IQueryable<Processo> query = _context.Processos;

            query = query.OrderBy(p => p.DataCriacao).ThenBy(p => p.Estado);

            return await query.ToArrayAsync();
        }

        public async Task<Processo> GetAllProcessoAsyncById(int ProcessoId)
        {
            IQueryable<Processo> query = _context.Processos;

            query = query.OrderBy(p => p.DataCriacao)
                .Where(p => p.Id == ProcessoId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<long> GetQtdProcessosAsync()
        {
            IQueryable<Processo> query = _context.Processos;

            var processos = await query.ToArrayAsync();

            return processos.Count();
        }
        public async Task<long> GetQtdProcessosAsync(bool ativo)
        {
            IQueryable<Processo> query = _context.Processos;

            query = query.Where(p => p.Ativo == ativo);

            var processos = await query.ToArrayAsync();
            
            return processos.Count();
        }
        public async Task<double> GetValorProcessosAsync(bool ativo)
        {
            IQueryable<Processo> query = _context.Processos;

            query = query.Where(p => p.Ativo == ativo);

            return await query.SumAsync(p => p.Valor);
        }
        public async Task<double> GetMediaValorProcessosAsyncByCliente(int clienteId, string estado)
        {
            IQueryable<Processo> query = _context.Processos;

            query = query.Where(p => p.ClienteId == clienteId && p.Estado.Contains(estado,StringComparison.OrdinalIgnoreCase));

            return await query.AverageAsync(p => p.Valor);
        }
        public async Task<Processo[]> GetAllProcessosAsyncByFiltro(ProcessoFiltros filtros)
        {
            IQueryable<Processo> query = _context.Processos;

            if (filtros.Id > 0) query.Where(p => p.Id == filtros.Id);

            if (!string.IsNullOrWhiteSpace(filtros.NumeroProcesso))
                query = query.OrderBy(p => p.DataCriacao).ThenBy(p => p.Estado)
               .Where(p => p.NumeroProcesso.Contains(filtros.NumeroProcesso, StringComparison.OrdinalIgnoreCase));

            if (filtros.Valor > 0) query = query.Where(p => p.Valor > filtros.Valor);

            if (filtros.DataCriacao != null && filtros.DataCriacao != DateTime.MinValue)
                query = query.Where(p => p.DataCriacao.Month == filtros.DataCriacao.Month 
                                    && p.DataCriacao.Year == filtros.DataCriacao.Year);


            return await query.ToArrayAsync();
        }              
        public async Task<Processo[]> GetAllProcessosAsyncByEstadoCliente(int clienteId)
        {
            IQueryable<Cliente> query = _context.Clientes
                .Include(p => p.Processos);

            if (clienteId > 0) query.Where(c => c.Id == clienteId);

            query = query.OrderBy(c => c.Id)
                .Where(c => c.Processos.Select(p => p.Estado).Contains(c.Estado));

            var cliente = await query.ToArrayAsync();

            return cliente.SelectMany(c => c.Processos).ToArray();
        }
        #endregion

    }
}
