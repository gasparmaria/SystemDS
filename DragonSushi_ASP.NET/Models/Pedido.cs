using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Pedido
    {
        public int idPedido { get; set; }
        [Display(Name = "Quantidade do produto")]
        [Required(ErrorMessage = "Informe o a quantidade do produto")]
        public int qtdProd { get; set; }
        [Display(Name = "Descrição do pedido")]
        public string descrPedido { get; set; }
        public int fkProd { get; set; }
        public int fkComanda { get; set; }
        public double subtotal { get; set; }
    }
}