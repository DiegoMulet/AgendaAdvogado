using AgendaAdvogado.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaAdvogado.Repository
{
    public class AgendaAdvogadoContext : DbContext
    {
        public AgendaAdvogadoContext(DbContextOptions<AgendaAdvogadoContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Processo> Processos { get; set; }

    }
}
