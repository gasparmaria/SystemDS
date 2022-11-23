using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class UnMedida
    {
        public int idUnMedida { get; set; }

        [Display(Name = "Unidade de medida")]
        public string unMedida { get; set; }
    }
}