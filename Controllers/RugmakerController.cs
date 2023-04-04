using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VintageRugsApi.Data;
using VintageRugsApi.DTOs.Rugs;
using VintageRugsApi.Models;

namespace VintageRugsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RugmakerController : ControllerBase
{
    private ILogger<RugmakerController> _logger;
    private VintageRugsDbContext _dbContext;
    
    public RugmakerController(ILogger<RugmakerController> logger, VintageRugsDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("GET /rugmaker");
        var rugs = await _dbContext.Rugmakers.ToListAsync();
        
        return Ok(rugs);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation("GET /rugmaker/{Id}", id);
        var rugmaker = await _dbContext.Rugmakers.FindAsync(id);
        if (rugmaker == null)
        {
            return NotFound(rugmaker);
        }
        
        return Ok(rugmaker);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRugmakerRequestDTO rugmakerDto)
    {
        _logger.LogInformation("POST /rugmaker");
        
        var rugmaker = new Rugmaker()
        {
            FullName = rugmakerDto.FullName,
            Country = rugmakerDto.Country,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        _dbContext.Rugmakers.Add(rugmaker);
        await _dbContext.SaveChangesAsync();
        
        return Ok(rugmaker);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRugmakerRequestDTO updateRugmakerRequest)
    {
        _logger.LogInformation("PUT /rugmaker");
        var rugmaker = await _dbContext.Rugmakers.FindAsync(new Rugmaker() { Id = updateRugmakerRequest.Id });
        if (rugmaker == null)
        {
            return NotFound();
        }
        
        rugmaker.FullName = updateRugmakerRequest.FullName;
        rugmaker.Country = updateRugmakerRequest.Country;
        rugmaker.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();
        return Ok(rugmaker);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("DELETE /rugmaker");
        var rugmaker = await _dbContext.Rugmakers.FindAsync(new Rugmaker { Id = id });
        if (rugmaker == null)
        {
            return Ok();
        }
        
        _dbContext.Rugmakers.Remove(rugmaker);
        
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}