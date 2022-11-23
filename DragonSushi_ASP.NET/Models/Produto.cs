using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Produto
    {

        public int idProd { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome")]
        [MaxLength(100, ErrorMessage = "O nome deve conter no máximo 100 caracteres")]
        public string nomeProd { get; set; }
        public string imgProd { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição")]
        [MaxLength(200, ErrorMessage = "A descrição deve conter no máximo 200 caracteres")]
        public string descrProd { get; set; }

        [Display(Name = "Preço")]
        public decimal preco { get; set; }

        [Display(Name = "Estoque")]
        public bool estoque { get; set; }

        [Display(Name = "Ingrediente")]
        public bool ingrediente { get; set; }
        public int fkCategoria { get; set; }

        [Display(Name = "Quantidade")]
        public int qtdProd { get; set; }

        public int fkUnMedida { get; set; }
    }
}