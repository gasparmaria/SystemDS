using DragonSushi_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DragonSushi_ASP.NET.ViewModel
{
    public class PedidoViewModel
    {
        public Pedido Pedido { get; set; }

        public Produto Produto { get; set; }

        public Comanda Comanda { get; set; }
    }
}