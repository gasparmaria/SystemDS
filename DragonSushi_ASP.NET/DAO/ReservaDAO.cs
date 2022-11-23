using DragonSushi_ASP.NET.DataBase;
using DragonSushi_ASP.NET.Models;
using DragonSushi_ASP.NET.ViewModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DragonSushi_ASP.NET.DAO
{
    public class ReservaDAO
    {

        // CADASTRAR RESERVA
        public void CadastrarReserva(ReservaViewModel vmReserva)
        {
            Database db = new Database();

            string insertQuery = String.Format("call spCadastrarReserva(@dataReserva,@hora,@numPessoas,@cpf)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@dataReserva", MySqlDbType.Date).Value = vmReserva.Reserva.dataReserva;
            command.Parameters.Add("@hora", MySqlDbType.Time).Value = vmReserva.Reserva.hora;
            command.Parameters.Add("@numPessoas", MySqlDbType.Int32).Value = vmReserva.Reserva.numPessoas;
            command.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = vmReserva.Pessoa.cpf;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }

        // LISTAR RESERVAS POR DATA
        public List<ReservaViewModel> ExibirReserva()
        {
            Database db = new Database();
            {
                MySqlCommand exibir = new MySqlCommand("CALL spListarReserva()", db.conectarDb());
                var leitor = exibir.ExecuteReader();
                return listaReserva(leitor);
            }
        }

        // CONSULTAR RESERVA PELO CPF DO CLIENTE
        public ReservaViewModel ConsultarReserva(string cpf)
        {
            Database db = new Database();
            {
                string strQuery = string.Format("CALL spConsultarReserva('{0}');", cpf);
                MySqlCommand exibir = new MySqlCommand(strQuery, db.conectarDb());
                var leitor = exibir.ExecuteReader();
                return listaReserva(leitor).FirstOrDefault();
            }
        }

        // GERADOR DE LISTA 
        public List<ReservaViewModel> listaReserva(MySqlDataReader leitor)
        {
            var reserva = new List<ReservaViewModel>();

            while (leitor.Read())
            {
                var lstReserva = new ReservaViewModel()
                {
                    Reserva = new Reserva()
                    {
                        idReserva = Convert.ToInt32(leitor["idReserva"]),
                        dataReserva = Convert.ToDateTime(leitor["dataReserva"]),
                        hora = TimeSpan.Parse(Convert.ToString(leitor["hora"])),
                        numPessoas = Convert.ToInt32(leitor["numPessoas"])

                    },
                    Pessoa = new Pessoa()
                    {
                        nomePessoa = Convert.ToString(leitor["nomePessoa"])
                    }
                };
                reserva.Add(lstReserva);
            }

            leitor.Close();
            return reserva;
        }

        // EDITAR RESERVA
        public void EditarReserva(ReservaViewModel vmReserva)
        {
            Database db = new Database();

            string insertQuery = String.Format("CALL spEditarReserva(@idReserva,@dataReserva,@hora,@numPessoas)");
            MySqlCommand command = new MySqlCommand(insertQuery, db.conectarDb());
            command.Parameters.Add("@idReserva", MySqlDbType.Int32).Value = vmReserva.Reserva.idReserva;
            command.Parameters.Add("@dataReserva", MySqlDbType.Date).Value = vmReserva.Reserva.dataReserva;
            command.Parameters.Add("@hora", MySqlDbType.Time).Value = vmReserva.Reserva.hora;
            command.Parameters.Add("@numPessoas", MySqlDbType.Int32).Value = vmReserva.Reserva.numPessoas;

            command.ExecuteNonQuery();
            db.desconectarDb();
        }

        public ReservaViewModel spSelectReservas(int id)
        {
            Database db = new Database();
            string selectQuery = String.Format("CALL spSelectReservas('{0}')", id);
            MySqlCommand command = new MySqlCommand(selectQuery, db.conectarDb());
            var dados = command.ExecuteReader();


            return listaReserva(dados).FirstOrDefault();
        }

        // EXCLUIR RESERVA
        public void ExcluirReserva(int id)
        {
            Database db = new Database();

            string deleteQuery = String.Format("CALL spExcluirReserva('{0}')", id);
            MySqlCommand command = new MySqlCommand(deleteQuery, db.conectarDb());
            command.ExecuteNonQuery();

            db.desconectarDb();
        }
    }
}