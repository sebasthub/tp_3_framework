namespace Cinemaxx
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ingresso")]
    public partial class ingresso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ingresso()
        {
            pagamento = new HashSet<pagamento>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int? cadeira { get; set; }

        public int? fileira { get; set; }

        public int? programacao { get; set; }

        public int? usuario { get; set; }

        public virtual fileira fileira1 { get; set; }

        public virtual programacao programacao1 { get; set; }

        public virtual usuario usuario1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pagamento> pagamento { get; set; }
    }
}
