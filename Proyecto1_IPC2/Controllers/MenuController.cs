using System;
using System.Collections.Generic;  
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
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        file.SaveAs(_path);
                        XmlTextReader reader = new XmlTextReader(_path);
                        string etiquetaFinal = "";
                        while (reader.Read())
                        {

                        }
                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                    TempData["Inicio"] = true;
                    return RedirectToAction("UnJugador", "Partida", usuario);
                }
                return View("Principal", usuario);
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View("Principal", usuario);
            }
        }

        [HttpPost]
        public ActionResult PVM()
        {
            TempData["Inicio"] = true;
            return RedirectToAction("UnJugador", "Partida", usuario);
        }
    }

}
