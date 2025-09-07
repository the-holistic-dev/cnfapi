using System;

namespace api.DbEntities;

public class FoodNutrient : BaseEntity
{
    public required int Amount { get; set; }
    public required int FoodId { get; set; }
    public required int NutrientId { get; set; }
}
