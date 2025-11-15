using jem_id_backend_assessment.DTOs;
using jem_id_backend_assessment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jem_id_backend_assessment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _service;

    public ArticlesController(IArticleService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateArticle(CreateArticleDto dto)
    {
        var result = await _service.CreateArticleAsync(dto);
        return CreatedAtAction(nameof(GetArticleById), new { id = result.Id }, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetArticleById(int id)
    {
        var result = await _service.GetArticleByIdAsync(id);
        if (result == null) return NotFound();

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetArticles([FromQuery] ArticleQueryParameters query)
    {
        var result = await _service.GetArticlesAsync(query);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> UpdateArticle(int id, UpdateArticleDto dto)
    {
        try
        {
            var updated = await _service.UpdateArticleAsync(id, dto);
            return updated ? await GetArticleById(id) : NotFound();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);

            return StatusCode(500, new { message = "An unexpected server error occurred." });
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var deleted = await _service.DeleteArticleAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
