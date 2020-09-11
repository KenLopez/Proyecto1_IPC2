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
            using (OTHELLOEntities db = new OTHELLOEntities())
            {
                listaUsuarios = (from d in db.Usuario
                                  select new getUsuarioViewModel
                                  {
                                      nombreUsuario = d.nombreUsuario,
                                      contraseña = d.contraseña
                                  }).ToList();
            }

            return View(listaUsuarios);
        }

        [HttpPost]
        public ActionResult AddUsuario(addUsuarioViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid && modelo.contraseña == modelo.confirmar)
                {
                    using (OTHELLOEntities db = new OTHELLOEntities())
                    {
                        var usuario = new Usuario();
                        usuario.nombre = modelo.nombre;
                        usuario.apellido = modelo.apellido;
                        usuario.nombreUsuario = modelo.nombreUsuario;
                        usuario.contraseña = modelo.contraseña;
                        usuario.fechaNacimiento = modelo.fecha;
                        usuario.pais = modelo.pais;
                        usuario.correo = modelo.correo;
                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                    }
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}