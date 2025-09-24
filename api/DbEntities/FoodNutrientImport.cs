
using System;

namespace api.DbEntities;

public class FoodNutrientImport : BaseEntity
{
    public required float Amount { get; set; }
    public required int Food { get; set; }
    public required int Nutrient { get; set; }
}