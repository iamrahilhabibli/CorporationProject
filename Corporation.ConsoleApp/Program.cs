using Corporation.Core.Entities;
using Corporation.Infrastructure.Services;
using Corporation.Infrastructure.Utilities;
using Corporation.Infrastructure.Utilities.Exceptions;

CompanyServices newCompany = new CompanyServices();
DepartmentServices newDepartment = new DepartmentServices();

while (true)
{
    Console.WriteLine("WELCOME! Please select your option");
    Console.WriteLine("**********************************");
    Console.WriteLine("\n 0 - Exit \n 1 - To Create a Company \n 2 - Get list of Companies \n 3 - Create Department \n 4 - Get list of Departments ");
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
                    Console.WriteLine($"Congratulations {companyName} has been created!");
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
                Console.WriteLine("Enter Company ID");
                newCompany.GetAll();
                int departmentCompanyId = int.Parse(Console.ReadLine());
                newDepartment.Create(departmentName, employeeLimit, departmentCompanyId);
                break;

            case (int)Helper.ConsoleMenu.GetDepartmentsList:
                newDepartment.GetAll();
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