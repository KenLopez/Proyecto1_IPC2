using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models
{
    public class Jugador : User
    {
        private int punteo;
        private int color;
        private bool playable;
        private int movimientos;

        public bool Playable { get { return playable; } set { playable = value; } }
        public int Punteo { get { return punteo; } set { punteo = value; } }
        public int Color { get { return color; } set { color = value; } }

        public int Movimientos { get { return movimientos; } set { movimientos = value; } }

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
    }
}