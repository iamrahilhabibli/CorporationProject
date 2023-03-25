using System.Runtime.Serialization;
using Corporation.Core.Interface;

namespace Corporation.Core.Entities;

public class Department : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int EmployeeLimit { get; set; }
    public int CurrentEmployeeCount { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    private static int _count { get; set; }

    public Department()
    {
        Id = _count++;
    }
    public Department(string name, int employeelimit, int companyid, string companyname) : this()
    {
        this.Name = name;
        this.EmployeeLimit = employeelimit;
        this.CompanyName = companyname;
        this.CompanyId = companyid;
        this.CurrentEmployeeCount = 0;
    }
    public void AddEmployee(Employee employee)
    {
        CurrentEmployeeCount++;
        employee.DepartmentId = this.Id;
    }
    public override string ToString() { return $"Department Id: {Id} | Department name: {Name} | Employee Limit: {EmployeeLimit} | Current Employee Count: {CurrentEmployeeCount} "; }
}
