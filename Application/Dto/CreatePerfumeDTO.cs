using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class CreatePerfumeDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int VolumeInMl { get; set; }
        public string FragranceFamily { get; set; }
        public string Description { get; set; }
        public bool IsUnisex { get; set; }
        public bool IsNew { get; set; }
    }
}
