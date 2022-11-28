using DragonSushi_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.ViewModel
{
    public class ComandaViewModel
    {
        public Comanda Comanda;
        public Produto Produto;
        public Pedido Pedido;
        public double subtotal;
        public double total;
    }
}