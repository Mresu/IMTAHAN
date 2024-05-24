using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        [MinLength(3)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(255)]
        [MinLength(3)]

        public string Description { get; set; }

        [Required]
        [MaxLength(255)]

        [MinLength(3)]
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }

    }
}
