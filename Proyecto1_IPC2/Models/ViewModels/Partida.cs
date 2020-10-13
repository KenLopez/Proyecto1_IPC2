using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class Partida
    {
        private Jugador p1 = new Jugador();
        private Jugador p2 = new Jugador();
        private Maquina maquina = new Maquina(0);
        private int turnos;
        private int turno;
        private Tablero mesa = new Tablero();
        private int state;
        private Jugador winner = new Jugador();
        private int type;

        public Jugador P1 { get { return p1; } set { p1 = value; } }
        public Jugador P2 { get { return p2; } set { p2 = value; } }
        public int Turnos { get { return turnos; } set { turnos = value; } }
        public int Turno { get { return turno; } set { turno = value; } }
        public Tablero Mesa { get { return mesa; } set { mesa = value; } }
        public int IsPlaying { get { return state; } set { state = value; } }
        public Jugador Winner { get { return winner; } }
        public Maquina Maquina { get { return maquina; } set { maquina = value; } }

        public int Type { get { return type; } set { type = value; } }

        public string toXml()
        {
            string data = "<tablero>\n";
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (mesa.Cuadricula[i, j].Color != 0)
                    {
                        data += "<ficha>\n<color>" + mesa.Cuadricula[i, j].getColor() + "</color>\n<columna>" + mesa.intToLetra(j) + 
                            "</columna>\n" + "<fila>" + (i+1).ToString() + "</fila>\n</ficha>\n";
                    } 
                }
            }
            data += "<siguienteTiro>\n<color>";
            if(turno == 1)
            {
                data += "blanco";
            }
            else
            {
                data += "negro";
            }
            data += "</color>\n</siguienteTiro>\n</tablero>";
            return data;
        }

        public void disableAll()
        {
            for(int i = 0; i<8; i++)
            {
                for(int j = 0; j<8; j++)
                {
                    mesa.Cuadricula[i, j].Estado = false;
                }
            }
        }

        public void playMaquina()
        {
            disableAll();
            enableSpaces();
            maquina.ponerFicha(mesa);
            play(maquina.Fila, maquina.Columna, maquina.Color);
        }

        public void play(int fila, int columna, int color)
        {
            bool arriba = true;
            bool abajo = true;
            bool izquierda = true;
            bool derecha = true;
            if (fila == 0)
            {
                arriba = false;
            }
            if (fila == 7)
            {
                abajo = false;
            }
            if (columna == 0)
            {
                izquierda = false;
            }
            if (columna == 7)
            {
                derecha = false;
            }
            mesa.ponerFicha(arriba, abajo, derecha, izquierda, fila, columna, color);
            if (turno == p1.Color)
            {
                p1.Movimientos++;
            }
            else
            {
                p2.Movimientos++;
            }
            if (turno == 1) {turno = 2;}
            else if(turno == 2) { turno = 1; }
            turnos++;

        }

        public int contarFichas(Jugador color)
        {
            int contador=0;
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(mesa.Cuadricula[i,j].Color == color.Color)
                    {
                        contador++;
                    }
                }
            }
            return contador;
        }

        public string getGanador()
        {
            int contadorP1 = contarFichas(P1);
            int contadorP2 = contarFichas(P2);
            if (contadorP1 > contadorP2)
            {
                winner = p1;
                if(type == 1)
                {
                    return "¡VICTORIA!";
                }
                else
                {
                    return "¡EL GANADOR ES " + p1.NombreUsuario + "!";
                }
            }else if (contadorP2 > contadorP1)
            {
                winner = p2;
                if (type == 1)
                {
                    return "¡DERROTA!";
                }
                else
                {
                    return "¡EL GANADOR ES " + p2.NombreUsuario + "!";
                }
            }
            else
            {
                winner = null;
                return "¡TENEMOS UN EMPATE!";
            }
        }

        public void enableSpaces()
        {
            disableAll();
            int play = 0;
            int color = turno;
            bool arriba;
            bool abajo;
            bool izquierda;
            bool derecha;
            if(p1.Color == color)
            {
                p1.Playable = true;
            }
            else
            {
                p2.Playable = true;
            }
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    arriba = true;
                    abajo = true;
                    izquierda = true;
                    derecha = true;
                    if (mesa.Cuadricula[i, j].Color == 0)
                    {
                        if (i == 0)
                        {
                            arriba = false;
                        }
                        if (i == 7)
                        {
                            abajo = false;
                        }
                        if (j == 0)
                        {
                            izquierda = false;
                        }
                        if (j == 7)
                        {
                            derecha = false;
                        }
                        if(mesa.enableSpaces(arriba, abajo, derecha, izquierda, i, j, color))
                        {
                            mesa.Cuadricula[i, j].Estado = true;
                            play++;
                        }
                        
                    }
                    else
                    {
                        mesa.Cuadricula[i, j].Estado = false;
                    }
                }
            }
            if (play == 0)
            {
                if(P1.Color == color)
                {
                    p1.Playable = false;
                }
                else
                {
                    p2.Playable = false;
                }
            }
        }
    }
}