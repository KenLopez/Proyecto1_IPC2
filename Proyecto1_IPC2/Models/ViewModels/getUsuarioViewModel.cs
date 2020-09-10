using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class getUsuarioViewModel
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombreUsuario { get; set; }
        public string contraseña { get; set; }
        public string fecha { get; set; }
        public string pais { get; set; }
        public string correo { get; set; }
    }
}