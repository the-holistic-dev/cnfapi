using System;

namespace api.DbEntities;

public class Food : BaseEntity
{
    public required string NameFr { get; set; }
    public required string NameEn { get; set; }
    public required FoodGroup FoodGroup { get; set; }
    public List<FoodNutrient> Nutrients { get; set; } = [];
}
