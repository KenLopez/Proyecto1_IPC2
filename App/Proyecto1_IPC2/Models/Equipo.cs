using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models
{
    public class Equipo
    {
        private Jugador[] integrantes;
        private string nombre;
        private int puntos;

        public Equipo()
        {
            integrantes = new Jugador[3];
            integrantes[0] = new Jugador();
            integrantes[1] = new Jugador();
            integrantes[2] = new Jugador();
            nombre = "";
            puntos = 0;
        }

        public Jugador[] Integrantes { get { return integrantes; } set { integrantes = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public int Puntos { get { return puntos; } set { puntos = value; } }
    }
}