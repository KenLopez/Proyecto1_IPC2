using Proyecto1_IPC2.Models;
using Proyecto1_IPC2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1_IPC2.Controllers
{
    public class PartidaController : Controller
    {
        static Models.ViewModels.Partida juego = new Models.ViewModels.Partida();
        // GET: Partida/UnJugador
        public ActionResult UnJugador(User usuario)
        {
            if(TempData["Inicio"] == null)
            {
                TempData["Inicio"] = false;
            }
            if ((bool)TempData["Inicio"])
            {
                var rand = new Random();
                Models.ViewModels.Partida partida = new Models.ViewModels.Partida();
                partida.Turnos = 0;
                partida.P1.NombreUsuario = usuario.NombreUsuario;
                partida.P1.Id = usuario.Id;
                partida.P1.Color = rand.Next(1, 3);
                if (partida.P1.Color == 1)
                {
                    partida.P2.Color = 2;
                }
                else
                {
                    partida.P2.Color = 1;
                }
                partida.Mesa.Cuadricula[3, 3].Color = 1;
                partida.Mesa.Cuadricula[3, 4].Color = 2;
                partida.Mesa.Cuadricula[4, 3].Color = 2;
                partida.Mesa.Cuadricula[4, 4].Color = 1;

                partida.Cronometro = new System.Timers.Timer();
                if (rand.Next(1, 3) == 1)
                {
                    partida.Turno = partida.P1.Color;
                }
                else
                {
                    partida.Turno = partida.P2.Color;
                }
                partida.P1.Playable = true;
                partida.P2.Playable = true;

                TempData["Inicio"] = false;
                partida.enableSpaces();
                juego = partida;
            }
            TempData["Inicio"] = false;
            var viewResult = new ViewResult();
            viewResult.ViewData.Model = juego;
            return View(juego);
        }

        [HttpPost]
        public ActionResult PonerFicha(string boton)
        {
            int fila = Int32.Parse(boton) / 8;
            int columna = Int32.Parse(boton) % 8;
            juego.play(fila, columna, juego.Turno);
            juego.enableSpaces();
            return RedirectToAction("UnJugador");
        }
    }
}