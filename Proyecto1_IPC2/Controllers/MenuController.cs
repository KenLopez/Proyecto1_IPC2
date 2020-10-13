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
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    if (Path.GetExtension(file.FileName) != "xml")
                    {
                        ViewBag.Message = "El archivo subido debe tener la extensión xml";
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
                                    TempData["Ruta"] = _path;
                                    return RedirectToAction("setTablero", "Partida", usuario);
                                }
                                else
                                {
                                    ViewBag.Message = "ERROR: El archivo XML posee errores en su contenido.";
                                    return RedirectToAction("Principal", usuario);
                                }

                            }
                        }
                        catch
                        {
                            ViewBag.Message = "ERROR: El archivo XML posee errores en su contenido.";
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
    }

}
