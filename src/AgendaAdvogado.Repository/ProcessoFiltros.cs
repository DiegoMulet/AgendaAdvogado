using System;

namespace AgendaAdvogado.Repository
{
    public class ProcessoFiltros
    {
        public int Id { get; set; }
        public string NumeroProcesso { get; set; }
        public string Estado { get; set; }
        public DateTime DataCriacao { get; set; }
        public double Valor { get; set; }
        public bool Ativo { get; set; }
        public int ClienteId { get; set; }
    }
}