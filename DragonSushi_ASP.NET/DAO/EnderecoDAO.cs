using DragonSushi_ASP.NET.DataBase;
using DragonSushi_ASP.NET.Models;
using DragonSushi_ASP.NET.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DragonSushi_ASP.NET.DAO
{
    public class EnderecoDAO
    {
        // CADASTRAR ENDEREÇO
        public void cadastrarEndereco(EnderecoViewModel vmEndereco)
        {
            Database db = new Database();

            string insertQuery = String.Format("call spCadastrarEndereco(@rua,@bairro,@cidade, @idEstado)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());

            command.Parameters.Add("@rua", MySqlDbType.VarChar).Value = vmEndereco.Rua.rua;
            command.Parameters.Add("@bairro", MySqlDbType.VarChar).Value = vmEndereco.Bairro.bairro;
            command.Parameters.Add("@cidade", MySqlDbType.VarChar).Value = vmEndereco.Cidade.cidade;
            command.Parameters.Add("@idEstado", MySqlDbType.VarChar).Value = vmEndereco.Estado.idEstado;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }
    }
}