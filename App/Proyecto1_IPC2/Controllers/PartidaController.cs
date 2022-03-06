﻿using Proyecto1_IPC2.Models;
using Proyecto1_IPC2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.Http.ModelBinding;

namespace Proyecto1_IPC2.Controllers
{
    public class PartidaController : Controller
    {
        static Models.ViewModels.Partida juego = new Models.ViewModels.Partida();
        static int counter;
        // GET: Partida/UnJugador

        public ActionResult UnJugador(usuarioViewModel usuario)
        {
            if (juego.IsPlaying == 1) 
            { 
                if(juego.Type == 2 | juego.Type == 8)
                {
                    juego.enableSpaces();
                    if(juego.Turno == juego.P1.Color)
                    {
                        juego.P1.Cronometro.Start();
                    }
                    else
                    {
                        juego.P2.Cronometro.Start();
                    }
                }else{
                    if(juego.Turno == juego.P1.Color)
                    {
                        juego.P1.Cronometro.Start();
                    }
                    if(juego.Turno == juego.P2.Color)
                    {
                        juego.disableAll();
                    }
                    else
                    {
                        juego.enableSpaces();
                    }
                }
                
            }
            var viewResult = new ViewResult();
            viewResult.ViewData.Model = juego;
            return View(juego);
        }

        public ActionResult DosJugadores(usuarioViewModel usuario)
        {
            initJuego(usuario);
            juego.Type = 2;
            return RedirectToAction("UnJugador", usuario);
        }

        [HttpPost]
        public ActionResult IniciarMaquina()
        {
            juego.IsPlaying = 1;
            if (juego.Turno == juego.P1.Color)
            {
                juego.P1.Cronometro.Start();
            }
            juego.enableSpaces();
            if (juego.Turno == juego.P2.Color)
            {
                juego.playMaquina();
                juego.P2.Cronometro.Stop();
            }
            return RedirectToAction("UnJugador", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario });
        }

