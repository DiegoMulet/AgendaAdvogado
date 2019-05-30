using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgendaAdvogado.Domain
{
    public class Processo
    {
        public int Id { get; set; }

        [Required]
        public string NumeroProcesso { get; set; }

        [Required]
        public string Estado { get; set; }

        public DateTime DataCriacao { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public bool Ativo { get; set; }

        [Required]
        public int ClienteId { get; set; }
    }
}
