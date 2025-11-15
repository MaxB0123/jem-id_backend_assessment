using jem_id_backend_assessment.DTOs;

namespace jem_id_backend_assessment.Services;

public interface IArticleService
{
    Task<ArticleDto> CreateArticleAsync(CreateArticleDto dto);
    Task<ArticleDto?> GetArticleByIdAsync(int id);
    Task<bool> UpdateArticleAsync(int id, UpdateArticleDto dto);
    Task<bool> DeleteArticleAsync(int id);
    
    Task<PagedResult<ArticleDto>> GetArticlesAsync(ArticleQueryParameters query);
}
