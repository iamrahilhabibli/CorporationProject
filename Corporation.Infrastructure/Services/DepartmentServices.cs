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
        bool companyExists = false;

        foreach (var companies in AppDbContextSim.companies)
        {
            if (AppDbContextSim.companies[0] is null)
            {
                throw new NonExistentEntityException("Company does not exist");
            }
            else if (companies != null && companies.Id == companyid && companies.Name.ToUpper() == companyname.ToUpper())
            {
                companyExists = true;
                Console.WriteLine($"Department: \"{departmentName}\" is successfully added!");
                break;
            }
        }
        if (!companyExists) { throw new NonExistentEntityException("Company with given ID and/or name does not exist\n"); }

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


        Department newDepartment = new(departmentName, employeelimit, companyid, companyname);
        AppDbContextSim.departments[indexCounter++] = newDepartment;
    }
    public void Update(int departmentId, string newDepartmentName, int newEmployeeLimit)
    {
        {
            bool departmentFound = false;
            for (int i = 0; i < indexCounter; i++)
            {
                if (i == departmentId)
                {
                    if (AppDbContextSim.departments[i].Name.ToUpper() == newDepartmentName.ToUpper() && AppDbContextSim.departments[i].EmployeeLimit == newEmployeeLimit)
                    {
                        throw new IdenticalNameException("The new values for department is exactly same as old values.");
                    }
                    AppDbContextSim.departments[i].Name = newDepartmentName;
                    AppDbContextSim.departments[i].EmployeeLimit = newEmployeeLimit;
                    departmentFound = true;
                    break;
                }
            }
            if (!departmentFound)
            {
                throw new NonExistentEntityException("Department with given ID does not exist");
            }
        }

    }
    public void GetAll()
    {
        if (AppDbContextSim.departments[0] is null) { throw new NonExistentEntityException("No departments have been created!"); }
        for (int i = 0; i < indexCounter; i++)
        {
            if (AppDbContextSim.departments[i] is null) { break; }
            else { Console.WriteLine(AppDbContextSim.departments[i].ToString()); }
        }
    }
    public bool doesDepartmentBelongToCompany(string name, int id)
    {
        for (int i = 0; i < AppDbContextSim.departments.Length; i++)
        {
            if (AppDbContextSim.departments[i] is null) { break; }
            else if (i == id && AppDbContextSim.departments[i] != null && AppDbContextSim.departments[id].CompanyName.ToUpper() == name.ToUpper())
            {
                return true;
            }
        }
        throw new UnauthorizedAccessException("Department does not belong to your company");
    }
}


