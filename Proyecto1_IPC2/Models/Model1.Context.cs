﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto1_IPC2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OTHELLOEntities : DbContext
    {
        public OTHELLOEntities()
            : base("name=OTHELLOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Participante> Participante { get; set; }
        public virtual DbSet<Partida> Partida { get; set; }
        public virtual DbSet<Ronda> Ronda { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<TipoPartida> TipoPartida { get; set; }
        public virtual DbSet<Torneo> Torneo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
