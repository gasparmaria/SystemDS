using DragonSushi_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.ViewModel
{
    public class EnderecoViewModel
    {
        public Endereco Endereco { get; set; }

        public Bairro Bairro { get; set; }

        public Cidade Cidade { get; set; }

        public Estado Estado { get; set; }

        public Rua Rua { get; set; }
    }
}