using System;
using System.Threading.Tasks;
using api.DbEntities;
using api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace api.Controllers;

public class FoodsController : BaseController
{
    private readonly AppDbContext _dbContext;

    public FoodsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetFoodDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        IQueryable<Food> query = _dbContext.Foods.Include(e => e.FoodGroup).OrderBy(e => e.NameFr).Take(10);
        var foods = await query.ToArrayAsync();

        return Ok(foods.Select(FoodEntityToDto));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetFoodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var food = await _dbContext.Foods.Include(e => e.FoodGroup).SingleOrDefaultAsync(e => e.Id == id);
        if (food == null)
        {
            return NotFound();
        }

        return Ok(FoodEntityToDto(food));
    }

    private async Task<GetFoodDto> FoodEntityToDto(Food food)
    {
        var query = _dbContext.FoodNutrients.Include(e => e.Nutrient).Where(e => e.Food.Id == food.Id);
        var nutrients = await query.ToListAsync();

        return new GetFoodDto
        {
            NameFr = food.NameFr,
            NameEn = food.NameEn,
            FoodGroup = new GetFoodGroupDto
            {
                NameEn = food.FoodGroup.NameEn,
                NameFr = food.FoodGroup.NameFr
            },
            FoodNutrients = nutrients.Select(e => new GetFoodNutrientDto
            {
                Amount = e.Amount,
                NameEn = e.Nutrient.NameEn,
                NameFr = e.Nutrient.NameFr
            }).ToList()
        };
    }
}
