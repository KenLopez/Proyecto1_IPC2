using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Stopwatch cronometro;

        public Jugador()
        {
            cronometro = new Stopwatch();
        }

        public bool Playable { get { return playable; } set { playable = value; } }
        public int Punteo { get { return punteo; } set { punteo = value; } }
        public int Color { get { return color; } set { color = value; } }
        public int Movimientos { get { return movimientos; } set { movimientos = value; } }
        public Stopwatch Cronometro { get { return cronometro; } }

        public string getTiempo()
        {
            TimeSpan tiempo = cronometro.Elapsed;
            string cadena = String.Format("{0:00}:{1:00}:{2:00}",
            tiempo.Hours, tiempo.Minutes, tiempo.Seconds);
            return cadena;
        }

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