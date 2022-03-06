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
        private string nombre;
        private string apellido;
        private string nombreUsuario;
        private string contraseña;
        private string confirmar;
        private DateTime fecha;
        private string pais;
        private string correo;

        [Required(ErrorMessage = "*Campo requerido")]
        public string Nombre { get { return nombre; } set { nombre = value; } }

        [Required(ErrorMessage = "*Campo requerido")]
        public string Apellido { get { return apellido; } set { apellido = value; } }

        [Required(ErrorMessage = "*Campo requerido")]
        [Remote("usuarioRegistrado", "Ingreso", HttpMethod = "POST", ErrorMessage = "*Nombre de Usuario ya fue registrado")]
        public string NombreUsuario { get { return nombreUsuario; } set { nombreUsuario = value; } }

        [Required(ErrorMessage = "*Campo requerido")]
        public string Contraseña { get { return contraseña; } set { contraseña = value; } }

        [Required(ErrorMessage = "*Campo requerido")]
        public string Confirmar { get { return confirmar; } set { confirmar = value; } }

        [Required(ErrorMessage = "*Campo requerido")]
        public DateTime Fecha { get { return fecha; } set { fecha = value; } }

        [Required(ErrorMessage = "*Campo requerido")]
        public string Pais { get { return pais; } set { pais = value; } }

        [Required(ErrorMessage = "*Campo requerido")]
        public string Correo { get { return correo; } set { correo = value; } }
    }

    public enum pais
    {
        GUA,
        USA,
        SLV,
        MEX,

    }
}