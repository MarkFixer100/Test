using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Perfume
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty ;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = decimal.Zero;

        public int VolumeInMl { get; set; } = 0;
        public string FragranceFamily { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public bool IsUnisex { get; set; } 
        public bool IsNew { get; set; }
    }
}