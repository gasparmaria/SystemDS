using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Reserva
    {
        public int idReserva { get; set; }
        [Display(Name = "Data da reserva")]
        public DateTime dataReserva { get; set; }
        [Display(Name = "Hora")]
        public TimeSpan hora { get; set; }
        [Display(Name = "Número de pessoas")]
        public int numPessoas { get; set; }
        public int fkPessoa { get; set; }
    }
}