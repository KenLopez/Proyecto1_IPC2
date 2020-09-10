using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1_IPC2.Controllers
{
    public class IngresoController : Controller
    {
        public ActionResult Inicio_sesion()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }
    }
}