using Proyecto1_IPC2.Models;
using Proyecto1_IPC2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Proyecto1_IPC2.Controllers
{
    public class PartidaController : Controller
    {
        static bool pvp;
        static Models.ViewModels.Partida juego = new Models.ViewModels.Partida();
        // GET: Partida/UnJugador
        public ActionResult UnJugador(usuarioViewModel usuario)
        {
            if (TempData["Inicio"] != null)
            {
                initJuego(usuario);
                juego.Type = 1;
            }
            if (juego.IsPlaying == 1) { juego.enableSpaces(); }
            var viewResult = new ViewResult();
            viewResult.ViewData.Model = juego;
            return View(juego);
        }

        public ActionResult DosJugadores(usuarioViewModel usuario)
        {
            TempData["Inicio"] = null;
            initJuego(usuario);
            juego.Type = 2;
            pvp = true;
            return RedirectToAction("UnJugador", usuario);
        }

        public void initJuego(usuarioViewModel usuario)
        {
            var rand = new Random();
            Models.ViewModels.Partida partida = new Models.ViewModels.Partida();
            partida.Turnos = 0;
            partida.P1.NombreUsuario = usuario.NombreUsuario;
            partida.P1.Id = usuario.Id;
            partida.P1.Movimientos = 0;
            partida.P2.Movimientos = 0;
            partida.Mesa.Cuadricula[3, 3].Color = 1;
            partida.Mesa.Cuadricula[3, 4].Color = 2;
            partida.Mesa.Cuadricula[4, 3].Color = 2;
            partida.Mesa.Cuadricula[4, 4].Color = 1;
            partida.P1.Playable = true;
            partida.P2.Playable = true;
            partida.IsPlaying = 0;
            partida.P1.Color = rand.Next(1, 3);
            if (partida.P1.Color == 1)
            {
                partida.P2.Color = 2;
            }
            else
            {
                partida.P2.Color = 1;
            }
            if (rand.Next(1, 3) == 1)
            {
                partida.Turno = partida.P1.Color;
            }
            else
            {
                partida.Turno = partida.P2.Color;
            }
            juego = partida;
        }

        [HttpPost]
        public ActionResult DefinirP2(string p2Nombre)
        {
            if(p2Nombre == "" | p2Nombre == null)
            {
                juego.P2.NombreUsuario = "Jugador2";
            }
            else
            {
                juego.P2.NombreUsuario = p2Nombre;
            }
            juego.IsPlaying = 1;
            return RedirectToAction("UnJugador", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario });
        }

        [HttpPost]
        public ActionResult PonerFicha(string boton)
        {
            int fila = Int32.Parse(boton) / 8;
            int columna = Int32.Parse(boton) % 8;
            juego.play(fila, columna, juego.Turno);
            juego.enableSpaces();
            if(juego.Turno == juego.P1.Color)
            {
                if (!juego.P1.Playable)
                {
                    juego.Turno = juego.P2.Color;
                    juego.enableSpaces();
                    if (!juego.P2.Playable)
                    {
                        juego.IsPlaying = 2;
                    }
                }
            }
            else
            {
                if (!juego.P2.Playable)
                {
                    juego.Turno = juego.P1.Color;
                    juego.enableSpaces();
                    if (!juego.P1.Playable)
                    {
                        juego.IsPlaying = 2;
                    }
                }
            }
            if(juego.IsPlaying == 2)
            {
                juego.getGanador();
                int result;
                if (juego.Winner == null)
                {
                    result = 3;
                }
                else if (juego.Winner == juego.P1)
                {
                    result = 1;
                }
                else
                {
                    result = 2;
                }
                using (OTHELLOEntities db = new OTHELLOEntities())
                {
                    var partida = new Proyecto1_IPC2.Models.Partida
                    {
                        idUsuario = juego.P1.Id,
                        idAdversario = null,
                        horaFecha = DateTime.Now,
                        idTipoPartida = juego.Type,
                        idEstado = result,
                        turnos = juego.P1.Movimientos,
                    };

                    db.Partida.Add(partida);
                    db.SaveChanges();
                }
                return RedirectToAction("UnJugador");
            }
            return RedirectToAction("UnJugador", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario }); 
        }

        [HttpPost]
        public ActionResult CambiarColor()
        {
            if (juego.P1.Color == 1) { juego.P1.Color = 2; juego.P2.Color = 1; }
            else { juego.P1.Color = 1; juego.P2.Color = 2; }
            return RedirectToAction("UnJugador", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario });
        }

        [HttpPost]
        public ActionResult CambiarTurno()
        {
            if(juego.Turno == 1) { juego.Turno = 2; }
            else { juego.Turno = 1; }
            return RedirectToAction("UnJugador", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario });
        }

        [HttpPost]
        public ActionResult Salir()
        {
            return RedirectToAction("Principal", "Menu", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario });
        }
    }
}