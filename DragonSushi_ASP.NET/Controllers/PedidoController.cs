using DragonSushi_ASP.NET.DAO;
using DragonSushi_ASP.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;

namespace DragonSushi_ASP.NET.Controllers
{
    public class PedidoController : Controller
    {
        // CADASTRAR PEDIDO

        public ActionResult CadastrarPedido()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarPedido(PedidoViewModel pedido)
        {
            PedidoDAO dao = new PedidoDAO();
            dao.CadastrarPedido(pedido);
            return View();
        }

        // EXCLUIR PEDIDO

        [HttpDelete]
        public ActionResult ExcluirPedido(int id)
        {
            PedidoDAO dao = new PedidoDAO();
            dao.ExcluirPedido(id);
            return View();
        }

        // HISTORIOCO DE PEDIDOS

        [HttpGet]
        public ActionResult HistoricoPedido()
        {
            string fileName = "usuariologado.json";
            string jsonString = System.IO.File.ReadAllText(fileName);
            UsuarioViewModel vmusuario = JsonSerializer.Deserialize<UsuarioViewModel>(jsonString);

            DeliveryDAO dao = new DeliveryDAO();
            var estoque = dao.spHistoricoPedido(vmusuario.Usuario.idUsuario);

            return View(estoque);
        }

        // PEDIDO FINALIZADO
        public ActionResult PedidoFinalizado()
        {
            return View();
        }
    }
}