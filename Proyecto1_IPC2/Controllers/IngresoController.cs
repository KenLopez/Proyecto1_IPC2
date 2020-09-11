using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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
                                      contraseña = d.contraseña
                                  }).ToList();
            }
            return listaUsuarios;
        }

        [HttpPost]
        public ActionResult Registro(addUsuarioViewModel modelo)
        {
            try
            {
                List<getUsuarioViewModel> listaUsuarios = getUsuarios();
                foreach(var user in listaUsuarios)
                {
                    if (modelo.nombreUsuario == user.nombreUsuario)
                    {
                        return View(modelo);
                    }
                    else if(modelo.correo == user.correo)
                    {
                        return View(modelo);
                    }
                }
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