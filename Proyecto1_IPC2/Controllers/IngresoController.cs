using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1_IPC2.Models;
using Proyecto1_IPC2.Models.ViewModels;

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

        public ActionResult getUsuarios()
        {
            List<getUsuarioViewModel> listaUsuarios;
            using (Model1 db = new Model1())
            {
                listaUsuarios = (from d in db.Usuario
                                  select new getUsuarioViewModel
                                  {
                                      nombre = d.nombre,
                                      precio = (double)d.precio,
                                      categoria = d.Categoria1.nombre
                                  }).ToList();
            }

            return View(listaProductos);
        }
    }
}