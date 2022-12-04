using Microsoft.EntityFrameworkCore;
using ThisRefactored.Persistence;

namespace ThisRefactored.Application.Tests;

public static class DbContextHelper
{
    public static ProductDbContext GetContextWithFreshDb(string dbName)
    {
        var path = $"App_Data/{dbName}.db";
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        if (!Directory.Exists("App_Data"))
        {
            Directory.CreateDirectory("App_Data");
        }

        if (!File.Exists("App_Data/products.db"))
        {
            using var _ = File.Create("App_Data/products.db");
        }

        var options = new DbContextOptionsBuilder<ProductDbContext>().UseSqlite($"Data Source=App_Data/{dbName}.db")
                                                                     .LogTo(Console.WriteLine)
                                                                     .EnableDetailedErrors()
                                                                     .EnableSensitiveDataLogging()
                                                                     .Options;

        var context = new ProductDbContext(options);
        context.Database.Migrate();

        return context;
    }

    public static ProductDbContext GetContextWithExistingDb(string dbName)
    {
        var options = new DbContextOptionsBuilder<ProductDbContext>().UseSqlite($"Data Source=App_Data/{dbName}.db")
                                                                     .LogTo(Console.WriteLine)
                                                                     .EnableDetailedErrors()
                                                                     .EnableSensitiveDataLogging()
                                                                     .Options;

        var context = new ProductDbContext(options);
        return context;
    }
}