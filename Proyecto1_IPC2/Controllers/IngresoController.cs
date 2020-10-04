using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Proyecto1_IPC2.Models;
using Proyecto1_IPC2.Models.ViewModels;
using Proyecto1_IPC2.Controllers;
using System.Web.UI.WebControls;
using System.Security.Permissions;

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

        public List<Jugador> getUsuarios()
        {
            List<Jugador> listaUsuarios;
            using (OTHELLOEntities db = new OTHELLOEntities())
            {
                listaUsuarios = (from d in db.Usuario
                                 select new Jugador
                                 {
                                     Id = d.idUsuario,
                                     Nombre = d.nombre,
                                     Apellido = d.apellido,
                                     NombreUsuario = d.nombreUsuario,
                                     Contraseña = d.contraseña,
                                     Fecha = d.fechaNacimiento,
                                     Pais = d.pais,
                                     Correo = d.correo
                                 }).ToList();
            }
            return listaUsuarios;
        }

        [HttpPost]
        public JsonResult usuarioRegistrado(string nombreUsuario, string correo)
        {

            return Json(disponible(nombreUsuario, correo), JsonRequestBehavior.AllowGet);

        }

        public bool disponible(string nombreUsuario, string correo)
        { 
            List<Jugador> users = getUsuarios();

            foreach (var user in users)
            {
                if (user.NombreUsuario == nombreUsuario | user.Correo == correo)
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
                if (ModelState.IsValid & disponible(modelo.Nombre, modelo.Correo) & modelo.Confirmar == modelo.Contraseña)
                {
                    using (OTHELLOEntities db = new OTHELLOEntities())
                    {
                        var usuario = new Usuario
                        {
                            nombre = modelo.Nombre,
                            apellido = modelo.Apellido,
                            nombreUsuario = modelo.NombreUsuario,
                            contraseña = modelo.Contraseña,
                            fechaNacimiento = modelo.Fecha,
                            pais = modelo.Pais,
                            correo = modelo.Correo
                        };

                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Principal", "Menu");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    [HttpPost]
        public ActionResult Inicio_sesion(inicioSesionViewModel modelo)
        {
            //return Redirect("~/Menu/Principal");
            List<Jugador> listaUsuarios = getUsuarios();
            foreach (var user in listaUsuarios)
            {
                if (modelo.NombreUsuario == user.NombreUsuario & modelo.Contraseña == user.Contraseña)
                {
                    return RedirectToAction("Principal", "Menu", user);
                }

            }

            return View();
        }
    }
}