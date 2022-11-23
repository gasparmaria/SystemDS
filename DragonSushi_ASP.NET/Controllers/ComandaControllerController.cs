using DragonSushi_ASP.NET.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushi_ASP.NET.Controllers
{
    public class ComandaController : Controller
    {
        // EXIBIR COMANDAS
        [HttpGet]
        public ActionResult ExibirComanda()
        {
            ComandaDAO dao = new ComandaDAO();
            var comandas = dao.ExibirComanda();
            return View(comandas);
        }

        // DETALHES DA COMANDA
        [HttpGet]
        public ActionResult DetalhesComanda(int id)
        {
            ComandaDAO dao = new ComandaDAO();
            var comanda = dao.DetalhesComanda(id);
            return View(comanda);
        }
    }
}