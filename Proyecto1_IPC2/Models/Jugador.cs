using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models
{
    public class Jugador : User
    {
        private int punteo;
        private int color;
        private int[] colores;
        private bool playable;
        private int movimientos;
        private Stopwatch cronometro;

        public Jugador()
        {
            cronometro = new Stopwatch();
            Id = 0;
        }

        public bool Playable { get { return playable; } set { playable = value; } }
        public int Punteo { get { return punteo; } set { punteo = value; } }
        public int Color { get { return color; } set { color = value; } }
        public int Movimientos { get { return movimientos; } set { movimientos = value; } }
        public Stopwatch Cronometro { get { return cronometro; } }
        public int[] Colores { get { return colores; } set { colores = value;  } }

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

        public void sigColor()
        {
            int index = getColorIndex();
            if(index != colores.Length-1)
            {
                color = colores[index + 1];
            }
            else
            {
                color = colores[0];
            }
        }

        public bool colorExists(int color)
        {
            foreach(int elemento in colores)
            {
                if(color == elemento)
                {
                    return true;
                }
            }
            return false;
        }

        public int getColorIndex()
        {
            for(int i = 0; i<colores.Length; i++)
            {
                if(color == colores[i])
                {
                    return i;
                }
            }
            return 0;
        }
    }
}