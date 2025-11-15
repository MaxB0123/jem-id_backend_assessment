using jem_id_backend_assessment.Data;
using jem_id_backend_assessment.DTOs;
using jem_id_backend_assessment.Models;
using Microsoft.EntityFrameworkCore;

namespace jem_id_backend_assessment.Services;

public class ArticleService : IArticleService
{
    private readonly AppDbContext _context;

    public ArticleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ArticleDto> CreateArticleAsync(CreateArticleDto dto)
    {
        if (await _context.Articles.AnyAsync(a => a.Code == dto.Code))
            throw new Exception("Article with this code already exists.");

        var article = new Article
        {
            Code = dto.Code,
            Name = dto.Name,
            PotSize = dto.PotSize,
            PlantHeight = dto.PlantHeight,
            Color = dto.Color,
            ProductGroup = dto.ProductGroup
        };

        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return new ArticleDto
        {
            Id = article.Id,
            Code = article.Code,
            Name = article.Name,
            PotSize = article.PotSize,
            PlantHeight = article.PlantHeight,
            Color = article.Color,
            ProductGroup = article.ProductGroup
        };
    }

    public async Task<ArticleDto?> GetArticleByIdAsync(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) return null;

        return new ArticleDto
        {
            Id = article.Id,
            Code = article.Code,
            Name = article.Name,
            PotSize = article.PotSize,
            PlantHeight = article.PlantHeight,
            Color = article.Color,
            ProductGroup = article.ProductGroup
        };
    }

    public async Task<bool> UpdateArticleAsync(int id, UpdateArticleDto dto)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) return false;

        article.Name = dto.Name;
        article.PotSize = dto.PotSize;
        article.PlantHeight = dto.PlantHeight;
        article.Color = dto.Color;
        article.ProductGroup = dto.ProductGroup;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteArticleAsync(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) return false;

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<PagedResult<ArticleDto>> GetArticlesAsync(ArticleQueryParameters query)
    {
        var q = _context.Articles.AsQueryable();

        // Searching (name contains)
        if (!string.IsNullOrWhiteSpace(query.Name))
        {
            q = q.Where(a => a.Name.Contains(query.Name));
        }

        // Filtering
        if (!string.IsNullOrWhiteSpace(query.ProductGroup))
        {
            q = q.Where(a => a.ProductGroup == query.ProductGroup);
        }

        if (!string.IsNullOrWhiteSpace(query.Color))
        {
            q = q.Where(a => a.Color == query.Color);
        }

        if (query.PotSizeFrom.HasValue)
        {
            q = q.Where(a => a.PotSize >= query.PotSizeFrom.Value);
        }

        if (query.PotSizeTo.HasValue)
        {
            q = q.Where(a => a.PotSize <= query.PotSizeTo.Value);
        }

        if (query.PlantHeightFrom.HasValue)
        {
            q = q.Where(a => a.PlantHeight >= query.PlantHeightFrom.Value);
        }

        if (query.PlantHeightTo.HasValue)
        {
            q = q.Where(a => a.PlantHeight <= query.PlantHeightTo.Value);
        }

        // Sorting
        q = (query.SortBy?.ToLowerInvariant()) switch
        {
            "name" => query.Desc ? q.OrderByDescending(a => a.Name) : q.OrderBy(a => a.Name),
            "height" or "plantheight" => query.Desc
                ? q.OrderByDescending(a => a.PlantHeight)
                : q.OrderBy(a => a.PlantHeight),
            "potsize" => query.Desc ? q.OrderByDescending(a => a.PotSize) : q.OrderBy(a => a.PotSize),
            "code" => query.Desc ? q.OrderByDescending(a => a.Code) : q.OrderBy(a => a.Code),
            _ => q.OrderBy(a => a.Id) // default sort
        };

        // Total count BEFORE paging
        var totalCount = await q.CountAsync();

        // Paging
        var page = query.Page < 1 ? 1 : query.Page;
        var pageSize = query.PageSize <= 0 ? 20 : query.PageSize;

        var items = await q
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new ArticleDto
            {
                Id = a.Id,
                Code = a.Code,
                Name = a.Name,
                PotSize = a.PotSize,
                PlantHeight = a.PlantHeight,
                Color = a.Color,
                ProductGroup = a.ProductGroup
            })
            .ToListAsync();

        return new PagedResult<ArticleDto>(items, totalCount, page, pageSize);
    }
}
