using DragonSushi_ASP.NET.DAO;
using DragonSushi_ASP.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushi_ASP.NET.Controllers
{
    public class ClienteController : Controller
    {

        // EDITAR PERFIL (ALTERAR PERFIL)



        // CADASTRAR CLIENTE (ADICIONAR CLIENTE)

        public ActionResult CadastrarCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarCliente(ClienteViewModel vmCliente)
        {
            ClienteDAO dao = new ClienteDAO();
            dao.cadastrarCliente(vmCliente);

            return RedirectToAction("AreaFuncionario", "Funcionario", new { area = "" });

        }


    }
}