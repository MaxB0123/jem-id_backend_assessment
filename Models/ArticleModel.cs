using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace jem_id_backend_assessment.Models;

public class ArticleModel
{
    [Index(nameof(Code), IsUnique = true)]
    public class Article
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(13)]
        public string Code { get; set; } = null!;
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        
        [Required]
        public int PotSize { get; set; }
        
        [Required]
        public int PlantHeight { get; set; }
        
        public string? Color { get; set; }
        
        [Required]
        public string ProductGroup { get; set; } = null!;
    }
}
