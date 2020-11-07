using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;  
using System.Linq;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using Proyecto1_IPC2.Models;
using Proyecto1_IPC2.Models.ViewModels;



namespace Proyecto1_IPC2.Controllers
{
    public class MenuController : Controller
    {
        static usuarioViewModel usuario = new usuarioViewModel();
        // GET: Menu/Principal
        public ActionResult Principal(usuarioViewModel model)
        {
            usuario = model;
            var viewResult = new ViewResult();
            viewResult.ViewData.Model = model;

            return View(model);
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            return View("Principal", usuario);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, string opcion)
        {
            try
            {
                if (file != null)
                {
                    if (Path.GetExtension(file.FileName) != ".xml")
                    {
                        ViewBag.Message = "El archivo subido debe tener la extensión xml";
                        return View("Principal", usuario);
                    }
                    if (file.ContentLength > 0)
                    {
                        
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        int counter = 1;
                        while (System.IO.File.Exists(_path))
                        {
                            _FileName = Path.GetFileNameWithoutExtension(file.FileName);
                            _FileName = _FileName + "(" + counter + ").xml";
                            _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                            counter += 1;
                        }
                        file.SaveAs(_path);
                        XmlTextReader reader = new XmlTextReader(_path);
                        Tablero tablero = new Tablero();
                        reader.Read();
                        try
                        {
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
                                if (tablero.validar())
                                {
                                    TempData["Type"] = opcion;
                                    TempData["Ruta"] = _path;
                                    return RedirectToAction("setTablero", "Partida", usuario);
                                }
                                else
                                {
                                    ViewBag.Message = "ERROR: El archivo XML posee errores en su contenido.";
                                    return View("Principal", usuario);
                                }

                            }
                        }
                        catch
                        {
                            ViewBag.Message = "ERROR: El archivo XML posee errores en su contenido.";
                            return View("Principal", usuario);
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "No se subió ningún archivo.";
                }
                return View("Principal", usuario);
            }
            catch
            {
                ViewBag.Message = "El archivo no pudo ser cargado.";
                return View("Principal", usuario);
            }
        }

        [HttpPost]
        public ActionResult ConfigTorneo(string nombre, int participantes, HttpPostedFile file = null)
        {
            TempData["nombre"] = nombre;
            TempData["cantidad"] = participantes;
            if(file == null)
            {
                return RedirectToAction("Equipos", "Torneo", usuario);
            }
            else
            {
                return RedirectToAction("setTeams", "Torneo", usuario);
            }
        }

        [HttpPost]
        public ActionResult UploadFileX(HttpPostedFileBase file, string opcion)
        {
            try
            {
                if (file != null)
                {
                    if (Path.GetExtension(file.FileName) != ".xml")
                    {
                        ViewBag.MessageX = "El archivo subido debe tener la extensión xml";
                        return View("Principal", usuario);
                    }
                    if (file.ContentLength > 0)
                    {

                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        int counter = 1;
                        while (System.IO.File.Exists(_path))
                        {
                            _FileName = Path.GetFileNameWithoutExtension(file.FileName);
                            _FileName = _FileName + "(" + counter + ").xml";
                            _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                            counter += 1;
                        }
                        file.SaveAs(_path);
                        XmlTextReader reader = new XmlTextReader(_path);
                        Tablero tablero = new Tablero();
                        reader.Read();
                        try
                        {
                            if (reader.IsStartElement("partida"))
                            {
                                reader.ReadToDescendant("filas");
                                tablero.Filas = reader.ReadElementContentAsInt();
                                reader.ReadToNextSibling("columnas");
                                tablero.Columnas = reader.ReadElementContentAsInt();
                                tablero.dimensionTablero();
                                reader.ReadToNextSibling("Jugador1");
                                reader.ReadToDescendant("color");
                                List<int> coloresP1 = new List<int>();
                                while (reader.IsStartElement("color"))
                                {
                                    coloresP1.Add(tablero.colorToInt(reader.ReadElementContentAsString()));
                                    reader.Read();
                                }
                                int[] colorP1 = coloresP1.ToArray();
                                reader.Read();
                                List<int> coloresP2 = new List<int>();
                                reader.ReadToNextSibling("Jugador2");
                                reader.ReadToDescendant("color");
                                while (reader.IsStartElement("color"))
                                {
                                    coloresP2.Add(tablero.colorToInt(reader.ReadElementContentAsString()));
                                    reader.Read();
                                }
                                int[] colorP2 = coloresP2.ToArray();
                                reader.Read();
                                reader.ReadToNextSibling("Modalidad");
                                string modalidad = reader.ReadElementContentAsString();
                                bool inverso;
                                if(modalidad == "Inversa")
                                {
                                    inverso = true;
                                }
                                else if(modalidad == "Normal")
                                {
                                    inverso = false;
                                }
                                else
                                {
                                    ViewBag.MessageX = "ERROR: El archivo XML posee errores en su contenido.";
                                    return View("Principal", usuario);
                                }
                                reader.ReadToNextSibling("tablero");
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
                                reader.ReadToDescendant("color");
                                TempData["siguienteTiro"] = tablero.colorToInt(reader.ReadElementContentAsString());
                                if (tablero.validar())
                                {
                                    if(opcion == "1")
                                    {
                                        if (inverso)
                                        {
                                            TempData["Type"] = 7;
                                        }
                                        else
                                        {
                                            TempData["Type"] = 6;
                                        }
                                    }
                                    else
                                    {
                                        if (inverso)
                                        {
                                            TempData["Type"] = 5;
                                        }
                                        else
                                        {
                                            TempData["Type"] = 4;
                                        }
                                    }
                                    if (colorP1.Length == 0 | colorP2.Length == 0)
                                    {
                                        ViewBag.MessageX = "ERROR: El archivo XML posee errores en su contenido.";
                                        return View("Principal", usuario);
                                    }
                                    foreach(int color in colorP1)
                                    {
                                        foreach(int color2 in colorP2)
                                        {
                                            if(color == color2)
                                            {
                                                ViewBag.MessageX = "ERROR: El archivo XML posee errores en su contenido.";
                                                return View("Principal", usuario);
                                            }
                                        }
                                    }
                                    TempData["colores1"] = colorP1;
                                    TempData["colores2"] = colorP2;
                                    TempData["tablero"] = tablero;
                                    TempData["columnas"] = tablero.Columnas;
                                    TempData["filas"] = tablero.Filas;
                                    return RedirectToAction("setTablero", "PartidaX", usuario);
                                }
                                else
                                {
                                    ViewBag.MessageX = "ERROR: El archivo XML posee errores en su contenido.";
                                    return View("Principal", usuario);
                                }

                            }
                        }
                        catch
                        {
                            ViewBag.MessageX = "ERROR: El archivo XML posee errores en su contenido.";
                            return View("Principal", usuario);
                        }
                    }
                }
                else
                {
                    ViewBag.MessageX = "No se subió ningún archivo.";
                }
                return View("Principal", usuario);
            }
            catch
            {
                ViewBag.MessageX = "El archivo no pudo ser cargado.";
                return View("Principal", usuario);
            }
        }

        [HttpPost]
        public ActionResult PVM()
        {
            TempData["Inicio"] = true;
            return RedirectToAction("JugadorMaquina", "Partida", usuario);
        }

        [HttpPost]
        public ActionResult PVP()
        {
            TempData["Inicio"] = true;
            return RedirectToAction("DosJugadores", "Partida", usuario);
        }

        [HttpPost]
        public ActionResult PartidaXtream(int[] coloresP1, int[] coloresP2, bool maquina, bool modo, bool inicio = false, int filas = 0, int columnas = 0)
        {
            if (coloresP1 == null | coloresP2 == null)
            {
                ViewBag.ErrorColores = "Ambos jugadores deben escoger al menos un color.";
                ViewBag.XtreamError = true;
                return View("Principal", usuario);
            }
            foreach (int color in coloresP1)
            {
                foreach(int color2 in coloresP2)
                {
                    if(color == color2)
                    {
                        ViewBag.ErrorColores = "Los jugadores no pueden tener colores en común.";
                        ViewBag.XtreamError = true;
                        return View("Principal", usuario);
                    }
                }
            }
            TempData["filas"] = filas;
            TempData["columnas"] = columnas;
            TempData["coloresP1"] = coloresP1;
            TempData["coloresP2"] = coloresP2;
            TempData["personalizado"] = inicio;
            Console.Write(TempData["coloresP1"]);
            if(maquina && modo)
            {
                TempData["modo"] = 6;
            }
            else if(maquina && !modo)
            {
                TempData["modo"] = 7;
            }
            else if(!maquina && modo)
            {
                TempData["modo"] = 4;
            }
            else if(!maquina && !modo)
            {
                TempData["modo"] = 5;
            }
            return RedirectToAction("PartidaXtream", "PartidaX" , usuario);
        }
    }

}
