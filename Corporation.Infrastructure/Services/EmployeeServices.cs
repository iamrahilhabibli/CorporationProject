namespace Corporation.Infrastructure.Services;

using System;
using System.ComponentModel.Design;
using System.Xml.Linq;
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
    public void GetAll()
    {
        if (AppDbContextSim.employees[0] is null) { throw new NullOrEmptyException("No employees have been created!"); }
        for (int i = 0; i < indexCounter; i++)
        {
            if (AppDbContextSim.employees[i] is null) { break; }
            else { Console.WriteLine(AppDbContextSim.employees[i].ToString()); }
        }
    }
    public void GetAllByDepartmentId(int id)
    {
        for (int i = 0; i < AppDbContextSim.employees.Length; i++)
        {
            if (AppDbContextSim.employees[i] is null) { break; }
            else if (AppDbContextSim.employees[i].DepartmentId == id)
            {
                Console.WriteLine($"{AppDbContextSim.employees[i].ToString()}");
            }
            else { throw new NonExistentEntityException("Given department does not exist!"); }
        }
    }
    public void GetAllByCompanyName(string companyname)
    {
        bool foundEmployees = false;

        foreach (var employee in AppDbContextSim.employees)
        {
            if (employee?.CompanyName?.ToUpper() == companyname.ToUpper())
            {
                Console.WriteLine($"{employee.ToString()}");
                foundEmployees = true;
            }
        }
        if (!foundEmployees)
        {
            throw new NonExistentEntityException("Employee has not been added to your company");
        }
    }
    public void GetByName(string employeeNameOrSurname)
    {
        if (String.IsNullOrEmpty(employeeNameOrSurname) || employeeNameOrSurname.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(employeeNameOrSurname, "[^a-zA-Z0-9 -]"))
        { throw new InvalidNameInput("The search section can not be left empty and/or contain digits or include symbols"); }
        bool nameExists = false;
        string employeeFullname = String.Empty;
        for (int i = 0; i < indexCounter; i++)
        {
            employeeFullname = AppDbContextSim.employees[i].Name + "" + AppDbContextSim.employees[i].Surname;

            if (employeeFullname.ToUpper().Contains(employeeNameOrSurname.ToUpper()))
            {
                nameExists = true;
                Console.WriteLine($"Employee Name: {AppDbContextSim.employees[i].Name}\n" +
                    $"Employee Surname: {AppDbContextSim.employees[i].Surname}\n" +
                    $"Department ID: {AppDbContextSim.employees[i].DepartmentId}\n" +
                    $"Company Name: {AppDbContextSim.employees[i].CompanyName}");
            }
        }
        if (!nameExists) { throw new NotFound("Employee not found!"); }
    }
}

