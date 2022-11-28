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
    public class ComandaDAO
    {
        // CADASTRAR COMANDA
        public void cadastrarComanda(Comanda comanda)
        {
            Database db = new Database();

            string insertQuery = String.Format("spCadastrarComanda(@numMesa)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@numMesa", MySqlDbType.Int16).Value = comanda.numMesa;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }

        // LISTAR COMANDAS
        public List<Comanda> ExibirComanda()
        {
            Database db = new Database();

            MySqlCommand exibir = new MySqlCommand("call spExibirComandas()", db.conectarDb());
            var leitor = exibir.ExecuteReader();

            return listaComanda(leitor);
        }

        // EXIBIR DETALHES DA COMANDA
        public List<ComandaViewModel> DetalhesComanda(int num)
        {
            Database db = new Database();
            {
                string strQuery = string.Format("CALL spPedidosComanda('{0}');", num);
                MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
                var leitor = exibir.ExecuteReader();
                return listaPedido(leitor);
            }
        }

        // GERADOR DE LISTA DA COMANDA
        public List<Comanda> listaComanda(MySqlDataReader leitor)
        {
            var comanda = new List<Comanda>();

            while (leitor.Read())
            {
                var lstComanda = new Comanda()
                {
                    idComanda = Convert.ToInt32(leitor["idComanda"]),
                    numMesa = Convert.ToInt32(leitor["numMesa"]),
                    statusComanda = Convert.ToBoolean(leitor["statusComanda"])
                };
                comanda.Add(lstComanda);
            }

            leitor.Close();
            return comanda;
        }

        // GERADOR DE LISTA DOS PEDIDOS
        public List<ComandaViewModel> listaPedido(MySqlDataReader leitor)
        {
            var comanda = new List<ComandaViewModel>();

            while (leitor.Read())
            {           
                var lstComanda = new ComandaViewModel()
                {
                    //subtotal = total,
                    Comanda = new Comanda()
                    {
                        idComanda = Convert.ToInt32(leitor["fkComanda"]),
                        numMesa = Convert.ToInt32(leitor["numMesa"])

                    },
                    Pedido = new Pedido()
                    {
                        qtdProd = Convert.ToInt32(leitor["qtdProd"])
                    },
                    Produto = new Produto()
                    {
                        nomeProd = Convert.ToString(leitor["nomeProd"])

                    }
                };
                comanda.Add(lstComanda);
            }

            leitor.Close();
            return comanda;
        }
    }
}