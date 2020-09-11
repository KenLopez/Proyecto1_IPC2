using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class addUsuarioViewModel
    {
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Required]
        public string nombreUsuario { get; set; }
        [Required]
        public string contraseña { get; set; }
        [Required]
        public string confirmar { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public string pais { get; set; }
        [Required]
        public string correo { get; set; }
    }

    public enum pais
    {
        GUA,
        USA,
        SLV,

    }
}