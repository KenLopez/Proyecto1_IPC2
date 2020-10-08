using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models
{
    public class Tablero
    {
        private Casilla[,] cuadricula;

        public Tablero()
        {
            cuadricula = new Casilla[8, 8];
            for (int i = 0; i<8; i++)
            {
                for(int j = 0; j<8; j++)
                {
                    cuadricula[i, j] = new Casilla();
                }
            }
        }

        public Casilla[,] Cuadricula { get { return cuadricula; } }

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
                        while(fila-i>=0 & i < 8 - columna)
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
                    while (i < 8)
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
                        while (i<8-fila & i<8-columna)
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
                        while (i < 8 - fila & columna+i>=0)
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
                    while (i < 8)
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
                for (int i = fila + 1; i<8 ; i++)
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
                for (int i = columna + 1; i<8; i++)
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
                for (int i = 1; fila-i>=0 & i < 8 - columna; i++)
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
                for (int i = 1; i < 8 - fila & columna-i>=0; i++)
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
                for (int i = 1; i < 8 - fila & i < 8 - columna; i++)
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


    }
}