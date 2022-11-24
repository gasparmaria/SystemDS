using DragonSushi_ASP.NET.DAO;
using DragonSushi_ASP.NET.Models;
using DragonSushi_ASP.NET.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.Script.Serialization;

namespace DragonSushi_ASP.NET.Controllers
{
    public class UsuarioController : Controller
    {
        // CADASTRAR USUÁRIO 

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

        // LOGIN
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsuarioViewModel vmusuario)
        {
            UsuarioDAO dao = new UsuarioDAO();

            var login = dao.spSelectUsuario(vmusuario.Usuario.login);

            //string usuario = login.ToString();
            //FileStream createStream = File.Create(fileName);

            string fileName = "usuariologado.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(vmusuario);
            File.WriteAllText(fileName, jsonString);


            JavaScriptSerializer serializer = new JavaScriptSerializer();

            if (login == null)
                return View();
            else
            {
                int ocupacao = login.Pessoa.ocupacao;

                if (ocupacao == 1)
                {
                    return RedirectToAction("AreaGerente", "Funcionario");
                }
                else if (ocupacao == 2)
                {
                    return RedirectToAction("AreaFuncionario", "Funcionario");
                }
                else
                {
                    return RedirectToAction("ConsultarCardapio", "Produto");
                }
            }
        }

        // LOGOUT
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Usuario");
        }


        // ALTERAR LOGIN
        public ActionResult EditarPerfil(UsuarioViewModel vmusuario)
        {
            UsuarioDAO dao = new UsuarioDAO();
            var usuario = dao.spSelectUsuario(vmusuario.Usuario.login);

            return View(usuario);
        }

        [HttpPost]
        public ActionResult EditarPerfil(UsuarioViewModel vmusuario)
        {
            UsuarioDAO dao = new UsuarioDAO();
            dao.EditarPerfil(vmusuario);


            return RedirectToAction("ConsultarCategoria", "Produto");
        }
    }
}