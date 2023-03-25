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
                Console.WriteLine($"Department: \"{departmentName}\" is successfully added!");
                break;
            }
        }
        if (!companyExists) { throw new NonExistentEntityException("Company with given ID and/or name does not exist\n"); }

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
    public void GetAll()
    {
        if (AppDbContextSim.departments[0] is null) { throw new NullOrEmptyException("No departments have been created!"); }
        for (int i = 0; i < AppDbContextSim.departments.Length; i++)
        {
            if (AppDbContextSim.departments[i] is null) { break; }
            else { Console.WriteLine(AppDbContextSim.departments[i].ToString()); }
        }
    }
}


