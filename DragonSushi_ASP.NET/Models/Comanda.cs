using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Comanda
    {
        [Display(Name = "Número da comanda")]
        [Required(ErrorMessage = "Informe o ID da Comanda")]
        public int idComanda { get; set; }

        public int numMesa { get; set; }

        public bool statusComanda { get; set; }
    }
}