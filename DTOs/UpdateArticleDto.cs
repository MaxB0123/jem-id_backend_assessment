using System.ComponentModel.DataAnnotations;

namespace jem_id_backend_assessment.DTOs;

public class UpdateArticleDto
{
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
