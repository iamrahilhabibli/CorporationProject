namespace Corporation.Infrastructure.Services;
using System;
using System.Xml.Linq;
using Corporation.Core.Entities;
using Corporation.Infrastructure.DbContextSim;
using Corporation.Infrastructure.Utilities.Exceptions;


public class CompanyServices
{
    public static int indexCounter = 0;

    public void Create(string companyName)
    {
        if (String.IsNullOrWhiteSpace(companyName))
        {
            throw new NullOrEmptyException("The company name must contain at least one character and cannot be left empty");
        }
        else if (companyName.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(companyName, "[^a-zA-Z0-9 &-]"))
        {
            throw new NonDigitException("A company name must include at least one non-digit character and can not consist of only digits and/or contain symbols.");
        }

        bool compNameExists = false;
        for (int i = 0; i < indexCounter; i++)
        {
            if (AppDbContextSim.companies[i].Name.ToUpper() == companyName.ToUpper())
            {
                compNameExists = true;
                break;
            }
        }
        if (compNameExists)
        {
            throw new IdenticalNameException("This company name has already been registered. Please choose a different name for your new company");
        }
        Company newCompany = new Company(companyName);
        AppDbContextSim.companies[indexCounter++] = newCompany;
    }
    public void GetAll()
    {
        if (AppDbContextSim.companies[0] is null) { throw new NonExistentEntityException("No companies have been created yet!"); }
        foreach (Company company in AppDbContextSim.companies)
        {
            if (company is null) break;
            Console.WriteLine($"Company ID: {company.Id}, Company Name: {company.Name}");
        }
    }
    public void GetAllDepartmentsByID(int id)
    {
        if (AppDbContextSim.departments is null) throw new NonExistentEntityException("Departments do not exist");
        bool departmentExists = false;
        for (int i = 0; i < AppDbContextSim.departments.Length; i++)
        {
            if (AppDbContextSim.departments[i] != null && AppDbContextSim.departments[i].CompanyId == id)
            {
                departmentExists = true;
                Console.WriteLine(AppDbContextSim.departments[i].ToString());
            }
        }
        if (!departmentExists)
        {
            throw new NonExistentEntityException("Department with given ID does not exist for this company");
        }
    }

    public void GetAllDepartmentsByName(string name)
    {
        bool compNameExists = false;
        for (int i = 0; i < indexCounter; i++)
        {
            if (AppDbContextSim.companies[i].Name.ToUpper() == name.ToUpper())
            {
                compNameExists = true;
                break;
            }
        }
        if (!compNameExists) { throw new NonExistentEntityException("This company does not exist"); }
        for (int i = 0; i < AppDbContextSim.departments.Length; i++)
        {
            if (AppDbContextSim.departments[0] is null) { throw new NonExistentEntityException("Departments do not exist for this Company!"); }
            else if (AppDbContextSim.departments[i] != null && AppDbContextSim.departments[i].CompanyName.ToUpper() == name.ToUpper())
            {
                Console.WriteLine(AppDbContextSim.departments[i].ToString());
            }
        }
    }
    public void RemoveCompany(int id)
    {
        bool compIdExists = false;
        for (int i = 0; i < AppDbContextSim.companies.Length; i++)
        {
            if (AppDbContextSim.companies[i] != null && AppDbContextSim.companies[i].Id == id)
            {
                compIdExists = true;

                AppDbContextSim.companies[i] = null;
                break;
            }
        }
        if (!compIdExists) { throw new NonExistentEntityException("This company does not exist"); }

        int indexMover = 0;
        for (int i = 0; i < AppDbContextSim.companies.Length; i++)
        {
            if (AppDbContextSim.companies[i] != null)
            {
                AppDbContextSim.companies[indexMover] = AppDbContextSim.companies[i];
                indexMover++;
            }
        }
        for (int i = indexMover; i < AppDbContextSim.companies.Length; i++)
        {
            AppDbContextSim.companies[i] = null;
        }
    }
}

