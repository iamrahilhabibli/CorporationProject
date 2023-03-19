﻿using Corporation.Core.Entities;
using Corporation.Infrastructure.DbContextSim;
using Corporation.Infrastructure.Utilities.Exceptions;
namespace Corporation.Infrastructure.Services;

public class DepartmentServices
{
    int indexCounter = 0;

    public void Create(string name, int employeelimit, int companyid)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new NullOrEmptyException("The department name must contain at least one character and cannot be left empty");
        }

        bool depExists = false;

        for (int i = 0; i < indexCounter; i++)
        {
            if (AppDbContextSim.departments[i].Name.ToUpper() == name.ToUpper() && AppDbContextSim.departments[i].CompanyId == companyid)
            {
                depExists = true;
                break;
            }
        }
        if (depExists)
        {
            throw new IdenticalNameException("This department has already been registered for your Company");
        }
        Department newDepartment = new(name, employeelimit, companyid);
        AppDbContextSim.departments[indexCounter++] = newDepartment;
    }
    public void GetAll()
    {
        for (int i = 0; i < indexCounter; i++)
        {
            Console.WriteLine($"Department belongs to Company ID: {AppDbContextSim.departments[i].Id}" +
                $"\nDepartment Name: {AppDbContextSim.departments[i].Name}" +
                $"\nHas Employee Limit: {AppDbContextSim.departments[i].EmployeeLimit}");
        }
    }
}
