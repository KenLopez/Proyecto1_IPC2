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

        public void enableSpaces(bool arriba, bool abajo, bool derecha, bool izquierda, int fila, int columnna, int color)
        {
            int validaciones = 0;
            if(cuadricula[fila,columnna].co)
        }

        public bool enableArriba(int fila, int columna)
        {

        }
    }
}