using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<getUsuarioViewModel> getUsuarios()
        {
            List<getUsuarioViewModel> listaUsuarios;
            using (OTHELLOEntities db = new OTHELLOEntities())
            {
                listaUsuarios = (from d in db.Usuario
                                 select new getUsuarioViewModel
                                 {
                                     nombreUsuario = d.nombreUsuario,
                                     contraseña = d.contraseña,
                                     correo = d.correo
                                 }).ToList();
            }
            return listaUsuarios;
        }

        [HttpPost]
        public JsonResult usuarioRegistrado(string nombreUsuario)
        {

            return Json(usuarioDisponible(nombreUsuario), JsonRequestBehavior.AllowGet);

        }

        public bool usuarioDisponible(string nombreUsuario)
        { 
            List<getUsuarioViewModel> users = getUsuarios();

            foreach (var user in users)
            {
                if (user.nombreUsuario == nombreUsuario)
                {
                    return false;
                }
            }
            return true;
        }

        [HttpPost]
        public ActionResult Registro(addUsuarioViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid && usuarioDisponible(modelo.nombreUsuario) && modelo.confirmar == modelo.contraseña)
                {
                    using (OTHELLOEntities db = new OTHELLOEntities())
                    {
                        var usuario = new Usuario
                        {
                            nombre = modelo.nombre,
                            apellido = modelo.apellido,
                            nombreUsuario = modelo.nombreUsuario,
                            contraseña = modelo.contraseña,
                            fechaNacimiento = modelo.fecha,
                            pais = modelo.pais,
                            correo = modelo.correo
                        };

                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                    }
                    return Redirect("~/Menu/Principal");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    [HttpPost]
        public ActionResult Inicio_sesion(getUsuarioViewModel modelo)
        {
            return Redirect("~/Menu/Principal");
            List<getUsuarioViewModel> listaUsuarios = getUsuarios();
            foreach (var user in listaUsuarios)
            {
                if (modelo.nombreUsuario == user.nombreUsuario && modelo.contraseña == user.contraseña)
                {
                    return Redirect("~/Menu/Principal");
                }

            }

            return View();
        }
    }
}