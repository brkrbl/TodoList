using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


#nullable disable

namespace Entities
{
    [Table("todotable")]
    public partial class Todotable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("theader")]
        [StringLength(100)]
        public string Theader { get; set; }
        [Required]
        [Column("tcontent")]
        [StringLength(10000)]
        public string Tcontent { get; set; }
        [Column("puserid")]
        public int Puserid { get; set; }
        [Column("pdate", TypeName = "date")]
        public DateTime Pdate { get; set; }
    }
}
