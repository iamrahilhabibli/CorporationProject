using System;
using Corporation.Core.Entities;
using Corporation.Infrastructure.DbContextSim;
using Corporation.Infrastructure.Utilities.Exceptions;
namespace Corporation.Infrastructure.Services;

public class DepartmentServices
{
    public static int indexCounter = 0;

    public void Create(string departmentName, int employeelimit, int companyid, string companyname)
    {
        // Possibly will delete this: 

        if (String.IsNullOrWhiteSpace(departmentName))
        {
            throw new NullOrEmptyException("The company name must contain at least one character and cannot be left empty");
        }
        else if (departmentName.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(departmentName, "[^a-zA-Z0-9 &-]"))
        {
            throw new NonDigitException("A company name must include at least one non-digit character and can not consist of only digits and/or contain symbols.");
        }
        bool depExists = false;

        for (int i = 0; i < indexCounter; i++)
        {
            if (AppDbContextSim.departments[i].Name.ToUpper() == departmentName.ToUpper() && AppDbContextSim.departments[i].CompanyId == companyid)
            {
                depExists = true;
                break;
            }
        }
        if (depExists) { throw new IdenticalNameException("This department has already been registered for your Company"); }

        // if (employeelimit <= 0) { throw new NegativeLimitException("Employee limit can not be negative"); }

        bool companyExists = false;

        foreach (var companies in AppDbContextSim.companies)
        {
            if (companies != null && companies.Id == companyid && companies.Name.ToUpper() == companyname.ToUpper())
            {
                companyExists = true;
                Console.WriteLine($"Department Successfully added!");
                break;
            }
        }
        if (!companyExists) { throw new NonExistentEntityException("Company with given ID and/or name does not exist"); }

        Department newDepartment = new(departmentName, employeelimit, companyid, companyname);
        AppDbContextSim.departments[indexCounter++] = newDepartment;
    }
    public void Update(int departmentId, string newDepartmentName, int newEmployeeLimit)
    {
        for (int i = 0; i < AppDbContextSim.departments.Length; i++)
        {
            if (i == departmentId)
            {
                AppDbContextSim.departments[i].Name = newDepartmentName;
                AppDbContextSim.departments[i].EmployeeLimit = newEmployeeLimit;
                break;
            }
        }
    }
}

