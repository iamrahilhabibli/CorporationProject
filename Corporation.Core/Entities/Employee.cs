using Corporation.Core.Interface;

namespace Corporation.Core.Entities;

public class Employee : IEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    private static int _count { get; set; }
    public double Salary { get; set; }

    public Employee()
    {
        Id = _count++;
    }
    public Employee(string name, string surname, double salary) : this()
    {
        this.Name = name;
        this.Surname = surname;
        this.Salary = salary;
    }
}

