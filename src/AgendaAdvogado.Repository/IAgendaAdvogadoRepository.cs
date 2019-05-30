using AgendaAdvogado.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgendaAdvogado.Repository
{
    public interface IAgendaAdvogadoRepository
    {
        #region All
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(List<T> entity) where T : class;
        Task<bool> SaveChangesAssync();
        #endregion

        #region Clientes      
        Task<Cliente[]> GetAllClienteAsync();
        Task<Cliente> GetAllClienteAsyncById(int ClienteId);
        Task<long> GetQtdClientesAsync();
        #endregion

        #region Processos
        Task<Processo[]> GetAllProcessoAsync();
        Task<Processo> GetAllProcessoAsyncById(int ProcessoId);
        Task<long> GetQtdProcessosAsync();
        Task<long> GetQtdProcessosAsync(bool ativo);
        Task<double> GetValorProcessosAsync(bool ativo);
        Task<Processo[]> GetAllProcessosAsyncByFiltro(ProcessoFiltros filtros);
        Task<Processo[]> GetAllProcessosAsyncByEstadoCliente(int clientId);
        Task<double> GetMediaValorProcessosAsyncByCliente(int clienteId, string estado);
        #endregion

    }
}
