using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1_IPC2.Models
{
    public class Tablero
    {
        private Casilla[,] cuadricula;
        private int filas;
        private int columnas;

        public Tablero(int filas = 8, int columnas = 8)
        {
            this.filas = filas;
            this.columnas = columnas;
            cuadricula = new Casilla[filas, columnas];
            for (int i = 0; i<filas; i++)
            {
                for(int j = 0; j<columnas; j++)
                {
                    cuadricula[i, j] = new Casilla();
                }
            }
        }

        public Casilla[,] Cuadricula { get { return cuadricula; } set { cuadricula = value; } }
        public int Filas { get { return filas; } set { filas = value; } }
        public int Columnas { get { return columnas; } set { columnas = value; } }


        public void dimensionTablero()
        {
            cuadricula = new Casilla[filas, columnas];
            for(int i = 0; i<filas; i++)
            {
                for(int j=0; j<columnas; j++)
                {
                    cuadricula[i, j] = new Casilla();
                }
            }
        }

        public int colorToInt(string color)
        {
            switch (color)
            {
                case "blanco":
                    return 1;
                case "negro":
                    return 2;
                default:
                    return 0;
            }
        }

        public string intToLetra(int num)
        {
            switch (num)
            {
                case 0:
                    return "A";
                case 1:
                    return "B";
                case 2:
                    return "C";
                case 3:
                    return "D";
                case 4:
                    return "E";
                case 5:
                    return "F";
                case 6:
                    return "G";
                case 7:
                    return "H";
                case 8:
                    return "J";
                case 9:
                    return "K";
                case 10:
                    return "L";
                case 11:
                    return "M";
                case 12:
                    return "N";
                case 13:
                    return "O";
                case 14:
                    return "P";
                case 15:
                    return "Q";
                case 16:
                    return "R";
                case 17:
                    return "S";
                case 18:
                    return "T";
                case 19:
                    return "U";
                case 20:
                    return "V";
                default:
                    return "";
            }
        }

        public int letraToInt(string letra)
        {
            switch (letra)
            {
                case "A":
                    return 0;
                case "B":
                    return 1;
                case "C":
                    return 2;
                case "D":
                    return 3;
                case "E":
                    return 4;
                case "F":
                    return 5;
                case "G":
                    return 6;
                case "H":
                    return 7;
                default:
                    return 8;
            }
        }

        public void ponerFicha(bool arriba, bool abajo, bool derecha, bool izquierda, int fila, int columna, int color)
        {
            cuadricula[fila, columna].Color = color;
            if (arriba)
            {
                if (enableAr(fila, columna, color))
                {
                    cuadricula[fila, columna].Color = color;
                    int i = fila - 1;
                    while (i >= 0)
                    {
                        if(cuadricula[i, columna].Color == color) { break; }
                        cuadricula[i, columna].Color = color;
                        i--;
                    }
                }
                if (derecha)
                {
                    if(enableArDer(fila, columna, color))
                    {
                        cuadricula[fila, columna].Color = color;
                        int i = 1;
                        while(fila-i>=0 & i < filas - columna)
                        {
                            if(cuadricula[fila - i, columna + i].Color == color) { break; }
                            cuadricula[fila - i, columna + i].Color = color;
                            i++;
                        }
                    }
                }
                if (izquierda)
                {
                    if (enableArIzq(fila, columna, color))
                    {
                        cuadricula[fila, columna].Color = color;
                        int i = 1;
                        while (fila-i>=0 & columna-i>=0)
                        {
                            if (cuadricula[fila - i, columna - i].Color == color) { break; }
                            cuadricula[fila - i, columna - i].Color = color;
                            i++;
                        }
                    }
                }
            }
            if (abajo)
            {
                if (enableAb(fila, columna, color))
                {
                    cuadricula[fila, columna].Color = color;
                    int i = fila + 1;
                    while (i < filas)
                    {
                        if (cuadricula[i, columna].Color == color) { break; }
                        cuadricula[i, columna].Color = color;
                        i++;
                    }
                }
                if (derecha)
                {
                    if (enableAbDer(fila, columna, color))
                    {
                        cuadricula[fila, columna].Color = color;
                        int i = 1;
                        while (i<filas-fila & i<columnas-columna)
                        {
                            if (cuadricula[fila + i, columna + i].Color == color) { break; }
                            cuadricula[fila + i, columna + i].Color = color;
                            i++;
                        }
                    }
                }
                if (izquierda)
                {
                    if (enableAbIzq(fila, columna, color))
                    {
                        cuadricula[fila, columna].Color = color;
                        int i = 1;
                        while (i < filas - fila & columna+i>=0)
                        {
                            if (cuadricula[fila + i, columna - i].Color == color) { break; }
                            cuadricula[fila + i, columna - i].Color = color;
                            i++;
                        }
                    }
                }
            }
            if (derecha)
            {
                if (enableDer(fila, columna, color))
                {
                    cuadricula[fila, columna].Color = color;
                    int i = columna + 1;
                    while (i < columnas)
                    {
                        if (cuadricula[fila, i].Color == color) { break; }
                        cuadricula[fila, i].Color = color;
                        i++;
                    }
                }
            }
            if (izquierda)
            {
                if (enableIzq(fila, columna, color))
                {
                    cuadricula[fila, columna].Color = color;
                    int i = columna - 1;
                    while (i >= 0)
                    {
                        if (cuadricula[fila, i].Color == color) { break; }
                        cuadricula[fila, i].Color = color;
                        i--;
                    }
                }
            }

        }

        public bool enableSpaces(bool arriba, bool abajo, bool derecha, bool izquierda, int fila, int columna, int color)
        {
            int validaciones = 0;
            if (arriba)
            {
                if (enableAr(fila, columna, color))
                {
                    validaciones++;
                }
                if (derecha)
                {
                    if (enableArDer(fila, columna, color))
                    {
                        validaciones++;
                    }
                }
                if (izquierda)
                {
                    if(enableArIzq(fila, columna, color))
                    {
                        validaciones++;
                    }
                }
            }
            if (abajo)
            {
                if (enableAb(fila, columna, color))
                {
                    validaciones++;
                }
                if (derecha)
                {
                    if (enableAbDer(fila, columna, color))
                    {
                        validaciones++;
                    }
                }
                if (izquierda)
                {
                    if (enableAbIzq(fila, columna, color))
                    {
                        validaciones++;
                    }
                }
            }
            if (izquierda)
            {
                if(enableIzq(fila, columna, color))
                {
                    validaciones++;
                }
            }
            if (derecha)
            {
                if (enableDer(fila, columna, color))
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

        public bool enableAr(int fila, int columna, int color)
        {
            if(cuadricula[fila-1,columna].Color == (color | 0))
            {
                return false;
            }
            else
            {
                for(int i = fila-1; i>=0; i--)
                {
                    if(cuadricula[i,columna].Color == 0)
                    {
                        return false;
                    }
                    if(cuadricula[i,columna].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableAb(int fila, int columna, int color)
        {
            if (cuadricula[fila + 1, columna].Color == (color | 0))
            {
                return false;
            }
            else
            {
                for (int i = fila + 1; i<filas ; i++)
                {
                    if (cuadricula[i, columna].Color == 0)
                    {
                        return false;
                    }
                    if (cuadricula[i, columna].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableIzq(int fila, int columna, int color)
        {
            if (cuadricula[fila, columna - 1].Color == (color | 0))
            {
                return false;
            }
            else
            {
                for (int i = columna - 1; i >= 0; i--)
                {
                    if (cuadricula[fila, i].Color == 0)
                    {
                        return false;
                    }
                    if (cuadricula[fila, i].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableDer(int fila, int columna, int color)
        {
            if (cuadricula[fila, columna + 1].Color == (color | 0))
            {
                return false;
            }
            else
            {
                for (int i = columna + 1; i<columnas; i++)
                {
                    if (cuadricula[fila, i].Color == 0)
                    {
                        return false;
                    }
                    if (cuadricula[fila, i].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableArIzq(int fila, int columna, int color)
        {
            if (cuadricula[fila - 1, columna - 1].Color == (color | 0))
            {
                return false;
            }
            else
            {
                for (int i = 1; fila-i>=0 & columna-i>=0; i++)
                {
                    if (cuadricula[fila - i, columna - i].Color == 0)
                    {
                        return false;
                    }
                    if (cuadricula[fila - i, columna - i].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableArDer(int fila, int columna, int color)
        {
            if (cuadricula[fila - 1, columna + 1].Color == (color | 0))
            {
                return false;
            }
            else
            {
                for (int i = 1; fila-i>=0 & i < columnas - columna; i++)
                {
                    if (cuadricula[fila - i, columna + i].Color == 0)
                    {
                        return false;
                    }
                    if (cuadricula[fila - i, columna + i].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableAbIzq(int fila, int columna, int color)
        {
            if (cuadricula[fila + 1, columna - 1].Color == (color | 0))
            {
                return false;
            }
            else
            {
                for (int i = 1; i < filas - fila & columna-i>=0; i++)
                {
                    if (cuadricula[fila + i, columna - i].Color == 0)
                    {
                        return false;
                    }
                    if (cuadricula[fila + i, columna - i].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool enableAbDer(int fila, int columna, int color)
        {
            if (cuadricula[fila + 1, columna + 1].Color == (color | 0))
            {
                return false;
            }
            else
            {
                for (int i = 1; i < filas - fila & i < columnas - columna; i++)
                {
                    if (cuadricula[fila + i, columna + i].Color == 0)
                    {
                        return false;
                    }
                    if (cuadricula[fila + i, columna + i].Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool validar()
        {
            if(cuadricula[3,3].Color == 0 | cuadricula[3,4].Color == 0 | cuadricula[4,3].Color == 0 | cuadricula[4,4].Color == 0)
            {
                return false;
            }
            int validaciones;
            for(int i = 0; i < filas; i++)
            {
                for(int j = 0; j < columnas; j++)
                {
                    validaciones = 0;
                    if(cuadricula[i,j].Color != 0)
                    {
                        if (i > 0)
                        {
                            if (cuadricula[i - 1, j].Color != 0)
                            {
                                validaciones++;
                            }
                            if (j > 0)
                            {
                                if(cuadricula[i-1,j-1].Color != 0)
                                {
                                    validaciones++;
                                }
                            }
                            if (j < columnas-1)
                            {
                                if(cuadricula[i-1,j+1].Color != 0)
                                {
                                    validaciones++;
                                }
                            }
                        }
                        if (i < filas-1)
                        {
                            if (cuadricula[i + 1, j].Color != 0)
                            {
                                validaciones++;
                            }
                            if (j > 0)
                            {
                                if (cuadricula[i + 1, j - 1].Color != 0)
                                {
                                    validaciones++;
                                }
                            }
                            if (j < columnas-1)
                            {
                                if (cuadricula[i + 1, j + 1].Color != 0)
                                {
                                    validaciones++;
                                }
                            }
                        }
                        if (j > 0)
                        {
                            if(cuadricula[i,j-1].Color != 0)
                            {
                                validaciones++;
                            }
                        }
                        if (j < columnas-1)
                        {
                            if(cuadricula[i,j+1].Color != 0)
                            {
                                validaciones++;
                            }
                        }
                        if (validaciones == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}