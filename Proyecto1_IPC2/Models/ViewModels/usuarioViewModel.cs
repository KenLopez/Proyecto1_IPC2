using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class usuarioViewModel
    {
        private int id;
        private string nombreUsuario;

        public int Id { get { return id; } set { id = value; } }
        public string NombreUsuario { get { return nombreUsuario; } set { nombreUsuario = value; } }
    }
}