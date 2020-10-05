using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class jugadorViewModel
    {
        private Jugador jugador = new Jugador();
        private int punteo;
        private int color;

        public Jugador Jugador { get { return jugador; } set { jugador = value; } }
        public int Punteo { get { return punteo; } set { punteo = value; } }
        public int Color { get { return color; } set { color = value; } }
    }
}