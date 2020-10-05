using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class partidaViewModel
    {
        private jugadorViewModel p1 = new jugadorViewModel();
        private jugadorViewModel p2 = new jugadorViewModel();
        private int turnos;
        private jugadorViewModel turno;
        private Tablero mesa = new Tablero();
        private System.Timers.Timer cronometro;

        public jugadorViewModel P1 { get { return p1; } set { p1 = value; } }
        public jugadorViewModel P2 { get { return p2; } set { p2 = value; } }
        public int Turnos { get { return turnos; } set { turnos = value; } }
        public jugadorViewModel Turno { get { return turno; } set { turno = value; } }
        public Tablero Mesa { get { return mesa; } set { mesa = value; } }
        public System.Timers.Timer Cronometro { get { return cronometro; } set { cronometro = value; } }

        public void enableSpaces(jugadorViewModel jugando)
        {
            int color = jugando.Color;
            bool arriba;
            bool abajo;
            bool izquierda;
            bool derecha;
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    arriba = true;
                    abajo = true;
                    izquierda = true;
                    derecha = true;
                    if(mesa.Cuadricula[i,j].Color != 0)
                    {
                        if(i == 0)
                        {
                            arriba = false;
                        }
                        if(i == 7)
                        {
                            abajo = false;
                        }
                        if(j == 0)
                        {
                            izquierda = false;
                        }
                        if(j == 7)
                        {
                            derecha = false;
                        }
                        mesa.enableSpaces(arriba, abajo, derecha, izquierda, i, j, color);
                    }
                    else
                    {
                        mesa.Cuadricula[i, j].Estado = false;
                    }
                }
            }
        }
    }
}