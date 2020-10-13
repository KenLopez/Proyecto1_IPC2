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

        public List<User> getUsuarios()
        {
            List<User> listaUsuarios;
            using (OTHELLOEntities db = new OTHELLOEntities())
            {
                listaUsuarios = (from d in db.Usuario
                                 select new User
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
            List<User> users = getUsuarios();

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
                    List<User> listaUsuarios = getUsuarios();
                    var user = new User();
                    user = listaUsuarios.Find(x => x.NombreUsuario.Equals(modelo.NombreUsuario));
                    var model = new usuarioViewModel();
                    model.NombreUsuario = user.NombreUsuario;
                    model.Id = user.Id;
                    return RedirectToAction("Principal", "Menu", model);
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
            List<User> listaUsuarios = getUsuarios();
            foreach (var user in listaUsuarios)
            {
                if (modelo.NombreUsuario == user.NombreUsuario & modelo.Contraseña == user.Contraseña)
                {
                    usuarioViewModel usuario = new usuarioViewModel();
                    usuario.NombreUsuario = user.NombreUsuario;
                    usuario.Id = user.Id;
                    return RedirectToAction("Principal", "Menu", usuario);
                }

            }

            return View();
        }
    }
}