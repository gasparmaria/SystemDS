using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Pessoa
    {
        public int idPessoa { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe seu nome")]
        [MaxLength(150, ErrorMessage = "O nome deve conter no máximo 150 caracteres")]
        public string nomePessoa { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Informe seu telefone")]
        [MaxLength(15, ErrorMessage = "O nome deve conter no máximo 15 caracteres")]
        public string telefone { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Informe seu CPF")]
        [MaxLength(14, ErrorMessage = "O nome deve conter no máximo 14 caracteres")]
        public string cpf { get; set; }

        [Display(Name = "Ocupação")]
        [Required(ErrorMessage = "Informe sua ocupação")]
        public int ocupacao { get; set; }
    }
}