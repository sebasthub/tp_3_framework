namespace Cinemaxx
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("usuario")]
    public partial class usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuario()
        {
            ingresso = new HashSet<ingresso>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Display(Name = "usuario")]
        [StringLength(50)]
        public string nome { get; set; }

        [StringLength(320)]
        public string email { get; set; }

        [StringLength(20)]
        public string senha { get; set; }

        public bool? administrador { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ingresso> ingresso { get; set; }
    }
}
