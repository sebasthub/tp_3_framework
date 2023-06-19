namespace Cinemaxx
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fileira")]
    public partial class fileira
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fileira()
        {
            ingresso = new HashSet<ingresso>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? sala { get; set; }

        [Display(Name = "fileira")]
        [StringLength(50)]
        public string indentificador { get; set; }

        public int? cadeiras_de { get; set; }

        public int? cadeiras_ate { get; set; }

        public bool? pne { get; set; }

        public int? pne_de { get; set; }

        public int? pne_ate { get; set; }

        public virtual sala sala1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ingresso> ingresso { get; set; }
    }
}
