using DragonSushi_ASP.NET.DataBase;
using DragonSushi_ASP.NET.Models;
using DragonSushi_ASP.NET.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Web;

namespace DragonSushi_ASP.NET.DAO
{
    public class DeliveryDAO
    {
        // CADASTRAR DELIVERY
        public void CadastrarDelivery(DeliveryViewModel vmDelivery)
        {
            string fileName = "C:/Users/Naja Informatica/Downloads/SystemDS/DragonSushi_ASP.NET/DataBase/usuariologado.json";
            string jsonString = System.IO.File.ReadAllText(fileName);
            UsuarioViewModel vmusuario = JsonSerializer.Deserialize<UsuarioViewModel>(jsonString);

            int idPessoa = vmusuario.Pessoa.idPessoa;
            int idComanda = ConsultarComanda();

            Database db = new Database();

            string insertQuery = String.Format("call spCadastrarDelivery(@idPessoa,@rua,@bairro,@cidade,@estado,@idComanda,@total,@formaPag,@numEndereco,@descrEndereco)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@idPessoa", MySqlDbType.Int32).Value = idPessoa;
            command.Parameters.Add("@rua", MySqlDbType.VarChar).Value = vmDelivery.Endereco.rua;
            command.Parameters.Add("@bairro", MySqlDbType.VarChar).Value = vmDelivery.Bairro.bairro;
            command.Parameters.Add("@cidade", MySqlDbType.VarChar).Value = vmDelivery.Cidade.cidade;
            command.Parameters.Add("@estado", MySqlDbType.VarChar).Value = vmDelivery.Estado.idEstado;
            command.Parameters.Add("@idComanda", MySqlDbType.Int32).Value = idComanda;
            command.Parameters.Add("@total", MySqlDbType.Decimal).Value = 0;
            command.Parameters.Add("@formaPag", MySqlDbType.VarChar).Value = vmDelivery.FormaPg.formaPag;
            command.Parameters.Add("@numEndereco", MySqlDbType.VarChar).Value = vmDelivery.Delivery.numEndereco;
            command.Parameters.Add("@descrEndereco", MySqlDbType.VarChar).Value = vmDelivery.Delivery.descrEndereco;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }

        // SELECIONAR ID DA COMANDA
        public int ConsultarComanda()
        {
            Database db = new Database();

            string strQuery = string.Format("CALL spComandaDeliveryS();");
            MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
            var leitor = exibir.ExecuteReader();
            leitor.Read();

            int id = Convert.ToInt32(leitor["idComanda"]);

            return id;
        }

        // EXIBIR HISTÓRICO DE PEDIDOS DO CLIENTE
        public List<DeliveryViewModel> HistoricoPedido(int idPessoa)
        {
            Database db = new Database();
            string strQuery = string.Format("CALL spHistoricoPedido({0});", idPessoa);
            MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
            var leitor = exibir.ExecuteReader();
            return listaPedido(leitor);

        }

        public DeliveryViewModel deliveryTotal(decimal deliverytotal)
        {
            DeliveryViewModel deliveryViewModel = new DeliveryViewModel()
            {
                Pagamento = new Pagamento
                {
                    total = deliverytotal
                }
            };
            return deliveryViewModel;
        }

        // GERADOR DE LISTA DE PEDIDOS
        public List<DeliveryViewModel> listaPedido(MySqlDataReader leitor)
        {
            var produto = new List<DeliveryViewModel>();

            while (leitor.Read())
            {
                var lstProduto = new DeliveryViewModel()
                {
                    Delivery = new Delivery()
                    {
                        dataDelivery = Convert.ToDateTime(leitor["dataDelivery"]),
                        itens = Convert.ToInt32(leitor["COUNT(p.idPedido)"])
                    },
                    Pagamento = new Pagamento()
                    {
                        total = Convert.ToDecimal(leitor["total"])
                    }
                };
                produto.Add(lstProduto);
            }

            leitor.Close();
            return produto;
        }
    }
}