using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class addUsuarioViewModel
    {
        [Required(ErrorMessage = "*Campo requerido")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "*Campo requerido")]
        public string apellido { get; set; }

        [Required(ErrorMessage = "*Campo requerido")]
        [Remote("usuarioRegistrado", "Ingreso", HttpMethod = "POST", ErrorMessage = "*Nombre de Usuario ya fue registrado")]
        public string nombreUsuario { get; set; }
        [Required(ErrorMessage = "*Campo requerido")]

        public string contraseña { get; set; }
        [Required(ErrorMessage = "*Campo requerido")]
        public string confirmar { get; set; }
        [Required(ErrorMessage = "*Campo requerido")]
        public DateTime fecha { get; set; }
        [Required(ErrorMessage = "*Campo requerido")]
        public string pais { get; set; }
        [Required(ErrorMessage = "*Campo requerido")]
        public string correo { get; set; }
    }

    public enum pais
    {
        GUA,
        USA,
        SLV,
        MEX,

    }
}