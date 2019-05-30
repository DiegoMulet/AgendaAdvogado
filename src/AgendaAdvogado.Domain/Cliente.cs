using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgendaAdvogado.Domain
{
   public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public long Cnpj { get; set; }

        [Required]
        public string Estado { get; set; }

        public List<Processo> Processos { get; set; }
    }
}
