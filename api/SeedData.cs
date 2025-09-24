using System.ComponentModel;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using api.DbEntities;
using ExcelDataReader;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
                                context.FoodGroups.Add(
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
                                context.Nutrients.Add(
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
                                context.Measures.Add(
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

        if (!context.Foods.Any())
        {
            using (var stream = File.Open(foodNameFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (reader.Depth > 0)
                            {
                                context.Foods.Add(
                                    new Food
                                    {
                                        Id = Convert.ToInt32(reader.GetDouble(0)),
                                        FoodGroup = context.FoodGroups.Single(e => e.Id == Convert.ToInt32(reader.GetDouble(2))),
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


        if (!context.Factors.Any())
        {
            using (var stream = File.Open(conversionFactorFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (reader.Depth > 0)
                            {
                                context.Factors.Add(
                                    new ConversionFactor
                                    {
                                        Food = context.Foods.Single(e => e.Id == Convert.ToInt32(reader.GetDouble(0))),
                                        Measure = context.Measures.Single(e => e.Id == Convert.ToInt32(reader.GetDouble(1))),
                                        Factor = (float)reader.GetDouble(2),
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

        if (!context.FoodNutrients.Any())
        {
            using (var stream = File.Open(nutrientAmountFilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var imports = new List<FoodNutrientImport>();
                    do
                    {
                        while (reader.Read())
                        {
                            if (reader.Depth > 0 && reader.GetValue(0) != null)
                            {
                                if (context.Foods.Any(e => e.Id == Convert.ToInt32(reader.GetDouble(0)))
                                    && context.Nutrients.Any(e => e.Id == Convert.ToInt32(reader.GetDouble(1))))
                                {
                                    var id = reader.Depth;
                                    var amount = (float)reader.GetDouble(2);
                                    var created = DateTime.UtcNow;
                                    var foodId = Convert.ToInt32(reader.GetDouble(0));
                                    var nutrientId = Convert.ToInt32(reader.GetDouble(1));

                                    context.Database.ExecuteSqlRaw($"INSERT INTO FoodNutrients VALUES(@id, @amount, @created, @foodId, @modified, @nutrientId)",
                                    new SqliteParameter("id", id),
                                    new SqliteParameter("amount", amount),
                                    new SqliteParameter("created", created),
                                    new SqliteParameter("foodId", foodId),
                                    new SqliteParameter("modified", ""),
                                    new SqliteParameter("nutrientId", nutrientId)
                                    );
                                }
                            }
                        }
                    } while (reader.NextResult());

                    context.SaveChanges();
                }
            }
        }
    }
}