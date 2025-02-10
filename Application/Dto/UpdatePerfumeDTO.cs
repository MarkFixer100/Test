using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class UpdatePerfumeDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int VolumeInMl { get; set; }
        public string FragranceFamily { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsUnisex { get; set; }
        [Required]
        public bool IsNew { get; set; }
    }
}
