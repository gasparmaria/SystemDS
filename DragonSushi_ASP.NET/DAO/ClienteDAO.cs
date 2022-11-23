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
        public void cadastrarCliente(ClienteViewModel vmCliente)
        {
            Database db = new Database();

            string insertQuery = String.Format("spCadastrarCliente(@nomePessoa,@telefone,@cpf)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@nomePessoa", MySqlDbType.VarChar).Value = vmCliente.Pessoa.nomePessoa;
            command.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = vmCliente.Pessoa.telefone;
            command.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = vmCliente.Pessoa.cpf;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }

        // EDITAR CLIENTE
        public void EditarCliente(ClienteViewModel vmCliente)
        {
            Database db = new Database();
            string updateQuery = String.Format("CALL spEditarCliente(@idPessoa,@nomePessoa,@telefone,@cpf)");

            MySqlCommand command = new MySqlCommand(updateQuery, db.conectarDb());
            command.Parameters.Add("@idPessoa", MySqlDbType.Int32).Value = vmCliente.Pessoa.idPessoa;
            command.Parameters.Add("@nomePessoa", MySqlDbType.VarChar).Value = vmCliente.Pessoa.nomePessoa;
            command.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = vmCliente.Pessoa.telefone;
            command.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = vmCliente.Pessoa.cpf;

            try
            {
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.desconectarDb();

            }
        }
        // CONSULTAR CLIENTE PELO CPF
        public ClienteViewModel ConsultarCliente(string cpf)
        {
            Database db = new Database();
            {
                string strQuery = string.Format("CALL spConsultarCliente('{0}');", cpf);
                MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
                var leitor = exibir.ExecuteReader();
                return GerarListaCliente(leitor).FirstOrDefault();
            }
        }

        // GERADOR DE LISTA
        public List<ClienteViewModel> GerarListaCliente(MySqlDataReader leitor)
        {
            var cliente = new List<ClienteViewModel>();

            while (leitor.Read())
            {
                var lstCliente = new ClienteViewModel()
                {
                    Pessoa = new Pessoa()
                    {
                        nomePessoa = leitor["nomePessoa"].ToString(),
                        telefone = leitor["telefone"].ToString(),
                        cpf = leitor["cpf"].ToString(),
                    }
                };
                cliente.Add(lstCliente);
            }
            leitor.Close();
            return cliente;
        }
    }
}