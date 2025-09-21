using System;

namespace api.DbEntities;

public class ConversionFactor : BaseEntity
{
    public required float Factor { get; set; }
    public required Food Food { get; set; }
    public required Measure Measure { get; set; }
}
