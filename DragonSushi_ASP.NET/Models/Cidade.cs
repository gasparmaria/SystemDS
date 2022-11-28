using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Cidade
    {
        public int idCidade { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Informe sua cidade")]
        public string cidade { get; set; }
    }
}