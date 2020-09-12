using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class getUsuarioViewModel
    {
        [Required  (ErrorMessage ="*Ingresa tu nombre de Usuario")]
        public string nombreUsuario { get; set; }
        [Required (ErrorMessage = "*Ingresa tu contraseña")]
        public string contraseña { get; set; }
        public string correo { get; set; }
    }
}