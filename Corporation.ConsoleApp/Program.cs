using Corporation.Core.Entities;
using Corporation.Infrastructure.DbContextSim;
using Corporation.Infrastructure.Services;
using Corporation.Infrastructure.Utilities;
using Corporation.Infrastructure.Utilities.Exceptions;

CompanyServices newCompany = new CompanyServices();
DepartmentServices newDepartment = new DepartmentServices();

while (true)
{
    Console.WriteLine("WELCOME! Please select your option");
    Console.WriteLine("0 - Exit \n1 - To Create a Company \n2 - Get list of Companies \n3 - Create Department \n4 - Get list of Departments by Company ID \n5- Get list of Departments by Company Name");
    int menuItems;
    string? userRes = Console.ReadLine();
    bool response = int.TryParse(userRes, out menuItems);

    if (response)
    {
        switch (menuItems)
        {
            case (int)Helper.ConsoleMenu.Exit:
                Environment.Exit(0);
                break;

            case (int)Helper.ConsoleMenu.CreateCompany:
                try
                {
                    Console.WriteLine("Enter Company name: ");
                    string companyName = Console.ReadLine();
                    newCompany.Create(companyName);
                    Console.WriteLine($"\nCongratulations {companyName} has been created!\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case (int)Helper.ConsoleMenu.GetCompanyList:
                newCompany.GetAll();
                break;

            case (int)Helper.ConsoleMenu.CreateDepartment:
            dep_name:
                Console.WriteLine("Enter Department Name");
                string departmentName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departmentName) || departmentName.All(char.IsDigit))
                {
                    Console.WriteLine("Department name can not be left blank or contain only integer");
                    goto dep_name;
                }
            limit_range:
                Console.WriteLine("Enter Employee Limit");
                int employeeLimit;
                string employeeLimitResponse = Console.ReadLine();
                bool employeeInLimitRange = int.TryParse(employeeLimitResponse, out employeeLimit);
                if (!employeeInLimitRange)
                {
                    Console.WriteLine("The limit range can only contain integer");
                    goto limit_range;
                }
            company_Id:
                Console.WriteLine("Enter Company ID department belongs to: ");
                newCompany.GetAll();
                int departmentCompanyId;
                string depCompIdResponse = Console.ReadLine();
                if (!int.TryParse(depCompIdResponse, out departmentCompanyId))
                {
                    Console.WriteLine("Incorrect format please enter an integer corresponding to your Companies unique ID");
                    goto company_Id;
                }
            company_Name:
                Console.WriteLine("Enter Company name department belongs to: ");
                string company_Name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(company_Name) || company_Name.All(char.IsDigit))
                {
                    Console.WriteLine("This section may not be left blank and/or Company name may not consist of only integers");
                    goto company_Name;
                }
                newDepartment.Create(departmentName, employeeLimit, departmentCompanyId, company_Name);
                break;

            case (int)Helper.ConsoleMenu.GetListOfDepartmentsByID:
            listing_departments_byId:
                Console.WriteLine("Enter the Unique ID of your Company to list all your departments");
                newCompany.GetAll();
                int listById;
                string listByIdResponse = Console.ReadLine();
                if (!int.TryParse(listByIdResponse, out listById))
                {
                    Console.WriteLine("Incorrect format please enter an integer corresponding to your Companies unique ID");
                    goto listing_departments_byId;
                }
                newCompany.GetAllDepartmentsByID(listById);
                break;
            case (int)Helper.ConsoleMenu.GetListOfDepartmentsByName:
            lisiting_departments_byName:
                Console.WriteLine("Enter the name of your Company");
                newCompany.GetAll();
                string listByNameResponse = Console.ReadLine();
                newCompany.GetAllDepartmentsByName(listByNameResponse);
                break;
            default:
                Console.WriteLine("Please select a valid option from the provided menu.");
                break;
        }
    }
    else
    {
        Console.WriteLine("Ensure that the input follows the correct format.");
    }
}