using System;

namespace api.DbEntities;

public class ConversionFactor : BaseEntity
{
    public required int FoodId { get; set; }
    public required int MeasureId { get; set; }
    public required float Factor { get; set; }
}
