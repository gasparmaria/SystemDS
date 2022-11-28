using DragonSushi_ASP.NET.DAO;
using DragonSushi_ASP.NET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragonSushi_ASP.NET.Controllers
{
    public class ReservaController : Controller
    {
        // RESERVAS (MOSTRAR RESERVAS PELA DATA)

        [HttpGet]
        public ActionResult ListarReserva()
        {
            ReservaDAO dao = new ReservaDAO();
            var reserva = dao.ExibirReserva();

            return View(reserva);
        }

        // CONSULTAR RESERVA POR CPF

        public ActionResult ConsultarReserva()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConsultarReserva(string cpf)
        {
            ReservaDAO dao = new ReservaDAO();
            var reserva = dao.ConsultarReserva(cpf);

            return View(reserva);
        }

        // CADASTRAR RESERVA (ADICIONAR RESERVA)

        public ActionResult CadastrarReserva()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarReserva(ReservaViewModel vmreserva)
        {
            ReservaDAO dao = new ReservaDAO();
            dao.CadastrarReserva(vmreserva);
            var reserva = dao.ExibirReserva();

            return View("ListarReserva", reserva);
        }

        // ALTERAR RESERVA (ALTERAR RESERVA)

        public ActionResult EditarReserva(int id)
        {
            ReservaDAO dao = new ReservaDAO();
            var reserva = dao.spSelectReservas(id);

            return View(reserva);
        }

        [HttpPost]
        public ActionResult EditarReserva(ReservaViewModel vmreserva)
        {
            ReservaDAO dao = new ReservaDAO();
            dao.EditarReserva(vmreserva);
            var reserva = dao.ExibirReserva();

            return View("ListarReserva", reserva);
        }

        // EXCLUIR RESERVA 


        public ActionResult ExcluirReserva(int id)
        {
            ReservaDAO dao = new ReservaDAO();
            dao.ExcluirReserva(id);
            var reserva = dao.ExibirReserva();

            return View("ListarReserva", reserva);
        }


    }
}