using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace Proyecto1_IPC2.Models
{
    public class Encuentro
    {
        private Models.ViewModels.Partida partida;
        private int numero;
        private Jugador jugador1;
        private Jugador jugador2;

        public Encuentro()
        {
            partida = new Models.ViewModels.Partida();
            jugador1 = new Jugador();
            jugador2 = new Jugador();
        }

        public Models.ViewModels.Partida Partida { get { return partida; } set { partida = value; } }
        public int Numero { get { return numero; } set { numero = value; } }
        public Jugador Jugador1 { get { return jugador1; } set { jugador1 = value; } }
        public Jugador Jugador2 { get { return jugador2; } set { jugador2 = value; } }

        public void createPartida()
        {
            var rand = new Random();
            Models.ViewModels.Partida partida = new Models.ViewModels.Partida();
            partida.Turnos = 0;
            partida.P1.NombreUsuario = jugador1.NombreUsuario;
            partida.P1.Id = jugador1.Id;
            partida.P2.NombreUsuario = jugador2.NombreUsuario;
            partida.P2.Id = jugador2.Id;
            partida.P1.Movimientos = 0;
            partida.P2.Movimientos = 0;
            partida.Mesa.Cuadricula[3, 3].Color = 1;
            partida.Mesa.Cuadricula[3, 4].Color = 2;
            partida.Mesa.Cuadricula[4, 3].Color = 2;
            partida.Mesa.Cuadricula[4, 4].Color = 1;
            partida.P1.Playable = true;
            partida.P2.Playable = true;
            partida.IsPlaying = 0;
            partida.P1.Color = rand.Next(1, 3);
            if (partida.P1.Color == 1)
            {
                partida.P1.Colores = new int[1];
                partida.P1.Colores[0] = partida.P1.Color;
                partida.P2.Color = 2;
                partida.P2.Colores = new int[1];
                partida.P2.Colores[0] = partida.P2.Color;
                partida.Orden = new int[2];
                partida.Orden[0] = partida.P1.Color;
                partida.Orden[1] = partida.P2.Color;
            }
            else
            {
                partida.P2.Colores = new int[1];
                partida.P2.Colores[0] = partida.P2.Color;
                partida.P1.Color = 2;
                partida.P1.Colores = new int[1];
                partida.P1.Colores[0] = partida.P1.Color;
                partida.Orden = new int[2];
                partida.Orden[0] = partida.P2.Color;
                partida.Orden[1] = partida.P1.Color;
            }
            if (rand.Next(1, 3) == 1)
            {
                partida.Turno = partida.P1.Color;
            }
            else
            {
                partida.Turno = partida.P2.Color;
            }
            partida.Type = 8;
            partida.IsPlaying = 1;
            this.partida = partida;
        }
    }
}