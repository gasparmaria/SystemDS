using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Estado
    {
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Informe a UF de seu estado")]
        [MaxLength(2, ErrorMessage = "A UF deve conter no máximo 2 caracteres")]
        public string idEstado { get; set; }
    }
}