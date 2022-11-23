using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Categoria
    {
        public int idCategoria { get; set; }

        [Display(Name = "Categoria")]
        public string categoria { get; set; }
    }
}