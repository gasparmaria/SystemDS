using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "Informe o login")]
        [MaxLength(50, ErrorMessage = "O login deve conter no máximo 50 caracteres")]
        public string login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha")]
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string senha { get; set; }

        [Display(Name = "Nova senha")]
        [Required(ErrorMessage = "Informe a nova senha")]
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string novaSenha { get; set; }

        [Display(Name = "Confirmar senha")]
        [Required(ErrorMessage = "Confirme a senha")]
        [DataType(DataType.Password)]
        [CompareAttribute(nameof(novaSenha), ErrorMessage = "As senhas são diferentes")]
        public string confirmarSenha { get; set; }

        public int fkPessoa { get; set; }

        public int ocupacao { get; set; }
    }
}