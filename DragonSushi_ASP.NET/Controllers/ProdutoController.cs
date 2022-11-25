using DragonSushi_ASP.NET.DAO;
using DragonSushi_ASP.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushi_ASP.NET.Controllers
{
    public class ProdutoController : Controller
    {
        // LISTAR PRODUTOS DO CARDÁPIO
        [HttpGet]
        public ActionResult MostrarCardapio()
        {
            ProdutoDAO dao = new ProdutoDAO();
            var estoque = dao.ExibirCardapio();

            return View(estoque);
        }

        // GERENTE (GERENCIAMENTO DO ESTOQUE)

        [HttpGet]
        public ActionResult MostrarEstoque()
        {
            ProdutoDAO dao = new ProdutoDAO();
            var ProdutoViewModel = dao.spExibirEstoque();

            return View(ProdutoViewModel);
        }

        // HOME (MOSTRAR PRODUTOS POR CATEGORIA)

        public ActionResult ConsultarCategoria()
        {
            ProdutoDAO dao = new ProdutoDAO();
            var ProdutoViewModel = dao.ExibirCardapio();
            return View(ProdutoViewModel);
        }

        // CARDÁPIO (BUSCAR PRATO ESPECIFICO)

        public ActionResult ConsultarCardapio()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConsultarCardapio(string id)
        {
            ProdutoDAO dao = new ProdutoDAO();
            var PedidoViewModel = dao.ConsultarCardapio(id);
            return View(PedidoViewModel);
        }

        // BUSCAR PRATO ESPECIFICO

        public ActionResult spSelectProdutos()
        {
            return View();
        }

        [HttpGet]
        public ActionResult spSelectProdutos(int id)
        {
            ProdutoDAO dao = new ProdutoDAO();
            var ProdutoViewModel = dao.spSelectProdutos(id);
            return View(ProdutoViewModel);
        }




        // CADASTRAR PRODUTO (ADICIONAR PRODUTO)

        public ActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarProduto(ProdutoViewModel vmProduto)
        {
            ProdutoDAO dao = new ProdutoDAO();
            dao.CadastrarProduto(vmProduto);
            return View();
        }

        // EDITAR PRODUTO (ADICIONAR PRODUTO)

        public ActionResult EditarProduto(int id)
        {
            ProdutoDAO dao = new ProdutoDAO();
            var produto = dao.spSelectProdutos(id);

            return View(produto);
        }

        //public ActionResult EditarProduto(string nome)
        //{
        //    ProdutoDAO produto = new ProdutoDAO();
        //    var produtoselecionado = produto.ConsultarCardapio(nome);
        //    return View(produtoselecionado);
        //}

        [HttpPost]
        public ActionResult EditarProduto(ProdutoViewModel produto)
        {
            ProdutoDAO dao = new ProdutoDAO();
            dao.EditarProduto(produto);
            return View();
        }

    }
}