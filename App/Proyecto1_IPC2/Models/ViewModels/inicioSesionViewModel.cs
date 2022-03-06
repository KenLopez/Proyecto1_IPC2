using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class inicioSesionViewModel
    {
        private string nombreUsuario;
        private string contraseña;

        [Required(ErrorMessage = "*Ingresa tu nombre de Usuario")]
        public string NombreUsuario { get { return nombreUsuario; } set { nombreUsuario = value; } }

        [Required(ErrorMessage = "*Ingresa tu contraseña")]
        public string Contraseña { get { return contraseña; } set { contraseña = value; } }
    }
}