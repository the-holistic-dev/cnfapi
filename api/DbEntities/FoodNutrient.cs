using System;

namespace api.DbEntities;

public class FoodNutrient : BaseEntity
{
    public required int Amount { get; set; }
    public required Food Food { get; set; }
    public required Nutrient Nutrient { get; set; }
}
