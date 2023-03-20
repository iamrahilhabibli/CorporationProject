using Corporation.Core.Entities;
namespace Corporation.Infrastructure.DbContextSim;

public static class AppDbContextSim
{
    public static Company[] companies { get; set; } = new Company[100];
    public static Department[] departments { get; set; } = new Department[100];
    public static Employee[] employees { get; set; } = new Employee[100];
}

