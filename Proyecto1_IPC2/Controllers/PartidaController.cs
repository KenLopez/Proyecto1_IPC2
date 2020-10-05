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
        static partidaViewModel juego = new partidaViewModel();
        static bool inicio = true;
        // GET: Partida/UnJugador
        public ActionResult UnJugador(Jugador usuario)
        {
            if (inicio)
            {
                var rand = new Random();
                partidaViewModel partida = new partidaViewModel();
                partida.Turnos = 0;
                partida.P1.Jugador = usuario;
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
                    partida.Turno = partida.P1;
                }
                else
                {
                    partida.Turno = partida.P2;
                }
                juego = partida;
                inicio = false;
                
            }
            var viewResult = new ViewResult();
            viewResult.ViewData.Model = juego;
            return View(juego);
        }
    }
}