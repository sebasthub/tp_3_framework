namespace Cinemaxx
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("filme")]
    public partial class filme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public filme()
        {
            programacao = new HashSet<programacao>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Display(Name = "filme")]
        [StringLength(255)]
        public String nome { get; set; }

        public TimeSpan? duracao { get; set; }

        [StringLength(50)]
        public string classificacao { get; set; }

        [StringLength(1000)]
        public string descricao { get; set; }

        [StringLength(30)]
        public string categoria { get; set; }

        [StringLength(200)]
        public string idioma { get; set; }

        public bool? legenda { get; set; }

        public bool? novo { get; set; }

        [Column(TypeName = "money")]
        public decimal? preco { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<programacao> programacao { get; set; }
    }
}
