using System;
using api.DbEntities;

namespace api.Dtos;

public class GetFoodDto
{
    public required string NameFr { get; set; }
    public required string NameEn { get; set; }
    public required GetFoodGroupDto FoodGroup { get; set; }
    public List<GetFoodNutrientDto> FoodNutrients { get; set; } = [];
}
