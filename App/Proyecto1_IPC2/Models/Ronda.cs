using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models
{
    public class Ronda
    {
        private Equipo equipo1;
        private Equipo equipo2;
        private List<Encuentro> encuentros;
        private Equipo ganador;
        private int actual;
        private int estado;

        public Ronda()
        {
            equipo1 = new Equipo();
            equipo2 = new Equipo();
            encuentros = new List<Encuentro>();
            actual = 0;
            estado = 0;
        }

        public Equipo Equipo1 { get { return equipo1; } set { equipo1 = value; } }
        public Equipo Equipo2 { get { return equipo2; } set { equipo2 = value; } }
        public List<Encuentro> Encuentros { get { return encuentros; } set { encuentros = value; } }
        public Equipo Ganador { get { return ganador; } set { ganador = value; } }
        public int Estado { get { return estado; } }

        public Encuentro getEncuentroActual()
        {
            return encuentros[actual];
        }

        public void crearEncuentros()
        {
            List<Jugador> e1 = new List<Jugador>();
            List<Jugador> e2 = new List<Jugador>();

            for(int i = 0; i < 3; i++)
            {
                e1.Add(equipo1.Integrantes[i]);
                e2.Add(equipo2.Integrantes[i]);
            }
            int seleccion;
            Random random = new Random();
            for(int i = 0; i < 3; i++)
            { 
                encuentros.Add(new Encuentro());
                seleccion = random.Next(0, 3 - i);
                encuentros[i].Jugador1 = e1[seleccion];
                e1.RemoveAt(seleccion);
                seleccion = random.Next(0, 3 - i);
                encuentros[i].Jugador2 = e2[seleccion];
                e2.RemoveAt(seleccion);

            }
        }

        public void sumarPuntos(int ganador)
        {
            switch (ganador)
            {
                case 1:
                    Equipo1.Puntos += 3;
                    break;
                case 2:
                    equipo2.Puntos += 3;
                    break;
                case 3:
                    equipo1.Puntos += 1;
                    equipo2.Puntos += 1;
                    break;
            }
        }

        public void sigEncuentro()
        {
            actual++;
            if(actual > 2)
            {
                if(equipo1.Puntos == equipo2.Puntos)
                {
                    estado = 2;
                    encuentros.Add(new Encuentro());

                }else if(equipo1.Puntos > equipo2.Puntos){
                    estado = 1;
                    ganador = equipo1;
                }
                else
                {
                    estado = 1;
                    ganador = equipo2;
                }
            }
        }
    }
}