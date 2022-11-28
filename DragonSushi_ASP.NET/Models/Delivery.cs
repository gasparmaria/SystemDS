using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Delivery
    {
        public int idDelivery { get; set; }
        
        public DateTime dataDelivery { get; set; }

        public int fkPessoa { get; set; }

        public int fkEndereco { get; set; }

        public int fkComanda { get; set; }

        public int fkPag { get; set; }

        [Display(Name = "Descrição do endereço")]
        public string descrEndereco { get; set; }

        [Display(Name = "Número do endereço")]
        [Required(ErrorMessage = "Informe o número do endereço")]
        public string numEndereco { get; set; }
        
        public int itens { get; set; }
    }
}