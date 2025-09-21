using System;

namespace api.DbEntities;

public class Nutrient : BaseEntity
{
    public required string NameFr { get; set; }
    public required string NameEn { get; set; }
    public required string Unit { get; set; }
}
