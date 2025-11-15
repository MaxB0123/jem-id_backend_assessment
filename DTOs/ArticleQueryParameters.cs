namespace jem_id_backend_assessment.DTOs;

public class ArticleQueryParameters
{
    // Paging
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    // Searching
    public string? Name { get; set; }

    // Filtering
    public string? ProductGroup { get; set; }
    public string? Color { get; set; }

    // Optional numeric filters
    public int? PotSizeFrom { get; set; }
    public int? PotSizeTo { get; set; }
    public int? PlantHeightFrom { get; set; }
    public int? PlantHeightTo { get; set; }

    // Sorting
    public string? SortBy { get; set; }
    public bool Desc { get; set; } = false;
}
