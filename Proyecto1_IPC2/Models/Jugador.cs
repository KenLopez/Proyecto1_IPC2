using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models
{
    public class Jugador
    {
        private int id;
        private string nombre;
        private string apellido;
        private string nombreUsuario;
        private string contraseña;
        private DateTime fecha;
        private string pais;
        private string correo;

        public Jugador()
        {

        }

        public int Id { get { return id; } set { id = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Apellido { get { return apellido; } set { apellido = value; } }
        public string NombreUsuario { get { return nombreUsuario; } set { nombreUsuario = value; } }
        public string Contraseña { get { return contraseña; } set { contraseña = value; } }
        public DateTime Fecha { get { return fecha; } set { fecha = value; } }
        public string Pais { get { return pais; } set { pais = value; } }
        public string Correo { get { return correo; } set { correo = value; } }
    }
}