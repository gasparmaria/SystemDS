using DragonSushi_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.ViewModel
{
    public class ProdutoViewModel
    {
        public Produto Produto { get; set; }

        public Categoria Categoria { get; set; }

        public UnMedida UnMedida { get; set; }

    }
}