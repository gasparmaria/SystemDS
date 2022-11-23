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
        [MaxLength(50, ErrorMessage = "A senha deve conter no máximo 50 caracteres")]
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string senha { get; set; }


        [Display(Name = "Confirme a senha")]
        [Required(ErrorMessage = "Confirme a senha")]
        [DataType(DataType.Password)]
        [CompareAttribute(nameof(senha), ErrorMessage = "As senhas são diferentes")]
        public string ConfirmaSenha { get; set; }
        public int fkPessoa { get; set; }
        public int ocupacao { get; set; }







        MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand command = new MySqlCommand();



        public string SelectLogin(string vLogin)
        {
            connection.Open();
            command.CommandText = "CALL spSelectLogin(@Login);";
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = vLogin;
            command.Connection = connection;
            string login = (string)command.ExecuteScalar(); // ExecuteScalar: RETORNAR APENAS 1 VALOR
            connection.Close();

            if (login == null)
                login = "";
            return login;
        }

        public Usuario SelectUsuario(string vLogin)
        {
            connection.Open();
            command.CommandText = "CALL spSelectUsuario(@Login);";
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = vLogin;
            command.Connection = connection;
            var readUsuario = command.ExecuteReader();
            var tempUsuario = new Usuario();

            if (readUsuario.Read())
            {
                tempUsuario.idUsuario = int.Parse(readUsuario["idUsuario"].ToString());
                tempUsuario.login = readUsuario["login"].ToString();
                tempUsuario.senha = readUsuario["senha"].ToString();
            };

            readUsuario.Close();
            connection.Close();

            return tempUsuario;
        }
    }
}