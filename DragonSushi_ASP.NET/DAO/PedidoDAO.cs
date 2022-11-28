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
    public class PedidoDAO
    {
        // CADASTRAR PEDIDO
        public void CadastrarPedido(PedidoViewModel vmPedido)
        {
            Database db = new Database();

            int id = ConsultarComanda();



            string insertQuery = String.Format("CALL spCadastrarPedido(@qtdProd,@descrPedido,@idProd,@idComanda)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@qtdProd", MySqlDbType.Int32).Value = vmPedido.Pedido.qtdProd;
            command.Parameters.Add("@descrPedido", MySqlDbType.VarChar).Value = vmPedido.Pedido.descrPedido;
            command.Parameters.Add("@idProd", MySqlDbType.Int32).Value = vmPedido.Produto.idProd;
            command.Parameters.Add("@idComanda", MySqlDbType.Int32).Value = id;

            command.ExecuteNonQuery();
            db.desconectarDb();

            string amsdkasd = "jsdsjdksd";
        }

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

        // EXCLUIR PEDIDO
        public void ExcluirPedido(int id)
        {
            Database db = new Database();

            string deleteQuery = String.Format("CALL spExcluirPedido('{0}')", id);
            MySqlCommand command = new MySqlCommand(deleteQuery, db.conectarDb());
            command.ExecuteNonQuery();

            db.desconectarDb();
        }


        // CONSULTAR CARDÁPIO PELO NOME DO PRODUTO

        public List<PedidoViewModel> ConsultarPedido(string nomeProd)
        {
            Database db = new Database();
            {
                string strQuery = string.Format("CALL spConsultarCardapio('{0}');", nomeProd);
                MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
                var leitor = exibir.ExecuteReader();
                return Listapedido(leitor);
            }
        }

        // GERADOR DE LISTA DE PRODUTOS DO CARDÁPIO
        public List<PedidoViewModel> Listapedido(MySqlDataReader leitor)
        {
            var produto = new List<PedidoViewModel>();

            while (leitor.Read())
            {
                var lstProduto = new PedidoViewModel()
                {
                    Pedido = new Pedido()
                    {
                        subtotal = Convert.ToDouble(leitor["subtotal"])
                    },

                    Produto = new Produto()
                    {
                        idProd = Convert.ToInt32(leitor["idProd"]),
                        nomeProd = Convert.ToString(leitor["nomeProd"]),
                        fkCategoria = Convert.ToInt32(leitor["fkCategoria"]),
                        preco = Convert.ToDecimal(leitor["preco"]),
                        descrProd = Convert.ToString(leitor["descrProd"]),
                        imgProd = Convert.ToString(leitor["imgProd"])
                    },


                    Comanda = new Comanda()
                    {
                        idComanda = Convert.ToInt32(leitor["idComanda"]),
                        numMesa = Convert.ToInt32(leitor["numMesa"]),
                        statusComanda = Convert.ToBoolean(leitor["statusComanda"])
                    }
                };
                produto.Add(lstProduto);
            }

            leitor.Close();
            return produto;
        }

        public List<PedidoViewModel> Carrinho()
        {
            int id = ConsultarComanda();

            Database db = new Database();
            {
                string strQuery = string.Format("CALL spPedidosComanda('{0}');", id);
                MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
                var leitor = exibir.ExecuteReader();
                return listaCarrinho(leitor);
            }
        }

        public List<PedidoViewModel> listaCarrinho(MySqlDataReader leitor)
        {
            var produto = new List<PedidoViewModel>();

            while (leitor.Read())
            {
                var lstProduto = new PedidoViewModel()
                {
                    Pedido = new Pedido()
                    {
                        qtdProd = Convert.ToInt32(leitor["qtdProd"]),
                        descrPedido = Convert.ToString(leitor["descrPedido"])
                    },

                    Produto = new Produto()
                    {
                        nomeProd = Convert.ToString(leitor["nomeProd"]),
                        preco = Convert.ToDecimal(leitor["preco"]),                      
                        imgProd = Convert.ToString(leitor["imgProd"])
                    },                
                };
                produto.Add(lstProduto);
            }

            leitor.Close();
            return produto;
        }

        // LANÇAR PEDIDO
        public void LançarPedido(PedidoViewModel vmPedido)
        {
            Database db = new Database();

            string insertQuery = String.Format("CALL spCadastrarPedido(@qtdProd,@descrPedido,@idProd,@idComanda)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@qtdProd", MySqlDbType.Int32).Value = vmPedido.Pedido.qtdProd;
            command.Parameters.Add("@descrPedido", MySqlDbType.VarChar).Value = vmPedido.Pedido.descrPedido;
            command.Parameters.Add("@idProd", MySqlDbType.Int32).Value = vmPedido.Produto.idProd;
            command.Parameters.Add("@idComanda", MySqlDbType.Int32).Value = vmPedido.Comanda.idComanda;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }
        // CONSULTAR PEDIDO PELA ID 
    }
}