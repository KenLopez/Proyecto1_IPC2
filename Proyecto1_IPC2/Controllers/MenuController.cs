using System;
using System.Collections.Generic;  
using System.IO;  
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Proyecto1_IPC2.Models.ViewModels;


namespace Proyecto1_IPC2.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Principal()
        { 
            return View();
        }


        [HttpGet]
        public ActionResult UploadFile()
        {
            return View("Principal");
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
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
                return Redirect("~/Partida/UnJugador");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View("Principal");
            }
        }
    }
}