using System;
using DragonSushi_ASP.NET.ViewModel;
using DragonSushi_ASP.NET.DAO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushi_ASP.NET.Controllers
{
    public class DeliveryController : Controller
    {
        // CARRINHO DE COMPRAS (MOSTRAR PEDIDOS)

        public ActionResult FinalizarPedido()
        {
            //DeliveryDAO dao = new DeliveryDAO();
            //var total = dao.deliveryTotal(preco);
            //return View(total);
            return View();
        }

        [HttpPost]
        public ActionResult FinalizarPedido(DeliveryViewModel vmDelivery)
        {
            DeliveryDAO dao = new DeliveryDAO();
            dao.CadastrarDelivery(vmDelivery);
            return RedirectToAction("PedidoFinalizado", "Pedido");
        }

    }
}