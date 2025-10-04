using System;
using System.Threading.Tasks;
using api.DbEntities;
using api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

public class FoodNutrientsController : BaseController
{
    private readonly AppDbContext _dbContext;

    public FoodNutrientsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetFoodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var nutrients = await _dbContext.FoodNutrients.Where(e => e.Id == id).ToListAsync();
        if (nutrients.Count == 0)
        {
            return NotFound();
        }
        return Ok(nutrients);
    }
}
