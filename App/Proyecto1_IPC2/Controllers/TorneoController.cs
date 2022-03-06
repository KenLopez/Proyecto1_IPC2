using Proyecto1_IPC2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1_IPC2.Models;

namespace Proyecto1_IPC2.Controllers
{
    public class TorneoController : Controller
    {
        static Models.ViewModels.Torneo torneo;
        static Models.ViewModels.usuarioViewModel usuario;
        // GET: Torneo
        public ActionResult Equipos(usuarioViewModel creador)
        {
            usuario = creador;
            torneo = new Models.ViewModels.Torneo(creador, TempData["nombre"].ToString(), Int32.Parse(TempData["cantidad"].ToString()));
            return View(torneo);
        }

        public ActionResult Resultados()
        {
            torneo.getRondaActual().sumarPuntos(Int32.Parse(TempData["ganador"].ToString()));
            torneo.sigEncuentro();
            return RedirectToAction("Llaves");
        }


        [HttpPost]
        public ActionResult defEquipos(string[] nombreEquipo, string[] jugador)
        {
            torneo.armarEquipos(nombreEquipo, jugador);
            torneo.getRondaActual().crearEncuentros();
            return RedirectToAction("Llaves");
        }

        [HttpPost]
        public ActionResult Continuar(int opcion, int desempate1 = 0, int desempate2 = 0)
        {
            switch (opcion)
            {
                case 1:
                    torneo.getRondaActual().sumarPuntos(1);
                    torneo.sigEncuentro();
                    return RedirectToAction("Llaves");
                case 2:
                    torneo.getRondaActual().sumarPuntos(3);
                    torneo.sigEncuentro();
                    return RedirectToAction("Llaves");
                case 3:
                    torneo.getRondaActual().sumarPuntos(2);
                    torneo.sigEncuentro();
                    return RedirectToAction("Llaves");
                case 4:
                    if(torneo.getRondaActual().Estado == 2)
                    {
                        torneo.getRondaActual().getEncuentroActual().Jugador1 = torneo.getRondaActual().Equipo1.Integrantes[desempate1];
                        torneo.getRondaActual().getEncuentroActual().Jugador2 = torneo.getRondaActual().Equipo2.Integrantes[desempate2];
                    }
                    torneo.getRondaActual().getEncuentroActual().createPartida();
                    TempData["partida"] = torneo.getRondaActual().getEncuentroActual().Partida;
                    return RedirectToAction("Torneo", "Partida");
                default:
                    return RedirectToAction("Llaves");
            }
            
        }

        public ActionResult setTeams(usuarioViewModel creador)
        {
            usuario = creador;
            torneo = new Models.ViewModels.Torneo(creador, TempData["nombre"].ToString(), Int32.Parse(TempData["cantidad"].ToString()));
            List<string> teams = TempData["teams"] as List<string>;
            List<string> players = TempData["players"] as List<string>;
            string[] equipos = new string[teams.Count];
            string[] jugadores = new string[players.Count];
            int i = 0;
            foreach(string team in teams)
            {
                equipos[i] = team;
                i++;
            }
            i = 0;
            foreach(string player in players)
            {
                jugadores[i] = player;
                i++;
            }
            torneo.armarEquipos(equipos, jugadores);
            torneo.getRondaActual().crearEncuentros();
            return RedirectToAction("Llaves");
        }

        public ActionResult Llaves()
        {
            return View(torneo);
        }

        public ActionResult Salir()
        {
            return RedirectToAction("Principal", "Menu", new usuarioViewModel { Id = torneo.Creador.Id, NombreUsuario = torneo.Creador.NombreUsuario });
        }
    }
}