using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class Torneo
    {
        private usuarioViewModel creador;
        private int id;
        private string nombre;
        private Equipo[] equipos;
        private Equipo[] octavos;
        private Equipo[] cuartos;
        private Equipo[] semi;
        private Equipo[] final;
        private Equipo ganador;
        private int rondaActual;
        
        public Torneo(usuarioViewModel creador, string nombre, int cantidad)
        {
            this.creador = creador;
            this.nombre = nombre;
            id = 0;
            equipos = new Equipo[cantidad];
            octavos = new Equipo[16];
            cuartos = new Equipo[8];
            semi = new Equipo[4];
            final = new Equipo[2];
            ganador = new Equipo();
            rondaActual = 0;
        }

        public usuarioViewModel Creador { get { return creador; } set { creador = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public Equipo[] Equipos { get { return equipos; } set { equipos = value; } }
        public Equipo[] Octavos { get { return octavos; } set { octavos = value; } }
        public Equipo[] Cuartos { get { return cuartos; } set { cuartos = value; } }
        public Equipo[] Semi { get { return semi; } set { semi = value; } }
        public Equipo[] Final { get { return final; } set { final = value; } }
        public Equipo Ganador { get { return ganador; } set { ganador = value; } }
        public int Id { get { return id; } set { id = value; } }
    }
}