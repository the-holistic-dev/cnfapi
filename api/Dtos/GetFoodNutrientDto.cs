using System;

namespace api.Dtos;

public class GetFoodNutrientDto
{
    public required float Amount { get; set; }
    public required string NameFr { get; set; }
    public required string NameEn { get; set; }
}
