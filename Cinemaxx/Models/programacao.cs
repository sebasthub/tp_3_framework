namespace Cinemaxx
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("programacao")]
    public partial class programacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public programacao()
        {
            ingresso = new HashSet<ingresso>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? sala { get; set; }

        public int? filme { get; set; }

        public TimeSpan? horario { get; set; }

        public virtual filme filme1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ingresso> ingresso { get; set; }

        public virtual sala sala1 { get; set; }
    }
}
