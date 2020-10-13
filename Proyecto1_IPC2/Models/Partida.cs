//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Partida
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partida()
        {
            this.Ronda = new HashSet<Ronda>();
        }
    
        public int idPartida { get; set; }
        public int idUsuario { get; set; }
        public Nullable<int> idAdversario { get; set; }
        public System.DateTime horaFecha { get; set; }
        public int idTipoPartida { get; set; }
        public int idEstado { get; set; }
        public int turnos { get; set; }
    
        public virtual Estado Estado { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual TipoPartida TipoPartida { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ronda> Ronda { get; set; }
    }
}
