using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1_IPC2.Models
{
    public class Maquina
    {
        private int fila;
        private int columna;
        private int color;
        private int maxCount;

        public Maquina(int color)
        {
            fila = 0;
            columna = 0;
            Color = color;
            maxCount = 0;
        }

        public int Color { get { return color; } set { color = value; } }
        public int Fila { get { return fila; }}
        public int Columna { get { return columna; } }

        public void ponerFicha(Tablero mesa)
        {
            maxCount = 0;
            int counter;
            bool arriba;
            bool abajo;
            bool izquierda;
            bool derecha;
            for (int i = 0; i < mesa.Filas; i++)
            {
                for(int j = 0; j < mesa.Columnas; j++)
                {
                    arriba = true;
                    abajo = true;
                    izquierda = true;
                    derecha = true;
                    if (mesa.Cuadricula[i,j].Estado)
                    {
                        if (i == 0)
                        {
                            arriba = false;
                        }
                        if (i == mesa.Filas-1)
                        {
                            abajo = false;
                        }
                        if (j == 0)
                        {
                            izquierda = false;
                        }
                        if (j == mesa.Columnas-1)
                        {
                            derecha = false;
                        }
                        counter = contar(mesa, arriba, abajo, izquierda, derecha, i, j);
                        if(counter > maxCount)
                        {
                            maxCount = counter;
                            fila = i;
                            columna = j;
                        }else if(counter == maxCount)
                        {
                            var rand = new Random();
                            if (rand.Next(0, 2) == 1)
                            {
                                maxCount = counter;
                                fila = i;
                                columna = j;
                            }
                        }
                    }
                }
            }
        }

        public int contar(Tablero mesa, bool ar, bool ab, bool izq, bool der, int x, int y)
        {
            int counter = 0;
            if (ar)
            {
                if (mesa.enableAr(x, y, Color)){
                    counter += contarAr(mesa, x, y);
                }
                if (der)
                {
                    if (mesa.enableArDer(x, y, Color))
                    {
                        counter += contarArDer(mesa, x, y);
                    }
                }
                if (izq)
                {
                    if (mesa.enableArIzq(x, y, Color))
                    {
                        counter += contarArIzq(mesa, x, y);
                    }
                }
            }
            if (ab)
            {
                if (mesa.enableAb(x, y, Color)){
                    counter += contarAb(mesa, x, y);
                }
                if (der)
                {
                    if (mesa.enableAbDer(x, y, Color))
                    {
                        counter += contarAbDer(mesa, x, y);
                    }
                }
                if (izq)
                {
                    if (mesa.enableAbIzq(x, y, Color))
                    {
                        counter += contarAbIzq(mesa, x, y);
                    }
                }
            }
            if (der)
            {
                if (mesa.enableDer(x, y, Color))
                {
                    counter += contarDer(mesa, x, y);
                }
            }
            if (izq)
            {
                if (mesa.enableIzq(x, y, Color))
                {
                    counter += contarIzq(mesa, x, y);
                }
            }
            return counter;
        }

        public int contarAr(Tablero mesa, int x, int y)
        {
            int counter = 0;
            int i = x - 1;
            while (i >= 0)
            {
                if (mesa.Cuadricula[i, y].Color == color) { break; }
                counter++;
                i--;
            }
            return counter;
        }

        public int contarAb(Tablero mesa, int x, int y)
        {
            int counter = 0;
            int i = x + 1;
            while (i < 8)
            {
                if (mesa.Cuadricula[i, y].Color == color) { break; }
                counter++;
                i++;
            }
            return counter;
        }

        public int contarIzq(Tablero mesa, int x, int y)
        {
            int counter = 0;
            int i = y - 1;
            while (i >= 0)
            {
                if (mesa.Cuadricula[x, i].Color == color) { break; }
                counter++;
                i--;
            }
            return counter;
        }

        public int contarDer(Tablero mesa, int x, int y)
        {
            int counter = 0;
            int i = y + 1;
            while (i < 8)
            {
                if (mesa.Cuadricula[x, i].Color == color) { break; }
                counter++;
                i++;
            }
            return counter;
        }

        public int contarArIzq(Tablero mesa, int x, int y)
        {
            int counter = 0;
            int i = 1;
            while (x - i >= 0 & y - i >= 0)
            {
                if (mesa.Cuadricula[x - i, y - i].Color == color) { break; }
                counter++;
                i++;
            }
            return counter;
        }

        public int contarAbIzq(Tablero mesa, int x, int y)
        {
            int counter = 0;
            int i = 1;
            while (i < 8 - x & y + i >= 0)
            {
                if (mesa.Cuadricula[x + i, y - i].Color == color) { break; }
                counter++;
                i++;
            }
            return counter;
        }

        public int contarArDer(Tablero mesa, int x, int y)
        {
            int counter = 0;
            int i = 1;
            while (x - i >= 0 & i < 8 - y)
            {
                if (mesa.Cuadricula[x - i, y + i].Color == color) { break; }
                counter++;
                i++;
            }
            return counter;
        }

        public int contarAbDer(Tablero mesa, int x, int y)
        {
            int counter = 0;
            int i = 1;
            while (i < 8 - x & i < 8 - y)
            {
                if (mesa.Cuadricula[x + i, y + i].Color == color) { break; }
                counter++;
                i++;
            }
            return counter;
        }
    }
}