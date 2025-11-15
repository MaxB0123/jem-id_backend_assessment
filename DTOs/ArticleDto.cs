namespace jem_id_backend_assessment.DTOs;

public class ArticleDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int PotSize { get; set; }
    public int PlantHeight { get; set; }
    public string? Color { get; set; }
    public string ProductGroup { get; set; } = null!;
}
