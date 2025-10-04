namespace api.DbEntities;

public class FoodNutrient : BaseEntity
{
    public required float Amount { get; set; }
    public required Food Food { get; set; }
    public required Nutrient Nutrient { get; set; }
}
