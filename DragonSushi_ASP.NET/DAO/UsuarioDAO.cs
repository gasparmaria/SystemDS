using DragonSushi_ASP.NET.DataBase;
using DragonSushi_ASP.NET.Models;
using DragonSushi_ASP.NET.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushi_ASP.NET.DAO
{
    public class UsuarioDAO
    {
        Database db = new Database();

        // CADASTRAR USUÁRIO
        public void cadastrarUsuario(UsuarioViewModel vmUsuario)
        {
            Database db = new Database();

            string insertQuery = String.Format("call spCadastrarUsuario(@nomePessoa,@telefone,@cpf,@login,@senha)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@nomePessoa", MySqlDbType.VarChar).Value = vmUsuario.Pessoa.nomePessoa;
            command.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = vmUsuario.Pessoa.telefone;
            command.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = vmUsuario.Pessoa.cpf;
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = vmUsuario.Usuario.login;
            command.Parameters.Add("@senha", MySqlDbType.VarChar).Value = vmUsuario.Usuario.senha;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }

        // CONSULTAR CARDÁPIO PELO NOME DO PRODUTO
        public UsuarioViewModel ConsultarUsuario(string login)
        {
            Database db = new Database();
            {
                string strQuery = string.Format("CALL spConsultarUsuario('{0}');", login);
                MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
                var leitor = exibir.ExecuteReader();
                return listaUsuario(leitor).FirstOrDefault();
            }
        }

        // GERADOR DE LISTA DE USUARIO
        public List<UsuarioViewModel> listaUsuario(MySqlDataReader leitor)
        {
            var usuario = new List<UsuarioViewModel>();

            while (leitor.Read())
            {
                var lstUsuario = new UsuarioViewModel()
                {
                    Usuario = new Usuario()
                    {
                        idUsuario = Convert.ToInt32(leitor["idUsuario"]),
                        login = Convert.ToString(leitor["login"]),
                        senha = Convert.ToString(leitor["senha"])
                    },
                    Pessoa = new Pessoa()
                    {
                        idPessoa = Convert.ToInt32(leitor["idUsuario"]),
                        nomePessoa = Convert.ToString(leitor["nomePessoa"]),
                        telefone = Convert.ToString(leitor["telefone"]),
                        cpf = Convert.ToString(leitor["cpf"]),
                        ocupacao = Convert.ToInt32(leitor["ocupacao"])
                    }
                };
                usuario.Add(lstUsuario);
            }

            leitor.Close();
            return usuario;
        }

        // EDITAR PERFIL
        public void EditarPerfil(UsuarioViewModel vmUsuario)
        {
            Database db = new Database();

            string insertQuery = String.Format("CALL spEditarUsuario(@idUsuario,@idPessoa,@nomePessoa,@telefone,@login,@senha)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@idUsuario", MySqlDbType.Int32).Value = vmUsuario.Usuario.idUsuario;
            command.Parameters.Add("@idPessoa", MySqlDbType.Int32).Value = vmUsuario.Pessoa.idPessoa;
            command.Parameters.Add("@nomePessoa", MySqlDbType.String).Value = vmUsuario.Pessoa.nomePessoa;
            command.Parameters.Add("@telefone", MySqlDbType.String).Value = vmUsuario.Pessoa.telefone;
            command.Parameters.Add("@login", MySqlDbType.String).Value = vmUsuario.Usuario.login;
            command.Parameters.Add("@senha", MySqlDbType.String).Value = vmUsuario.Usuario.novaSenha;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }
    }
}