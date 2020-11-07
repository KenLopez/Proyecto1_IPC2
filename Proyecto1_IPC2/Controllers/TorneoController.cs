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


        [HttpPost]
        public ActionResult defEquipos(string[] nombreEquipo, string[] jugador)
        {
            torneo.armarEquipos(nombreEquipo, jugador);
            return RedirectToAction("Llaves");
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
            return RedirectToAction("Llaves");
        }

        public ActionResult Llaves()
        {
            return View(torneo);
        }
    }
}