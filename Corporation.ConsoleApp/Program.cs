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
    Console.WriteLine("0 - Exit \n1 - To Create a Company \n2 - Get list of Companies \n3 - Create Department \n4 - Get list of Departments by Company ID \n5 - Get list of Departments by Company Name \n6 - Update Department");
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
                if (string.IsNullOrWhiteSpace(departmentName) || departmentName.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(departmentName, "[^a-zA-Z0-9 ]"))
                {
                    Console.WriteLine("Department name can not be left blank, contain only integer and/or contain symbols");
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

            case (int)Helper.ConsoleMenu.UpdateDepartment:
                Console.WriteLine("Please input the name of your Company to modify department parameters");
                string companyResponse = Console.ReadLine();
                newCompany.GetAllDepartmentsByName(companyResponse);
                Console.WriteLine("Enter Department ID you wish to modify");
                int updateByName;
                string updateResponse = Console.ReadLine();
                if (!int.TryParse(updateResponse, out updateByName))
                {
                    Console.WriteLine("Enter correct format");
                }
                Console.WriteLine("Enter new Name for your Department");
                string newDepName = Console.ReadLine();
                Console.WriteLine("Enter new emp limit");
                int newEmpLimit = int.Parse(Console.ReadLine());
                newDepartment.Update(updateByName, newDepName, newEmpLimit);
                break;
        }
    }
    else
    {
        Console.WriteLine("Ensure that the input follows the correct format.");
    }
}