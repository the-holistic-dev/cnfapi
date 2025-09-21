using System;

namespace api.DbEntities;

public class FoodGroup : BaseEntity
{
    public required string NameFr { get; set; }
    public required string NameEn { get; set; }

    public List<Food> Foods { get; set; } = [];
}
