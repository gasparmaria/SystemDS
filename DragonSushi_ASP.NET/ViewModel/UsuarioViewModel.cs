using DragonSushi_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.ViewModel
{
    public class UsuarioViewModel
    {
        public Usuario Usuario { get; set; }

        public Pessoa Pessoa { get; set; }

        public string urlRetorno { get; set; }
    }
}