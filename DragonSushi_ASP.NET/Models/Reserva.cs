using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Reserva
    {
        public int idReserva { get; set; }
        public DateTime dataReserva { get; set; }
        public TimeSpan hora { get; set; }
        public int numPessoas { get; set; }
        public int fkPessoa { get; set; }
    }
}