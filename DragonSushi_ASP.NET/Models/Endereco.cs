using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Endereco
    {
        public int idEndereco { get; set; }

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "Informe a rua")]
        public string rua { get; set; }

        public int fkBairro { get; set; }

        public int fkCidade { get; set; }

        public int fkEstado { get; set; }
    }
}