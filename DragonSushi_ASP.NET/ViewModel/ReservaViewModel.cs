using DragonSushi_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.ViewModel
{
    public class ReservaViewModel
    {
        public Reserva Reserva { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}