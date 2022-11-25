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
    public class ProdutoDAO
    {
        // CADASTRAR PRODUTO
        public void CadastrarProduto(ProdutoViewModel vmProduto)
        {
            Database db = new Database();

            string insertQuery = String.Format("call spCadastrarProduto(@nomeProd,@imgProd,@descrProd,@preco,@estoque, @ingrediente, @categoria, @qntdProd, @unMedida)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@nomeProd", MySqlDbType.VarChar).Value = vmProduto.Produto.nomeProd;
            command.Parameters.Add("@imgProd", MySqlDbType.VarChar).Value = vmProduto.Produto.imgProd;
            command.Parameters.Add("@descrProd", MySqlDbType.VarChar).Value = vmProduto.Produto.descrProd;
            command.Parameters.Add("@preco", MySqlDbType.Decimal).Value = vmProduto.Produto.preco;
            command.Parameters.Add("@estoque", MySqlDbType.Byte).Value = vmProduto.Produto.estoque;
            command.Parameters.Add("@ingrediente", MySqlDbType.Byte).Value = vmProduto.Produto.ingrediente;
            command.Parameters.Add("@categoria", MySqlDbType.VarChar).Value = vmProduto.Categoria.categoria;
            command.Parameters.Add("@qntdProd", MySqlDbType.Decimal).Value = vmProduto.Produto.qtdProd;
            command.Parameters.Add("@unMedida", MySqlDbType.VarChar).Value = vmProduto.UnMedida.unMedida;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }
        // EDITAR PRODUTO
        public void EditarProduto(ProdutoViewModel vmProduto)
        {
            Database db = new Database();

            if (vmProduto.Produto.ingrediente == false)
            {
                string insertQuery = String.Format("CALL spEditarProduto(@idProd,@nomeProd,@imgProd,@descrProd,@preco,@estoque, @ingrediente, @fkCategoria, @qntdProd, @unMedida)");
                MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
                command.Parameters.Add("@idProd", MySqlDbType.Int32).Value = vmProduto.Produto.idProd;
                command.Parameters.Add("@nomeProd", MySqlDbType.VarChar).Value = vmProduto.Produto.nomeProd;
                command.Parameters.Add("@imgProd", MySqlDbType.VarChar).Value = vmProduto.Produto.imgProd;
                command.Parameters.Add("@descrProd", MySqlDbType.VarChar).Value = vmProduto.Produto.descrProd;
                command.Parameters.Add("@preco", MySqlDbType.Decimal).Value = vmProduto.Produto.preco;
                command.Parameters.Add("@estoque", MySqlDbType.Byte).Value = vmProduto.Produto.estoque;
                command.Parameters.Add("@ingrediente", MySqlDbType.Byte).Value = vmProduto.Produto.ingrediente;
                command.Parameters.Add("@fkCategoria", MySqlDbType.Int32).Value = vmProduto.Produto.fkCategoria;
                command.Parameters.Add("@qntdProd", MySqlDbType.Decimal).Value = null;
                command.Parameters.Add("@unMedida", MySqlDbType.VarChar).Value = 0;

                command.ExecuteNonQuery();
                db.desconectarDb();
            }
            if (vmProduto.Produto.ingrediente == true)
            {
                string insertQuery = String.Format("CALL spEditarProduto(@idProd,@nomeProd,@imgProd,@descrProd,@preco,@estoque, @ingrediente, @fkCategoria, @qntdProd, @unMedida)");
                MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
                command.Parameters.Add("@idProd", MySqlDbType.Int32).Value = vmProduto.Produto.idProd;
                command.Parameters.Add("@nomeProd", MySqlDbType.VarChar).Value = vmProduto.Produto.nomeProd;
                command.Parameters.Add("@imgProd", MySqlDbType.VarChar).Value = null;
                command.Parameters.Add("@descrProd", MySqlDbType.VarChar).Value = null;
                command.Parameters.Add("@preco", MySqlDbType.Decimal).Value = null;
                command.Parameters.Add("@estoque", MySqlDbType.Byte).Value = vmProduto.Produto.estoque;
                command.Parameters.Add("@ingrediente", MySqlDbType.Byte).Value = vmProduto.Produto.ingrediente;
                command.Parameters.Add("@fkCategoria", MySqlDbType.Int32).Value = null;
                command.Parameters.Add("@qntdProd", MySqlDbType.Decimal).Value = vmProduto.Produto.qtdProd;
                command.Parameters.Add("@unMedida", MySqlDbType.VarChar).Value = vmProduto.UnMedida.unMedida;

                command.ExecuteNonQuery();
                db.desconectarDb();
            }
        }

        // LISTAR PRODUTOS DO CARDÁPIO
        public List<ProdutoViewModel> ExibirCardapio()
        {
            Database db = new Database();
            MySqlCommand exibir = new MySqlCommand("call spExibirCardapio()", db.conectarDb());
            var leitor = exibir.ExecuteReader();
            return listaCardapio(leitor);
        }

        // LISTAR CARDÁPIO ATRAVÉS DA CATEGORIA
        public List<ProdutoViewModel> ConsultarCategoria(int fkCategoria)
        {
            Database db = new Database();

            string strQuery = string.Format("CALL spExibirCategoria('{0}');", fkCategoria);
            MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
            var leitor = exibir.ExecuteReader();
            return listaCardapio(leitor);

        }

        // CONSULTAR CARDÁPIO PELO NOME DO PRODUTO
        public List<ProdutoViewModel> ConsultarCardapio(string nomeProd)
        {
            Database db = new Database();
            {
                string strQuery = string.Format("CALL spConsultarCardapio('{0}');", nomeProd);
                MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
                var leitor = exibir.ExecuteReader();
                return listaCardapio(leitor);
            }
        }

        // GERADOR DE LISTA DE PRODUTOS DO CARDÁPIO
        public List<ProdutoViewModel> listaCardapio(MySqlDataReader leitor)
        {
            var produto = new List<ProdutoViewModel>();

            while (leitor.Read())
            {
                var lstProduto = new ProdutoViewModel()
                {
                    Produto = new Produto()
                    {
                        idProd = Convert.ToInt32(leitor["idProd"]),
                        nomeProd = Convert.ToString(leitor["nomeProd"]),
                        //qtdProd = Convert.ToDecimal(leitor["qntdProd"]),
                        //fkUnMedida = Convert.ToInt32(leitor["fkUnMedida"]),
                        fkCategoria = Convert.ToInt32(leitor["fkCategoria"]),
                        preco = Convert.ToDecimal(leitor["preco"]),
                        descrProd = Convert.ToString(leitor["descrProd"]),
                        imgProd = Convert.ToString(leitor["imgProd"])
                    },

                    Categoria = new Categoria()
                    {
                        idCategoria = Convert.ToInt32(leitor["fkCategoria"])
                    }

                };
                produto.Add(lstProduto);
            }

            leitor.Close();
            return produto;
        }

        // LISTAR PRODUTOS DO ESTOQUE
        public List<ProdutoViewModel> spExibirEstoque()
        {
            Database db = new Database();

            MySqlCommand exibir = new MySqlCommand("call spExibirEstoque()", db.conectarDb());
            var leitor = exibir.ExecuteReader();

            return listaEstoque(leitor);
        }

        // CONSULTAR ESTOQUE PELO NOME DO PRODUTO
        public ProdutoViewModel ConsultarEstoque(string nomeProd)
        {
            Database db = new Database();
            {
                string strQuery = string.Format("CALL spConsultarEstoque('{0}');", nomeProd);
                MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
                var leitor = exibir.ExecuteReader();
                return listaEstoque(leitor).FirstOrDefault();
            }
        }

        // GERADOR DE LISTA DE PRODUTOS DO ESTOQUE
        public List<ProdutoViewModel> listaEstoque(MySqlDataReader leitor)
        {
            var produto = new List<ProdutoViewModel>();

            while (leitor.Read())
            {
                var lstProduto = new ProdutoViewModel()
                {
                    Produto = new Produto()
                    {
                        idProd = Convert.ToInt32(leitor["idProd"]),
                        nomeProd = Convert.ToString(leitor["nomeProd"]),
                        qtdProd = Convert.ToInt32(leitor["qntdProd"]),
                        fkUnMedida = Convert.ToInt32(leitor["fkUnMedida"]),
                        imgProd = Convert.ToString(leitor["imgprod"])
                    },
                    UnMedida = new UnMedida()
                    {
                        unMedida = Convert.ToString(leitor["unMedida"])
                    }

                };
                produto.Add(lstProduto);
            }

            leitor.Close();
            return produto;
        }



        //SELEC PRODUTO ID

        public PedidoViewModel spSelectProdutos(int idProd)
        {
            Database db = new Database();
            string selectQuery = String.Format("CALL spSelectProdutos('{0}')", idProd);
            MySqlCommand command = new MySqlCommand(selectQuery, db.conectarDb());
            var dados = command.ExecuteReader();

            return listaprodutoespecifico(dados).FirstOrDefault();
        }


        // GERADOR DE LISTA DE PRODUTOS DO ESTOQUE
        public List<PedidoViewModel> listaprodutoespecifico(MySqlDataReader leitor)
        {
            var produto = new List<PedidoViewModel>();

            while (leitor.Read())
            {
                var lstProduto = new PedidoViewModel()
                {
                    Produto = new Produto()
                    {
                        idProd = Convert.ToInt32(leitor["idProd"]),
                        nomeProd = Convert.ToString(leitor["nomeProd"]),
                        imgProd = Convert.ToString(leitor["imgprod"]),
                        descrProd = Convert.ToString(leitor["descrProd"]),
                        preco = Convert.ToDecimal(leitor["preco"])
                    }
                };
                produto.Add(lstProduto);
            }

            leitor.Close();
            return produto;
        }

        // SUBTOTAL 



    }
}