using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Endereco
    {
        public int idEndereco { get; set; }
        public string numEndereco { get; set; }
        public string descrEndereco { get; set; }
        public int fkRua { get; set; }
        public int fkBairro { get; set; }
        public int fkCidade { get; set; }
        public int fkEstado { get; set; }
    }
}