using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VintageRugsApi.Data;
using VintageRugsApi.DTOs.Rugs;
using VintageRugsApi.Models;

namespace VintageRugsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RugController : ControllerBase
{
    private ILogger<RugController> _logger;
    private VintageRugsDbContext _dbContext;
    
    public RugController(ILogger<RugController> logger, VintageRugsDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("GET /rug");
        var rugs = await _dbContext.Rugs.ToListAsync();
        
        return Ok(rugs);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation("GET /rug/{Id}", id);
        var rug = await _dbContext.Rugs.FindAsync(id);
        if (rug == null)
        {
            return NotFound(rug);
        }
        
        return Ok(rug);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRugRequestDTO rugDto)
    {
        _logger.LogInformation("POST /rug");
        int? rugMakerId = null;
        if (rugDto.RugmakerId != null)
        {
            var rugMaker = await _dbContext.Rugmakers.FindAsync();
            if (rugMaker == null)
            {
                return BadRequest("Rugmaker not found");
            }
            
            rugMakerId = rugMaker.Id;
        }
        
        var rug = new Rug()
        {
            Name = rugDto.Name,
            MadeIn = rugDto.MadeIn,
            ImageUrl = rugDto.ImageUrl,
            Description = rugDto.Description,
            Price = rugDto.Price,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Rugmaker = rugMakerId == null 
                ? null 
                : new Rugmaker() { Id = (int)rugMakerId }
        };

        _dbContext.Rugs.Add(rug);
        await _dbContext.SaveChangesAsync();
        
        return Ok(rug);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRugRequestDTO updateRugRequestDto)
    {
        _logger.LogInformation("PUT /rug");
        var rug = await _dbContext.Rugs.FindAsync(new Rug { Id = updateRugRequestDto.Id });
        if (rug == null)
        {
            return NotFound();
        }
        
        rug.Name = updateRugRequestDto.Name;
        rug.MadeIn = updateRugRequestDto.MadeIn;
        rug.ImageUrl = updateRugRequestDto.ImageUrl;
        rug.Description = updateRugRequestDto.Description;
        rug.Price = updateRugRequestDto.Price;
        rug.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
        return Ok(rug);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("DELETE /rug");
        var rug = await _dbContext.Rugs.FindAsync(new Rug { Id = id });
        if (rug == null)
        {
            return Ok();
        }
        
        _dbContext.Rugs.Remove(rug);
        
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}