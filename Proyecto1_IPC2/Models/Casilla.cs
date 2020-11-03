using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models
{
    public class Casilla
    {
        private bool estado;
        private int color;

        public Casilla()
        {
            estado = false;
            color = 0;
        }

        public bool Estado { get { return estado; } set { estado = value; } }
        public int Color { get { return color; } set { color = value; } }

        public string getColor()
        {
            switch (color)
            {
                case 1:
                    return "blanco";
                case 2:
                    return "negro";
                case 3:
                    return "azul";
                case 4:
                    return "rojo";
                case 5:
                    return "cafe";
                case 6:
                    return "verde";
                case 7:
                    return "naranja";
                case 8:
                    return "amarillo";
                case 9:
                    return "rosado";
                case 10:
                    return "morado";
                default:
                    return "casilla";
            }
        }

        public string getState()
        {
            if (estado)
            {
                return "activo";
            }
            else
            {
                return "";
            }
        }
    }
}