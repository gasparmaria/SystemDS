using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Bairro
    {
        public int idBairro { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Informe o seu bairro")]
        public string bairro { get; set; }
    }
}