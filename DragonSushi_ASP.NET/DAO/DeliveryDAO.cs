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
    public class DeliveryDAO
    {
        // CADASTRAR DELIVERY
        public void cadastrarDelivery(DeliveryViewModel vmDelivery)
        {
            Database db = new Database();

            string insertQuery = String.Format("call spCadastrarDelivery(@idPessoa,@idEndereco,@idComanda,@total,@formaPag)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@idPessoa", MySqlDbType.Int32).Value = vmDelivery.Pessoa.idPessoa;
            command.Parameters.Add("@idEndereco", MySqlDbType.Int32).Value = vmDelivery.Endereco.idEndereco;
            command.Parameters.Add("@idComanda", MySqlDbType.Int32).Value = vmDelivery.Comanda.idComanda;
            command.Parameters.Add("@total", MySqlDbType.Decimal).Value = vmDelivery.Pagamento.total;
            command.Parameters.Add("@formaPag", MySqlDbType.VarChar).Value = vmDelivery.FormaPg.FormaPag;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }

        // EXIBIR HISTÓRICO DE PEDIDOS DO CLIENTE
        public List<DeliveryViewModel> spHistoricoPedido(int fkUsuario)
        {
            Database db = new Database();
            {
                string strQuery = string.Format("CALL spHistoricoPedido({0});", fkUsuario);
                MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
                var leitor = exibir.ExecuteReader();
                return listaPedido(leitor);
            }

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
                        idDelivery = Convert.ToInt32(leitor["idDelivery"]),
                        dataDelivery = Convert.ToDateTime(leitor["dataDelivery"]),
                        fkPessoa = Convert.ToInt32(leitor["fkPessoa"]),
                        fkEndereco = Convert.ToInt32(leitor["fkEndereco"]),
                        fkComanda = Convert.ToInt32(leitor["fkComanda"]),
                        fkPag = Convert.ToInt32(leitor["fkPag"])
                    }
                };
                produto.Add(lstProduto);
            }

            leitor.Close();
            return produto;

        }
    }
}