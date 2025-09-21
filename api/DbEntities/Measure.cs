using System;

namespace api.DbEntities;

public class Measure : BaseEntity
{
    public required string DescriptionFr { get; set; }
    public required string DescriptionEn { get; set; }
}
