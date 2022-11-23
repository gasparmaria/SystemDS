using DragonSushi_ASP.NET.DAO;
using DragonSushi_ASP.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushi_ASP.NET.Controllers
{
    public class FuncionarioController : Controller
    {
        // CADASTRO DE FUNCIONARIO

        public ActionResult CadastrarFuncionario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarFuncionario(FuncionarioViewModel vmFuncionario)
        {
            FuncionarioDAO dao = new FuncionarioDAO();
            dao.cadastrarFuncionario(vmFuncionario);
            return View();
        }


        public ActionResult AreaFuncionario()
        {
            return View();
        }

        public ActionResult AreaGerente()
        {
            return View();
        }

    }
}