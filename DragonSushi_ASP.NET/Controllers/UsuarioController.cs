using DragonSushi_ASP.NET.DAO;
using DragonSushi_ASP.NET.Models;
using DragonSushi_ASP.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushi_ASP.NET.Controllers
{
    public class UsuarioController : Controller
    {
        // CADASTRAR USUARIO (ADICIONAR USUARIO)

        public ActionResult CadastrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(UsuarioViewModel usuario)
        {
            UsuarioDAO dao = new UsuarioDAO();
            dao.cadastrarUsuario(usuario);
            return View();
        }

        public ActionResult SelectLogin(string vLogin)
        {
            bool LoginExists;
            string login = new Usuario().SelectLogin(vLogin);

            if (login.Length == 0)
                LoginExists = false;
            else
                LoginExists = true;

            return Json(!LoginExists, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }

        //public ActionResult Login(string ReturnUrl)
        //{
        //    var viewmodel = new LoginViewModel
        //    {
        //        urlRetorno = ReturnUrl
        //    };
        //    return View(viewmodel);
        //}


        //[HttpPost]
        //public ActionResult Login(LoginViewModel viewmodel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(viewmodel);
        //    }

        //    DataBase db = new DataBase();
        //    db.conectarDb();

        //    Usuario usuario = new Usuario();
        //    usuario = usuario.SelectUsuario(viewmodel.Login);

        //    int ocupacao = usuario.ocupacao;

        //    var identity = new ClaimsIdentity(new[]
        //    {
        //        new Claim(ClaimTypes.Name, usuario.login),
        //        new Claim("Login", usuario.login)
        //    }, "AppAplicationCookie");

        //    Request.GetOwinContext().Authentication.SignIn(identity);


        //    if (!String.IsNullOrWhiteSpace(viewmodel.urlRetorno) || Url.IsLocalUrl(viewmodel.urlRetorno))
        //        return Redirect(viewmodel.urlRetorno);
        //    else
        //    {
        //        switch (ocupacao)
        //        {
        //            case 1:
        //                return RedirectToAction("AreaGerente", "Funcionario");

        //            case 2:
        //                return RedirectToAction("AreaFuncionario", "Funcionario");

        //            case 3:
        //                return RedirectToAction("ConsultarCategoria", "Produto");

        //        }
        //    }      



        //}

        //public ActionResult Logout()
        //{
        //    Request.GetOwinContext().Authentication.SignOut("AppAplicationCookie");
        //    return RedirectToAction("ConsultarCategoria", "Produto");
        //}



        // ALTERAR LOGIN

        public ActionResult EditarPerfil(int id)
        {
            UsuarioDAO dao = new UsuarioDAO();
            var usuario = dao.spSelectUsuario(id);

            return View(usuario);
        }

        [HttpPost]
        public ActionResult EditarPerfil(UsuarioViewModel vmusuario)
        {
            UsuarioDAO dao = new UsuarioDAO();
            dao.EditarPerfil(vmusuario);


            return View("ConsultarCategoria");
        }
    }
}