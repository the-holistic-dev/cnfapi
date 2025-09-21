using api.DbEntities;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

namespace api;

public class SeedData
{
    public static void MigrateAndSeed(IServiceProvider serviceProvider)
    {
        string foodGroupFilePath = "./storage/FOOD GROUP.xlsx";
        string nutrientNameFilePath = "./storage/NUTRIENT NAME.xlsx";
        string measureFilePath = "./storage/MEASURE NAME.xlsx";
        string foodNameFilePath = "./storage/FOOD NAME.xlsx";
        string conversionFactorFilePath = "./storage/CONVERSION FACTOR.xlsx";
        string nutrientAmountFilePath = "./storage/NUTRIENT AMOUNT.xlsx";

        var context = serviceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        if (!context.FoodGroups.Any())
        {
            using (var stream = File.Open(foodGroupFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (reader.Depth > 0)
                            {
                                context.FoodGroups.AddRange(
                                    new FoodGroup
                                    {
                                        Id = Convert.ToInt16(reader.GetDouble(0)),
                                        NameEn = reader.GetString(2),
                                        NameFr = reader.GetString(3),
                                        Created = DateTime.UtcNow
                                    }
                                );
                            }
                        }
                    } while (reader.NextResult());

                    context.SaveChanges();
                }
            }
        }

        if (!context.Nutrients.Any())
        {
            using (var stream = File.Open(nutrientNameFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (reader.Depth > 0)
                            {
                                context.Nutrients.AddRange(
                                    new Nutrient
                                    {
                                        Id = Convert.ToInt16(reader.GetDouble(0)),
                                        Unit = reader.GetString(3),
                                        NameEn = reader.GetString(4),
                                        NameFr = reader.GetString(5),
                                        Created = DateTime.UtcNow
                                    }
                                );
                            }
                        }
                    } while (reader.NextResult());

                    context.SaveChanges();
                }
            }
        }

        if (!context.Measures.Any())
        {
            using (var stream = File.Open(measureFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (reader.Depth > 0)
                            {
                                context.Measures.AddRange(
                                    new Measure
                                    {
                                        Id = Convert.ToInt32(reader.GetDouble(0)),
                                        DescriptionEn = reader.GetString(1),
                                        DescriptionFr = reader.GetString(2),
                                        Created = DateTime.UtcNow
                                    }
                                );
                            }
                        }
                    } while (reader.NextResult());

                    context.SaveChanges();
                }
            }
        }

        /*         if (!context.Foods.Any())
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    using (var stream = File.Open(foodNameFilePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            do
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{reader.GetValue(2)}  {reader.GetValue(3)}");
                                    context.FoodGroups.AddRange(
                                        new FoodGroup
                                        {
                                            NameEn = reader.GetString(2),
                                            NameFr = reader.GetString(3)
                                        }
                                    );
                                }
                            } while (reader.NextResult());

                            context.SaveChanges();
                        }
                    }
                } */
    }
}
