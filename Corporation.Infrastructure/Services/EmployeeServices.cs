namespace Corporation.Infrastructure.Services;

using System.ComponentModel.Design;
using Corporation.Core.Entities;
using Corporation.Core.Interface;
using Corporation.Infrastructure.DbContextSim;
using Corporation.Infrastructure.Utilities.Exceptions;

public class EmployeeServices
{
    public static int indexCounter = 0;


    public void Create(string name, string surname, double salary, string companyname, int departmentid)
    {
        bool companyExists = false;
        foreach (var companies in AppDbContextSim.companies)
        {
            if (companies != null && companies.Name.ToUpper() == companyname.ToUpper())
            {
                companyExists = true;
                break;
            }
        }
        if (!companyExists) { throw new NonExistentEntityException("Company with given name does not exist"); }

        bool depExists = false;

        foreach (var departments in AppDbContextSim.departments)
        {
            if (departments != null && departments.Id == departmentid)
            {
                depExists = true;
            }
        }
        if (!depExists) { throw new NonExistentEntityException("Department with given ID does not exist"); }

        Department department = null;
        foreach (var departments in AppDbContextSim.departments)
        {
            if (departments != null && departments.Id == departmentid)
            {
                department = departments;
                break;
            }
        }
        if (department == null) { throw new NonExistentEntityException("Department with given ID does not exist"); }
        if (department.CurrentEmployeeCount >= department.EmployeeLimit) { throw new Utilities.Exceptions.CapacityLimitException("The department has reached the maximum number of employees."); }

        Employee newEmployee = new Employee(name, surname, salary, companyname, departmentid);
        AppDbContextSim.employees[indexCounter++] = newEmployee;
        department.AddEmployee(newEmployee); // add employee to department
    }
    public void GetAll() // SHOW DEPARTMENT NAME 
    {
        for (int i = 0; i < indexCounter; i++)
        {
            Console.WriteLine($"Employee ID: {AppDbContextSim.employees[i].Id}\n" +
                $"Name: {AppDbContextSim.employees[i].Name} \n" +
                $"Surname: {AppDbContextSim.employees[i].Surname}\n" +
                $"Company: {AppDbContextSim.employees[i].CompanyName}\n" +
                $"Department: {AppDbContextSim.employees[i].DepartmentId}\n");
        }
    }
    public void GetAllByDepartmentId(int id)
    {
        for (int i = 0; i < AppDbContextSim.employees.Length; i++)
        {
            if (AppDbContextSim.employees[i] is null) { break; }
            else if (AppDbContextSim.employees[i].DepartmentId == id)
            {
                Console.WriteLine($"Employees ID: {AppDbContextSim.employees[i].Id}\n" +
                    $"Name: {AppDbContextSim.employees[i].Name}\n" +
                    $"Surname: {AppDbContextSim.employees[i].Surname}");
            }
        }
    }
}

