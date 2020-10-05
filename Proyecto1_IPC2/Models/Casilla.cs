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