using DragonSushi_ASP.NET.DAO;
using DragonSushi_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushi_ASP.NET.Controllers
{
    public class ClienteController : Controller
    {
        // CADASTRAR CLIENTE
        public ActionResult CadastrarCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarCliente(Pessoa pessoa)
        {
            ClienteDAO dao = new ClienteDAO();
            dao.cadastrarCliente(pessoa);

            return RedirectToAction("AreaFuncionario", "Funcionario", new { area = "" });
        }
    }
}