        [HttpPost]
        public ActionResult JugarMaquina(usuarioViewModel usuario)
        {
            juego.playMaquina();
            if (juego.Turno == juego.P1.Color)
            {
                juego.enableSpaces();
                if (!juego.P1.Playable)
                {
                    juego.Turno = juego.P2.Color;
                    juego.enableSpaces();
                    if (!juego.P2.Playable)
                    {
                        juego.IsPlaying = 2;
                    }
                    else
                    {
                        juego.disableAll();
                    }
                }
            }
            if (juego.IsPlaying == 2 & juego.Type != 8)
            {
                
                guardarRegistro();
            }
            return RedirectToAction("UnJugador", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario });
        }

        public void guardarRegistro()
        {
            using (OTHELLOEntities db = new OTHELLOEntities())
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
                var partida = new Proyecto1_IPC2.Models.Partida
                {
                    idUsuario = juego.P1.Id,
                    idAdversario = null,
                    horaFecha = DateTime.Now,
                    idTipoPartida = juego.Type,
                    idEstado = result,
                    turnos = juego.P1.Movimientos,
                    tiempo = juego.P1.getTiempo(),
                };

                db.Partida.Add(partida);
                db.SaveChanges();
            }
        }

        public ActionResult JugadorMaquina(usuarioViewModel usuario)
        {
            initJuego(usuario);
            juego.Maquina = new Maquina(juego.P2.Color);
            juego.Type = 1;
            juego.P2.NombreUsuario = "Máquina";
            return RedirectToAction("UnJugador", usuario);
        }

        public ActionResult setTablero(usuarioViewModel usuario)
        {
            initJuego(usuario);
            if(TempData["Type"].ToString() == "1")
            {
                juego.Type = 1;
                juego.Maquina = new Maquina(juego.P2.Color);
                juego.P2.NombreUsuario = "Máquina";
            }
            else if(TempData["Type"].ToString() == "2")
            {
                juego.Type = 2;
            }
            XmlTextReader reader = new XmlTextReader(TempData["Ruta"].ToString());
            Tablero tablero = new Tablero();
            reader.Read();
            if (reader.IsStartElement("tablero"))
            {
                reader.ReadToDescendant("ficha");
                while (reader.IsStartElement("ficha"))
                {
                    reader.ReadToDescendant("color");
                    string color = reader.ReadElementContentAsString();
                    reader.ReadToNextSibling("columna");
                    string columna = reader.ReadElementContentAsString();
                    reader.ReadToNextSibling("fila");
                    int fila = reader.ReadElementContentAsInt();
                    int col = tablero.letraToInt(columna);
                    tablero.Cuadricula[fila - 1, col].Color = tablero.colorToInt(color);
                    reader.Read();
                    reader.Read();
                }
                juego.Mesa = tablero;
                reader.ReadToDescendant("color");
                string turn = reader.ReadElementContentAsString();
                juego.Turno = juego.Mesa.colorToInt(turn);
            }
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
                partida.P1.Colores = new int[1];
                partida.P1.Colores[0] = partida.P1.Color;
                partida.P2.Color = 2;
                partida.P2.Colores = new int[1];
                partida.P2.Colores[0] = partida.P2.Color;
                partida.Orden = new int[2];
                partida.Orden[0] = partida.P1.Color;
                partida.Orden[1] = partida.P2.Color;
            }
            else
            {
                partida.P2.Colores = new int[1];
                partida.P2.Colores[0] = partida.P2.Color;
                partida.P1.Color = 2;
                partida.P1.Colores = new int[1];
                partida.P1.Colores[0] = partida.P1.Color;
                partida.Orden = new int[2];
                partida.Orden[0] = partida.P2.Color;
                partida.Orden[1] = partida.P1.Color;
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

        public ActionResult Torneo()
        {
            juego = TempData["partida"] as Models.ViewModels.Partida;
            if (juego.Turno == juego.P1.Color)
            {
                juego.P1.Cronometro.Start();
            }
            else
            {
                juego.P2.Cronometro.Start();
            }
            return RedirectToAction("UnJugador", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario });
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
            if(juego.IsPlaying != -1)
            {
                juego.IsPlaying = 1;
                if(juego.Turno == juego.P1.Color)
                {
                    juego.P1.Cronometro.Start();
                }
                else
                {
                    juego.P2.Cronometro.Start();
                }
            }
            else
            {
                juego.IsPlaying = -2;
                juego.Mesa.Cuadricula[3, 3].Estado = true;
                juego.Mesa.Cuadricula[3, 4].Estado = true;
                juego.Mesa.Cuadricula[4, 3].Estado = true;
                juego.Mesa.Cuadricula[4, 4].Estado = true;

            }
            return RedirectToAction("UnJugador", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario });
        }

        [HttpPost]
        public ActionResult PonerFicha(string boton)
        {
            int fila = Int32.Parse(boton) / 8;
            int columna = Int32.Parse(boton) % 8;
            if(juego.IsPlaying == -2)
            {
                juego.Mesa.Cuadricula[fila, columna].Color = juego.Turno;
                juego.Mesa.Cuadricula[fila, columna].Estado = false;
                counter++;
                if (juego.Turno == 1) { juego.Turno = 2; }
                else { juego.Turno = 1; }
                if(counter == 4)
                {
                    juego.IsPlaying = 1;
                    if (juego.Turno == juego.P1.Color)
                    {
                        juego.P1.Cronometro.Start();
                    }
                    else
                    {
                        juego.P2.Cronometro.Start();
                    }
                }
                return RedirectToAction("UnJugador", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario });
            }
            if(juego.Type == 1)
            {
                juego.P1.Cronometro.Stop();
            }
            else
            {
                if(juego.Turno == juego.P1.Color)
                {
                    juego.P1.Cronometro.Stop();
                }
                else
                {
                    juego.P2.Cronometro.Stop();
                }
            }
            juego.play(fila, columna, juego.Turno);
            juego.enableSpaces();
            for (int i = 0; i <= juego.Orden.Length; i++)
            {
                if (i != juego.Orden.Length)
                {
                    if (juego.enableSpaces())
                    {
                        if (juego.Type == 1 )
                        {
                            if (juego.P2.colorExists(juego.Turno))
                            {
                                juego.disableAll();
                            }
                        }
                        break;
                    }
                    else
                    {
                        juego.cambiarTurno();
                    }
                }
                else
                {
                    juego.IsPlaying = 2;
                }
            }
            if (juego.IsPlaying == 2 & juego.Type != 8)
            {
                guardarRegistro();
            }
            return RedirectToAction("UnJugador", new usuarioViewModel { Id = juego.P1.Id, NombreUsuario = juego.P1.NombreUsuario }); 
        }

        [HttpPost]
        public ActionResult Rendirse()
        {
            if(juego.Turno == juego.P1.Color)
            {
                TempData["ganador"] = 2;
            }
            else if(juego.Turno == juego.P2.Color)
            {
                TempData["ganador"] = 1;
            }
            TempData["terminada"] = juego;
            return RedirectToAction("Resultados", "Torneo");
        }

        [HttpPost]
        public ActionResult RegresarTorneo()
        {
            TempData["terminada"] = juego;
            TempData["ganador"] = juego.Ganador();
            return RedirectToAction("Resultados", "Torneo");
        }

        [HttpPost]
        public ActionResult CambiarColor()
        {
            if (juego.P1.Color == 1) 
            { 
                juego.P1.Color = 2; 
                juego.P2.Color = 1; 
            }
            else { 
                juego.P1.Color = 1; 
                juego.P2.Color = 2; 
            }
            if(juego.Type == 1 | juego.Type == 6 | juego.Type == 7)
            {
                juego.Maquina.Color = juego.P2.Color;
            }
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

        [HttpPost]
        public ActionResult Timers()
        {
            return PartialView("_Cronometros", juego);
        }

        public FileResult Descargar()
        {
            string data = juego.toXml();
            string virtualPath = Server.MapPath("~/XMLFiles/partida-" + DateTime.Now.Day.ToString()+DateTime.Now.Month.ToString()+DateTime.Now.Year.ToString()+
                "-"+DateTime.Now.Hour.ToString()+"_"+DateTime.Now.Minute.ToString()+"_"+DateTime.Now.Second+".xml");
            using (FileStream file = System.IO.File.Create(virtualPath))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(data);
                file.Write(info, 0, info.Length);
            }
            return File(virtualPath, "application/force- download", Path.GetFileName(virtualPath));
        }
    }
}