namespace Corporation.Infrastructure.Services;
using Corporation.Core.Entities;
using Corporation.Core.Interface;
using Corporation.Infrastructure.DbContextSim;
using Corporation.Infrastructure.Utilities.Exceptions;

public class EmployeeServices
{
    public void Create(string name, string surname, double salary, int departmentid)
    {
        int indexCounter = 0;

        //if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || name.All(char.IsDigit) || surname.All(char.IsDigit))
        //{
        //    throw new InvalidNameInput("The employee name/surname must contain at least one character and cannot be left empty or contain only digits");
        //}
        Employee newEmployee = new Employee(name, surname, salary, departmentid);
        AppDbContextSim.employees[indexCounter++] = newEmployee;
    }
}

