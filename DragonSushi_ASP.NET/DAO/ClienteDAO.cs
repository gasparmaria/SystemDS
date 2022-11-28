using DragonSushi_ASP.NET.DataBase;
using DragonSushi_ASP.NET.Models;
using DragonSushi_ASP.NET.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DragonSushi_ASP.NET.DAO
{
    public class ClienteDAO
    {
        // CADASTRAR CLIENTE
        public void cadastrarCliente(Pessoa pessoa)
        {
            Database db = new Database();

            string insertQuery = String.Format("spCadastrarCliente(@nomePessoa,@telefone,@cpf)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@nomePessoa", MySqlDbType.VarChar).Value = pessoa.nomePessoa;
            command.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = pessoa.telefone;
            command.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = pessoa.cpf;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }
    }
}