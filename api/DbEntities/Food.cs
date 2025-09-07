using System;

namespace api.DbEntities;

public class Food : BaseEntity
{
    public required int FoodGroupId { get; set; }
    public required string NameFr { get; set; }
    public required string NameEn { get; set; }
}
