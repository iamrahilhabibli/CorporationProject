using System.Data;
using Corporation.Core.Entities;
using Corporation.Infrastructure.DbContextSim;
using Corporation.Infrastructure.Services;
using Corporation.Infrastructure.Utilities;
using Corporation.Infrastructure.Utilities.Exceptions;

CompanyServices newCompany = new CompanyServices();
DepartmentServices newDepartment = new DepartmentServices();
//Department new_department = new Department();
EmployeeServices newEmployee = new EmployeeServices();
Employee new_employee = new Employee();

while (true)
{
    Console.WriteLine("Please select an option");
    Console.WriteLine();
    Console.WriteLine(@"
[0] Exit
[1] Create a new company
[2] View all companies
[3] Create a new department
[4] View all departments
[5] View departments by company ID
[6] View departments by company name
[7] Update a department
[8] Add a new employee
[9] View all employees
[10] View employees by department ID
[11] View employees by company name
[12] Search for an employee by name");

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

            case (int)Helper.ConsoleMenu.CreateCompany: // OK!
                bool companyCreated = false;
                while (!companyCreated) // repeat until company is successfully created
                {
                    try
                    {
                        Console.WriteLine("Enter Company name: ");
                        string companyName = Console.ReadLine();
                        newCompany.Create(companyName);
                        Console.WriteLine($"\nCongratulations {companyName} has been created!\n");
                        companyCreated = true; // to exit the loop
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); } // loop will contin
                }
                break;

            case (int)Helper.ConsoleMenu.GetCompanyList: // OK! CHECK THE GET ALL METHOD AGAIN!
                try { newCompany.GetAll(); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                break;

            case (int)Helper.ConsoleMenu.CreateDepartment: // LAST INSPECTION STILL REQUIRED!
            dep_name:
                Console.WriteLine("Enter Department Name");
                string departmentName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departmentName) || departmentName.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(departmentName, "[^a-zA-Z0-9 -]"))
                {
                    Console.WriteLine("Department name can not be left blank, contain only integer and/or contain symbols");
                    goto dep_name;
                }
            employee_limit: // RENAME
                Console.WriteLine("Enter Employee Limit");
                int employeeLimit;
                string employeeLimitResponse = Console.ReadLine();
                bool employeeInLimitRange = int.TryParse(employeeLimitResponse, out employeeLimit);
                if (!employeeInLimitRange)
                {
                    Console.WriteLine("The limit range can only contain integer");
                    goto employee_limit;
                }
                else if (employeeLimit <= 0)
                {
                    Console.WriteLine("Employee limit can not be set to zero or negative");
                    goto employee_limit;
                }
            company_Id:
                Console.WriteLine("Enter Company ID department belongs to: ");
                try { newCompany.GetAll(); } // If no companies exist  
                catch (NullOrEmptyException ex) { Console.WriteLine(ex.Message); break; }

                int departmentCompanyId;
                string depCompIdResponse = Console.ReadLine();
                if (!int.TryParse(depCompIdResponse, out departmentCompanyId))
                {
                    Console.WriteLine("Incorrect format please enter an integer corresponding to your Companies unique ID");
                    goto company_Id;
                }
            company_Name:
                Console.WriteLine("Enter Company name department belongs to: ");
                string company_name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(company_name) || company_name.All(char.IsDigit))
                {
                    Console.WriteLine("This section may not be left blank and/or Company name may not consist of only integers");
                    goto company_Name;
                }
                try { newDepartment.Create(departmentName, employeeLimit, departmentCompanyId, company_name); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); goto company_Id; }
                break;

            case (int)Helper.ConsoleMenu.GetListOfAllDepartments: // EXCEPTION MUST BE THROWN IF NO DEPARTMENTS EXIST
                try { newDepartment.GetAll(); }
                catch (NullOrEmptyException ex) { Console.WriteLine(ex.Message); }
                break;

            case (int)Helper.ConsoleMenu.GetListOfDepartmentsByCompanyID: // LOOKS OK! FURTHER INSPECTION REQUIRED!
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

            case (int)Helper.ConsoleMenu.GetListOfDepartmentsByCompanyName: // LOOKS OK! FURTHER INSPECTION REQUIRED!
            lisiting_departments_byName:
                Console.WriteLine("Enter the name of your Company");
                try
                {
                    newCompany.GetAll();
                    string listByNameResponse = Console.ReadLine();
                    newCompany.GetAllDepartmentsByName(listByNameResponse);
                }
                catch (NullOrEmptyException ex) { Console.WriteLine(ex.Message); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); goto lisiting_departments_byName; }
                break;

            case (int)Helper.ConsoleMenu.UpdateDepartment: // NOT FINISHED INPUT VALIDATION MUST BE CHECKED
            company_response:
                try
                {
                    newCompany.GetAll();
                    Console.WriteLine("Please input the name of your Company to modify department parameters");
                    string companyResponse = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(companyResponse) || companyResponse.All(char.IsDigit))
                    {
                        Console.WriteLine("Please enter company name in a valid format");
                    }
                    newCompany.GetAllDepartmentsByName(companyResponse);
                }
                catch (NullOrEmptyException ex) { Console.WriteLine(ex.Message); break; }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); goto company_response; }
            update_department_id:
                Console.WriteLine("Enter Department ID you wish to modify");
                int updateByName;
                string updateResponse = Console.ReadLine();
                if (!int.TryParse(updateResponse, out updateByName))
                {
                    Console.WriteLine("Enter correct format");
                }

            new_department_name:
                Console.WriteLine("Enter new Name for your Department");
                string newDepName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newDepName) || newDepName.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(newDepName, "[^a-zA-Z0-9 -]"))
                {
                    Console.WriteLine("Department name can not be left blank, contain only integer and/or contain symbols");
                    goto new_department_name;
                }
            new_employee_limit:
                Console.WriteLine("Enter new employee limit");
                int newEmpLimit;
                string newEmpLimitResponse = Console.ReadLine();
                bool isEmpLimitValid = int.TryParse(newEmpLimitResponse, out newEmpLimit);
                if (!isEmpLimitValid)
                {
                    Console.WriteLine("Employee limit can only contain integers");
                    goto new_employee_limit;
                }
                else if (newEmpLimit <= 0)
                {
                    Console.WriteLine("Employee limit can not be set to zero or negative");
                    goto new_employee_limit;
                }
                try { newDepartment.Update(updateByName, newDepName, newEmpLimit); Console.WriteLine("Department successfully updated!"); }
                catch (IdenticalNameException ex) { Console.WriteLine(ex.Message); goto new_department_name; }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); goto update_department_id; }
                break;


            case (int)Helper.ConsoleMenu.AddEmployee:
            name_response:
                Console.WriteLine("Enter employee name: ");
                string nameResponse = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nameResponse) || nameResponse.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(nameResponse, "[^a-zA-Z0-9 -]"))
                {
                    Console.WriteLine("Name field can not be empty,contain symbols or include digits");
                    goto name_response;
                }
            surname_response:
                Console.WriteLine("Enter employee surname");
                string surnameResponse = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(surnameResponse) || surnameResponse.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(surnameResponse, "[^a-zA-Z0-9 -]"))
                {
                    Console.WriteLine("Surname field can not be empty,contain symbols or include digits");
                    goto surname_response;
                }
            salary_response:
                Console.WriteLine("Enter employee Salary");
                double doubleSalaryResponse;
                string salaryResponse = Console.ReadLine();
                if (!double.TryParse(salaryResponse, out doubleSalaryResponse) || doubleSalaryResponse < 0)
                {
                    Console.WriteLine("Incorrect format for Salary"); // show the correct format
                    goto salary_response;
                }
            employee_company:
                Console.WriteLine("Enter your company name: ");
                newCompany.GetAll();
                string companyNameResponse = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(companyNameResponse) || companyNameResponse.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(companyNameResponse, "[^a-zA-Z0-9 &-]"))
                {
                    Console.WriteLine("Company name field can not be empty,contain symbols that are not allowed or include digits");
                    goto employee_company;
                }
                newCompany.GetAllDepartmentsByName(companyNameResponse);
                Console.WriteLine();
                Console.WriteLine("Enter department ID: ");
                int departmentIdResponse;
                string depIdResponse = Console.ReadLine();
                if (!int.TryParse(depIdResponse, out departmentIdResponse))
                {
                    Console.WriteLine("You must enter unique ID of the department from the list");
                }
                try
                {
                    newEmployee.Create(nameResponse, surnameResponse, doubleSalaryResponse, companyNameResponse, departmentIdResponse);
                    Console.WriteLine("Employee Successfully added!");
                }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); goto employee_company; }
                catch (Corporation.Infrastructure.Utilities.Exceptions.CapacityLimitException ex) { Console.WriteLine(ex.Message); break; }
                break;

            case (int)Helper.ConsoleMenu.GetListOfAllEmployees:
                try
                {
                    newEmployee.GetAll();
                }
                catch (NullOrEmptyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case (int)Helper.ConsoleMenu.GetListOfEmployeesByDepID: // needs to throw exception No employees have been added to dep
                Console.WriteLine("Enter Company ID: ");
                newCompany.GetAll();
                int companyIdResponse;
                string compIdResponse = Console.ReadLine();
                if (!int.TryParse(compIdResponse, out companyIdResponse))
                {
                    Console.WriteLine("Choose a valid Company ID");
                }
                Console.WriteLine("Enter Department ID: ");
                newCompany.GetAllDepartmentsByID(companyIdResponse);
                int department_id_response;
                string dep_id_response = Console.ReadLine();
                if (!int.TryParse(dep_id_response, out department_id_response))
                {
                    Console.WriteLine("Choose valid Department ID");
                }

                newEmployee.GetAllByDepartmentId(department_id_response);
                break;

            case (int)Helper.ConsoleMenu.GetListOfEmployeesByCompanyName:
                Console.WriteLine("Enter your Company name");
                newCompany.GetAll();
                string employeeCompanyNameResponse = Console.ReadLine();
                newEmployee.GetAllByCompanyName(employeeCompanyNameResponse);
                break;

            case (int)Helper.ConsoleMenu.SearchEmployeeByName: // PUT THIS IN TRY CATCH AND ADD EXCEPTIONS
            search_employee_name:
                try
                {
                    Console.WriteLine("Please enter the name or surname of the employee you are looking for.");
                    string searchResponse = Console.ReadLine();
                    newEmployee.GetByName(searchResponse);
                }
                catch (InvalidNameInput ex) { Console.WriteLine(ex.Message); goto search_employee_name; }
                break;
        }
    }
    else
    {
        Console.WriteLine("Ensure that the input follows the correct format.");
    }
}