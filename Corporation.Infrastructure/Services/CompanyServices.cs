namespace Corporation.Infrastructure.Services;

using Corporation.Core.Entities;
using Corporation.Infrastructure.DbContextSim;
using Corporation.Infrastructure.Utilities.Exceptions;

public class CompanyServices
{
    private static int indexCounter = 0;

    public void Create(string companyName)
    {
        if (String.IsNullOrWhiteSpace(companyName))
        {
            throw new NullOrEmptyException("The company name must contain at least one character and cannot be left empty");
        }
        else if (companyName.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(companyName, "[^a-zA-Z0-9]"))
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
        for (int i = 0; i < indexCounter; i++)
        {
            Console.WriteLine($"Company ID: {AppDbContextSim.companies[i].Id}, Company Name: {AppDbContextSim.companies[i].Name}");
        }
    }
}

