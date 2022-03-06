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
        private int[] orden;
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
        public int[] Orden { get { return orden; } set { orden = value; } }


        public string intToColor(int color)
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

        public string toXml()
        {
            string data = "<partida>\n";
            data += "<filas>" + mesa.Filas.ToString() + "</filas>\n";
            data += "<columnas>" + mesa.Columnas.ToString() + "</columnas>\n";
            data += "<Jugador1>\n";
            foreach (int elemento in p1.Colores)
            {
                data += "<color>" + intToColor(elemento).ToString() + "</color>\n";
            }
            data += "</Jugador1>\n";
            data += "<Jugador2>\n";
            foreach (int elemento in p2.Colores)
            {
                data += "<color>" + intToColor(elemento).ToString() + "</color>\n";
            }
            data += "</Jugador2>\n";
            data += "<Modalidad>";
            if (type == 4 | type == 6)
            {
                data += "Normal";
            }
            else
            {
                data += "Inversa";
            }
            data += "</Modalidad>\n";
            data += "<tablero>\n";
            for (int i = 0; i < mesa.Filas; i++)
            {
                for (int j = 0; j < mesa.Columnas; j++)
                {
                    if (mesa.Cuadricula[i, j].Color != 0)
                    {
                        data += "<ficha>\n<color>" + mesa.Cuadricula[i, j].getColor() + "</color>\n<columna>" + mesa.intToLetra(j) +
                            "</columna>\n" + "<fila>" + (i + 1).ToString() + "</fila>\n</ficha>\n";
                    }
                }
            }
            data += "<siguienteTiro>\n<color>";
            data += intToColor(turno).ToString();
            data += "</color>\n</siguienteTiro>\n</tablero>\n</partida>";
            return data;
        }

        public void disableAll()
        {
            for (int i = 0; i < mesa.Filas; i++)
            {
                for (int j = 0; j < mesa.Columnas; j++)
                {
                    mesa.Cuadricula[i, j].Estado = false;
                }
            }
        }

        public void cambiarTurno()
        {
            if (turno == p1.Color)
            {
                turno = p2.Color;
                p1.sigColor();
            }
            else
            {
                turno = p1.Color;
                p2.sigColor();
            }
        }

        public void playMaquina()
        {
            disableAll();
            enableSpaces();
            maquina.ponerFicha(mesa, p2.Color);
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
            if (fila == mesa.Filas - 1)
            {
                abajo = false;
            }
            if (columna == 0)
            {
                izquierda = false;
            }
            if (columna == mesa.Columnas - 1)
            {
                derecha = false;
            }
            Jugador player;
            if (p1.colorExists(turno))
            {
                player = p1;
            }
            else
            {
                player = p2;
            }
            ponerFicha(arriba, abajo, derecha, izquierda, fila, columna, color, player);
            if (p1.colorExists(turno))
            {
                p1.Movimientos++;
            }
            else
            {
                p2.Movimientos++;
            }
            cambiarTurno();
            turnos++;

        }

        public int contarFichas(Jugador colores)
        {
            int contador = 0;
            for (int i = 0; i < mesa.Filas; i++)
            {
                for (int j = 0; j < mesa.Columnas; j++)
                {
                    if (colores.colorExists(mesa.Cuadricula[i, j].Color))
                    {
                        contador++;
                    }
                }
            }
            return contador;
        }

        public int Ganador()
        {
            int contadorP1 = contarFichas(P1);
            int contadorP2 = contarFichas(P2);
            if (contadorP1 > contadorP2)
            {
                return 1;
            }
            else if(contadorP2 > contadorP1)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        public string getGanador()
        {
            int contadorP1 = contarFichas(P1);
            int contadorP2 = contarFichas(P2);
            if (contadorP1 > contadorP2)
            {
                winner = p1;
                if (type == 5 | type == 7)
                {
                    winner = p2;
                    if (type == 7)
                    {
                        return "DERROTA";
                    }
                    return "¡EL GANADOR ES " + p2.NombreUsuario + "!";
                }
                if (type == 1)
                {
                    return "¡VICTORIA!";
                }
                else
                {
                    return "¡EL GANADOR ES " + p1.NombreUsuario + "!";
                }
            }
            else if (contadorP2 > contadorP1)
            {
                winner = p2;
                if (type == 5 | type == 7)
                {
                    winner = p1;
                    if (type == 7)
                    {
                        return "VICTORIA";
                    }
                    return "¡EL GANADOR ES " + p1.NombreUsuario + "!";
                }
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

        public bool enableSpaces()
        {
            disableAll();
            int play = 0;
            int color = turno;
            bool arriba;
            bool abajo;
            bool izquierda;
            bool derecha;
            if (p1.colorExists(turno))
            {
                p1.Playable = true;
            }
            else
            {
                p2.Playable = true;
            }
            for (int i = 0; i < mesa.Filas; i++)
            {
                for (int j = 0; j < mesa.Columnas; j++)
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
                        if (i == mesa.Filas - 1)
                        {
                            abajo = false;
                        }
                        if (j == 0)
                        {
                            izquierda = false;
                        }
                        if (j == mesa.Columnas - 1)
                        {
                            derecha = false;
                        }
                        Jugador player;
                        if (p1.colorExists(turno))
                        {
                            player = p1;
                        }
                        else
                        {
                            player = p2;
                        }
                        if (enableSpaces(arriba, abajo, derecha, izquierda, i, j, color, player))
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
                return false;
            }
            return true;
        }

        public void ponerFicha(bool arriba, bool abajo, bool derecha, bool izquierda, int fila, int columna, int color, Jugador player)
        {
            mesa.Cuadricula[fila, columna].Color = color;
            if (arriba)
            {
                if (enableAr(fila, columna, color, player))
                {
                    mesa.Cuadricula[fila, columna].Color = color;
                    int i = fila - 1;
                    while (i >= 0)
                    {
                        if (player.colorExists(mesa.Cuadricula[i, columna].Color)) { break; }
                        mesa.Cuadricula[i, columna].Color = color;
                        i--;
                    }
                }
                if (derecha)
                {
                    if (enableArDer(fila, columna, color, player))
                    {
                        mesa.Cuadricula[fila, columna].Color = color;
                        int i = 1;
                        while (fila - i >= 0 & i < mesa.Filas - columna)
                        {
                            if (player.colorExists(mesa.Cuadricula[fila - i, columna + i].Color)) { break; }
                            mesa.Cuadricula[fila - i, columna + i].Color = color;
                            i++;
                        }
                    }
                }
                if (izquierda)
                {
                    if (enableArIzq(fila, columna, color, player))
                    {
                        mesa.Cuadricula[fila, columna].Color = color;
                        int i = 1;
                        while (fila - i >= 0 & columna - i >= 0)
                        {
                            if (player.colorExists(mesa.Cuadricula[fila - i, columna - i].Color)) { break; }
                            mesa.Cuadricula[fila - i, columna - i].Color = color;
                            i++;
                        }
                    }
                }
            }
            if (abajo)
            {
                if (enableAb(fila, columna, color, player))
                {
                    mesa.Cuadricula[fila, columna].Color = color;
                    int i = fila + 1;
                    while (i < mesa.Filas)
                    {
                        if (player.colorExists(mesa.Cuadricula[i, columna].Color)) { break; }
                        mesa.Cuadricula[i, columna].Color = color;
                        i++;
                    }
                }
                if (derecha)
                {
                    if (enableAbDer(fila, columna, color, player))
                    {
                        mesa.Cuadricula[fila, columna].Color = color;
                        int i = 1;
                        while (i < mesa.Filas - fila & i < mesa.Columnas - columna)
                        {
                            if (player.colorExists(mesa.Cuadricula[fila + i, columna + i].Color)) { break; }
                            mesa.Cuadricula[fila + i, columna + i].Color = color;
                            i++;
                        }
                    }
                }
                if (izquierda)
                {
                    if (enableAbIzq(fila, columna, color, player))
                    {
                        mesa.Cuadricula[fila, columna].Color = color;
                        int i = 1;
                        while (i < mesa.Filas - fila & columna + i >= 0)
                        {
                            if (player.colorExists(mesa.Cuadricula[fila + i, columna - i].Color)) { break; }
                            mesa.Cuadricula[fila + i, columna - i].Color = color;
                            i++;
                        }
                    }
                }
            }
            if (derecha)
            {
                if (enableDer(fila, columna, color, player))
                {
                    mesa.Cuadricula[fila, columna].Color = color;
                    int i = columna + 1;
                    while (i < mesa.Columnas)
                    {
                        if (player.colorExists(mesa.Cuadricula[fila, i].Color)) { break; }
                        mesa.Cuadricula[fila, i].Color = color;
                        i++;
                    }
                }
            }
            if (izquierda)
            {
                if (enableIzq(fila, columna, color, player))
                {
                    mesa.Cuadricula[fila, columna].Color = color;
                    int i = columna - 1;
                    while (i >= 0)
                    {
                        if (player.colorExists(mesa.Cuadricula[fila, i].Color)) { break; }
                        mesa.Cuadricula[fila, i].Color = color;
                        i--;
                    }
                }
            }

        }

        public bool enableSpaces(bool arriba, bool abajo, bool derecha, bool izquierda, int fila, int columna, int color, Jugador player)
        {
            int validaciones = 0;
            if (arriba)
            {
                if (enableAr(fila, columna, color, player))
                {
                    validaciones++;
                }
                if (derecha)
                {
                    if (enableArDer(fila, columna, color, player))
                    {
                        validaciones++;
                    }
                }
                if (izquierda)
                {
                    if (enableArIzq(fila, columna, color, player))
                    {
                        validaciones++;
                    }
                }
            }
            if (abajo)
            {
                if (enableAb(fila, columna, color, player))
                {
                    validaciones++;
                }
                if (derecha)
                {
                    if (enableAbDer(fila, columna, color, player))
                    {
                        validaciones++;
                    }
                }
                if (izquierda)
                {
                    if (enableAbIzq(fila, columna, color, player))
                    {
                        validaciones++;
                    }
                }
            }
            if (izquierda)
            {
                if (enableIzq(fila, columna, color, player))
                {
                    validaciones++;
                }
            }
            if (derecha)
            {
                if (enableDer(fila, columna, color, player))
                {
                    validaciones++;
                }
            }
            if (validaciones > 0)
            {
                return true;
            }
            return false;
        }

        public bool enableAr(int fila, int columna, int color, Jugador player)
        {
            if (player.colorExists(mesa.Cuadricula[fila - 1, columna].Color) | mesa.Cuadricula[fila - 1, columna].Color == 0)
            {
                return false;
            }
            else
            {
                for (int i = fila - 1; i >= 0; i--)
                {
                    if (mesa.Cuadricula[i, columna].Color == 0)
                    {
                        return false;
                    }
                    if (player.colorExists(mesa.Cuadricula[i, columna].Color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableAb(int fila, int columna, int color, Jugador player)
        {
            if (player.colorExists(mesa.Cuadricula[fila + 1, columna].Color) | mesa.Cuadricula[fila + 1, columna].Color == 0)
            {
                return false;
            }
            else if (mesa.Cuadricula[fila + 1, columna].Color == 0)
            {
                return false;
            }
            else
            {
                for (int i = fila + 1; i < mesa.Filas; i++)
                {
                    if (mesa.Cuadricula[i, columna].Color == 0)
                    {
                        return false;
                    }
                    if (player.colorExists(mesa.Cuadricula[i, columna].Color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableIzq(int fila, int columna, int color, Jugador player)
        {
            if (player.colorExists(mesa.Cuadricula[fila, columna - 1].Color) | mesa.Cuadricula[fila, columna - 1].Color == 0)
            {
                return false;
            }
            else
            {
                for (int i = columna - 1; i >= 0; i--)
                {
                    if (mesa.Cuadricula[fila, i].Color == 0)
                    {
                        return false;
                    }
                    if (player.colorExists(mesa.Cuadricula[fila, i].Color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableDer(int fila, int columna, int color, Jugador player)
        {
            if (player.colorExists(mesa.Cuadricula[fila, columna + 1].Color) | mesa.Cuadricula[fila, columna + 1].Color == 0)
            {
                return false;
            }
            else
            {
                for (int i = columna + 1; i < mesa.Columnas; i++)
                {
                    if (mesa.Cuadricula[fila, i].Color == 0)
                    {
                        return false;
                    }
                    if (player.colorExists(mesa.Cuadricula[fila, i].Color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableArIzq(int fila, int columna, int color, Jugador player)
        {
            if (player.colorExists((mesa.Cuadricula[fila - 1, columna - 1].Color)) | mesa.Cuadricula[fila - 1, columna - 1].Color == 0)
            {
                return false;
            }
            else
            {
                for (int i = 1; fila - i >= 0 & columna - i >= 0; i++)
                {
                    if (mesa.Cuadricula[fila - i, columna - i].Color == 0)
                    {
                        return false;
                    }
                    if (player.colorExists(mesa.Cuadricula[fila - i, columna - i].Color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableArDer(int fila, int columna, int color, Jugador player)
        {
            if (player.colorExists(mesa.Cuadricula[fila - 1, columna + 1].Color) | mesa.Cuadricula[fila - 1, columna + 1].Color == 0)
            {
                return false;
            }
            else
            {
                for (int i = 1; fila - i >= 0 & i < mesa.Columnas - columna; i++)
                {
                    if (mesa.Cuadricula[fila - i, columna + i].Color == 0)
                    {
                        return false;
                    }
                    if (player.colorExists(mesa.Cuadricula[fila - i, columna + i].Color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableAbIzq(int fila, int columna, int color, Jugador player)
        {
            if (player.colorExists(mesa.Cuadricula[fila + 1, columna - 1].Color) | mesa.Cuadricula[fila + 1, columna - 1].Color == 0)
            {
                return false;
            }
            else
            {
                for (int i = 1; i < mesa.Filas - fila & columna - i >= 0; i++)
                {
                    if (mesa.Cuadricula[fila + i, columna - i].Color == 0)
                    {
                        return false;
                    }
                    if (player.colorExists(mesa.Cuadricula[fila + i, columna - i].Color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableAbDer(int fila, int columna, int color, Jugador player)
        {
            if (player.colorExists(mesa.Cuadricula[fila + 1, columna + 1].Color) | mesa.Cuadricula[fila + 1, columna + 1].Color == 0)
            {
                return false;
            }
            else
            {
                for (int i = 1; i < mesa.Filas - fila & i < mesa.Columnas - columna; i++)
                {
                    if (mesa.Cuadricula[fila + i, columna + i].Color == 0)
                    {
                        return false;
                    }
                    if (player.colorExists(mesa.Cuadricula[fila + i, columna + i].Color))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}