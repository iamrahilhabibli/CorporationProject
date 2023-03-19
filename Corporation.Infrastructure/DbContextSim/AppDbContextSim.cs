using Corporation.Core.Entities;
namespace Corporation.Infrastructure.DbContextSim;

public static class AppDbContextSim
{
    public static Company[] companies { get; set; } = new Company[10];
    public static Department[] departments { get; set; } = new Department[10];
    public static Employee[] employees { get; set; } = new Employee[10];
    public static Businesses[] businesses { get; set; } = new Businesses[10];
}

