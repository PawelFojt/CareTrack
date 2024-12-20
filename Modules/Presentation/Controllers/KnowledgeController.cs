using CareTrack.Server.Modules.Infrastructure.Entities;
using CareTrack.Server.Modules.Infrastructure.presistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CareTrack.Server.Modules.Presentation.Controllers;

[ApiController]
[Route("knowledge")]
public class KnowledgeController : ControllerBase
{
    private readonly CareTrackDbContext _context;
    public KnowledgeController(CareTrackDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _context.KnowledgeItems.ToListAsync();
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> AddKnowledgeItem([FromBody] KnowledgeItem item)
    {
        if (string.IsNullOrEmpty(item.Author))
            return BadRequest("Author is required.");
        
        _context.KnowledgeItems.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateKnowledgeItem(int id, [FromBody] KnowledgeItem updatedItem)
    {
        var item = await _context.KnowledgeItems.FindAsync(id);
        if (item == null) return NotFound("Item not found");

        if (item.Author != updatedItem.Author)
            return Forbid("You are not the author of this item.");

        item.Title = updatedItem.Title;
        item.Description = updatedItem.Description;
        item.Type = updatedItem.Type;
        item.Url = updatedItem.Url;

        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKnowledgeItem(int id, [FromBody] KnowledgeItem request)
    {
        var item = await _context.KnowledgeItems.FindAsync(id);
        if (item == null) return NotFound("Item not found");

        if (item.Author != request.Author)
            return Forbid("You are not the author of this item.");

        _context.KnowledgeItems.Remove(item);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Item deleted successfully" });
    }
}
