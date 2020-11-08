using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Web;

namespace Proyecto1_IPC2.Models.ViewModels
{
    public class Torneo
    {
        private usuarioViewModel creador;
        private int id;
        private string nombre;
        private Equipo[] equipos;
        private Ronda[] octavos;
        private Ronda[] cuartos;
        private Ronda[] semi;
        private Ronda final;
        private Equipo ganador;
        private int rondaActual;
        private int fase;
        
        public Torneo(usuarioViewModel creador, string nombre, int cantidad)
        {
            this.creador = creador;
            this.nombre = nombre;
            id = 0;
            equipos = new Equipo[cantidad];
            octavos = new Ronda[8];
            cuartos = new Ronda[4];
            semi = new Ronda[2];
            final = new Ronda();
            rondaActual = 0;
            for(int i = 0; i < 8; i++)
            {
                octavos[i] = new Ronda();
                if (i < 4) { cuartos[i] = new Ronda(); }
                if(i < 2) { semi[i] = new Ronda(); }
                if(i == 0) { final = new Ronda(); }
            }
            switch (cantidad)
            {
                case 4:
                    fase = 2;
                    break;
                case 8:
                    fase = 3;
                    break;
                case 16:
                    fase = 4;
                    break;
            }
        }

        public usuarioViewModel Creador { get { return creador; } set { creador = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public Equipo[] Equipos { get { return equipos; } set { equipos = value; } }
        public Ronda[] Octavos { get { return octavos; } set { octavos = value; } }
        public Ronda[] Cuartos { get { return cuartos; } set { cuartos = value; } }
        public Ronda[] Semi { get { return semi; } set { semi = value; } }
        public Ronda Final { get { return final; } set { final = value; } }
        public Equipo Ganador { get { return ganador; } set { ganador = value; } }
        public int Id { get { return id; } set { id = value; } }

        public void armarEquipos(string[] teams, string[] usernames)
        {
            int counter = 0;
            for(int i = 0; i < equipos.Length; i++)
            {
                equipos[i] = new Equipo();
                equipos[i].Nombre = teams[i];
                for(int j = 0; j<3; j++)
                {
                    equipos[i].Integrantes[j].NombreUsuario = usernames[counter];
                    counter++;
                }
            }
            armarRondas(fase);
            switch (equipos.Length)
            {
                case 4:
                    fase = 2;
                    break;
                case 8:
                    fase = 3;
                    break;
                case 16:
                    fase = 4;
                    break;
            }
        }

        public void armarRondas(int fase)
        {
            int i = 0;
            int j = 0;
            switch (fase)
            {
                case 2:
                    while (i < 2)
                    {
                        semi[i].Equipo1 = equipos[j];
                        semi[i].Equipo2 = equipos[j + 1];
                        i++;
                        j += 2;
                    }
                    break;
                case 3:
                    while (i < 4)
                    {
                        cuartos[i].Equipo1 = equipos[j];
                        cuartos[i].Equipo2 = equipos[j + 1];
                        i++;
                        j += 2;
                    }
                    break;
                case 4:
                    while (i < 8)
                    {
                        octavos[i].Equipo1 = equipos[j];
                        octavos[i].Equipo2 = equipos[j + 1];
                        i++;
                        j += 2;
                    }
                    break;
            }
        }

        public Ronda getRondaActual()
        {
            switch (fase)
            {
                case 1:
                    return final;
                case 2:
                    return semi[rondaActual];
                case 3:
                    return cuartos[rondaActual];
                case 4:
                    return octavos[rondaActual];
                default:
                    return new Ronda();
            }
        }

        public Ronda getRondaSig()
        {
            switch (fase)
            {
                case 1:
                    return final;
                case 2:
                    return semi[rondaActual];
                case 3:
                    return cuartos[rondaActual];
                case 4:
                    return octavos[rondaActual];
                default:
                    return new Ronda();
            }
        }

        public Ronda finalFase()
        {
            switch (fase)
            {
                case 1:
                    return final;
                case 2:
                    return semi[1];
                case 3:
                    return cuartos[3];
                case 4:
                    return octavos[7];
                default:
                    return new Ronda();
            }
        }

        public void sigEncuentro()
        {
            getRondaActual().sigEncuentro();
            if(getRondaActual().Estado == 1)
            {
                switch (fase)
                {
                    case 2:
                        if (final.Equipo1.Nombre == "")
                        {
                            final.Equipo1.Nombre = getRondaActual().Ganador.Nombre;
                            final.Equipo1.Integrantes = getRondaActual().Ganador.Integrantes;
                            final.Equipo1.Puntos = 0;
                        }
                        else
                        {
                            final.Equipo2.Nombre = getRondaActual().Ganador.Nombre;
                            final.Equipo2.Integrantes = getRondaActual().Ganador.Integrantes;
                            final.Equipo2.Puntos = 0;
                        }
                        break;
                    case 3:
                        if (semi[rondaActual / 2].Equipo1.Nombre == "")
                        {
                            semi[rondaActual / 2].Equipo1.Nombre = getRondaActual().Ganador.Nombre;
                            semi[rondaActual / 2].Equipo1.Integrantes = getRondaActual().Ganador.Integrantes;
                            semi[rondaActual / 2].Equipo1.Puntos = 0;
                        }
                        else
                        {
                            semi[rondaActual / 2].Equipo2.Nombre = getRondaActual().Ganador.Nombre;
                            semi[rondaActual / 2].Equipo2.Integrantes = getRondaActual().Ganador.Integrantes;
                            semi[rondaActual / 2].Equipo2.Puntos = 0;
                        }
                        break;
                    case 4:
                        if(cuartos[rondaActual/2].Equipo1.Nombre == "")
                        {
                            cuartos[rondaActual / 2].Equipo1.Nombre = getRondaActual().Ganador.Nombre;
                            cuartos[rondaActual / 2].Equipo1.Integrantes = getRondaActual().Ganador.Integrantes;
                            cuartos[rondaActual / 2].Equipo1.Puntos = 0;
                        }
                        else
                        {
                            cuartos[rondaActual / 2].Equipo1.Nombre = getRondaActual().Ganador.Nombre;
                            cuartos[rondaActual / 2].Equipo1.Integrantes = getRondaActual().Ganador.Integrantes;
                        }
                        break;
                }
                if(getRondaActual() == finalFase())
                {
                    if(fase != 1)
                    {
                        fase--;
                        rondaActual = 0;
                        getRondaActual().crearEncuentros();
                    }
                    else
                    {
                        ganador = getRondaActual().Ganador;
                    }
                }
                else
                {
                    rondaActual++;
                    getRondaActual().crearEncuentros();
                } 
            }
        }
    }
}