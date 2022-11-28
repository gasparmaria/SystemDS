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
using JsonSerializer = System.Text.Json.JsonSerializer;

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
        public ActionResult CadastrarUsuario(UsuarioViewModel vmUsuario)
        {
            UsuarioDAO dao = new UsuarioDAO();
            dao.cadastrarUsuario(vmUsuario);
            return RedirectToAction("ConsultarCategoria", "Produto"); 
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

            var login = dao.ConsultarUsuario(vmusuario.Usuario.login);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string fileName = "C:/Users/Naja Informatica/Downloads/SystemDS/DragonSushi_ASP.NET/DataBase/usuariologado.json";
            string jsonString = JsonSerializer.Serialize(login, options);
            System.IO.File.WriteAllText(fileName, jsonString);

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
                    return RedirectToAction("ConsultarCategoria", "Produto");
                }
            }
        }

        // LOGOUT
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Usuario");
        }


        // ALTERAR PERFIL
        public ActionResult EditarPerfil()
        {
            string fileName = "C:/Users/Naja Informatica/Downloads/SystemDS/DragonSushi_ASP.NET/DataBase/usuariologado.json";
            string jsonString = System.IO.File.ReadAllText(fileName);
            UsuarioViewModel vmusuario = JsonSerializer.Deserialize<UsuarioViewModel>(jsonString);

            UsuarioDAO dao = new UsuarioDAO();
            var usuario = dao.ConsultarUsuario(vmusuario.Usuario.login);

            return View(usuario);
        }

        [HttpPost]
        public ActionResult EditarPerfil(UsuarioViewModel vmusuario)
        {
            string fileName = "C:/Users/Naja Informatica/Downloads/SystemDS/DragonSushi_ASP.NET/DataBase/usuariologado.json";
            string jsonString = System.IO.File.ReadAllText(fileName);
            UsuarioViewModel usuario = JsonSerializer.Deserialize<UsuarioViewModel>(jsonString);
            string senha = usuario.Usuario.senha;

            if (vmusuario.Usuario.senha == senha)
            {
                UsuarioDAO dao = new UsuarioDAO();
                dao.EditarPerfil(vmusuario);

                return RedirectToAction("ConsultarCategoria", "Produto");
            }else
            {
                return View();
            }
        }
    }
}