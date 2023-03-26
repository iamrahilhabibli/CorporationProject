
using Corporation.Core.Entities;
using Corporation.Infrastructure.DbContextSim;
using Corporation.Infrastructure.Services;
using Corporation.Infrastructure.Utilities;
using Corporation.Infrastructure.Utilities.Exceptions;

CompanyServices newCompany = new CompanyServices();
DepartmentServices newDepartment = new DepartmentServices();
//Department new_department = new Department();
EmployeeServices newEmployee = new EmployeeServices();


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
[12] Search for an employee by name
[13] Remove employee by unique ID");

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
                    catch (Exception ex) { Console.WriteLine(ex.Message); } // loop will continue
                }
                break;

            case (int)Helper.ConsoleMenu.GetCompanyList: // OK! CHECK THE GET ALL METHOD AGAIN!
                try { newCompany.GetAll(); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); }
                break;

            case (int)Helper.ConsoleMenu.CreateDepartment: // ok
            dep_name:
                Console.WriteLine("Enter Department Name");
                string departmentName = Console.ReadLine();
                if (!Helper.NameValidation(departmentName))
                {
                    Console.WriteLine("Department name can not be left blank, contain only integer and/or contain symbols");
                    goto dep_name;
                }
            employee_limit: // RENAME
                Console.WriteLine("Enter Employee Limit");
                int employeeLimit;
                string employeeLimitResponse = Console.ReadLine();
                if (!Helper.EmployeeLimitValidation(employeeLimitResponse, out employeeLimit)) { goto employee_limit; }
            company_Id:
                Console.WriteLine("Enter Company ID department belongs to: ");
                try { newCompany.GetAll(); } // If no companies exist  
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                int departmentCompanyId;
                string depCompIdResponse = Console.ReadLine();
                if (!Helper.IntTypeValidation(depCompIdResponse, out departmentCompanyId))
                {
                    Console.WriteLine("Incorrect format please enter an integer corresponding to your Companies unique ID");
                    goto company_Id;
                }
            company_Name:
                Console.WriteLine("Enter Company name department belongs to: ");
                string company_name = Console.ReadLine();
                if (!Helper.EntityNameValidation(company_name)) { Console.WriteLine("This section may not be left blank and/or Company name may not consist of only integers"); }
                try { newDepartment.Create(departmentName, employeeLimit, departmentCompanyId, company_name); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                break;

            case (int)Helper.ConsoleMenu.GetListOfAllDepartments:
                try { newDepartment.GetAll(); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                break;

            case (int)Helper.ConsoleMenu.GetListOfDepartmentsByCompanyID:
            listing_departments_byId:
                try { newCompany.GetAll(); Console.WriteLine("Enter the Unique ID of your Company to list all your departments"); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                int listById;
                string listByIdResponse = Console.ReadLine();
                if (!Helper.IntTypeValidation(listByIdResponse, out listById))
                {
                    Console.WriteLine("Incorrect format please enter an integer corresponding to your Companies unique ID");
                    goto listing_departments_byId;
                }
                try { newCompany.GetAllDepartmentsByID(listById); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); }
                break;

            case (int)Helper.ConsoleMenu.GetListOfDepartmentsByCompanyName:
            listing_departments_byName:
                try { newCompany.GetAll(); Console.WriteLine("Enter the name of your Company"); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); }
                try { string listByNameResponse = Console.ReadLine(); newCompany.GetAllDepartmentsByName(listByNameResponse); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); }
                break;

            case (int)Helper.ConsoleMenu.UpdateDepartment:
            company_response:
                try { newCompany.GetAll(); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                Console.WriteLine("Please input the name of your Company to modify department parameters");
                string companyResponse = Console.ReadLine();
                try { newCompany.GetAllDepartmentsByName(companyResponse); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
            update_department_id:
                Console.WriteLine("Enter Department ID you wish to modify");
                int updateByName;
                string updateResponse = Console.ReadLine();
                if (!Helper.IntTypeValidation(updateResponse, out updateByName))
                {
                    Console.WriteLine("Incorrect format for department Unique ID");
                    goto update_department_id;
                }
            new_department_name:
                Console.WriteLine("Enter new name for your Department");
                string newDepName = Console.ReadLine();
                if (!Helper.NameValidation(newDepName))
                {
                    Console.WriteLine("Department name can not be left blank, contain only integer and/or contain symbols");
                    goto new_department_name;
                }
            new_employee_limit:
                Console.WriteLine("Enter new employee limit");
                int newEmpLimit;
                string newEmpLimitResponse = Console.ReadLine();
                if (!Helper.EmployeeLimitValidation(newEmpLimitResponse, out newEmpLimit)) { goto new_employee_limit; }
                try { newDepartment.Update(updateByName, newDepName, newEmpLimit); Console.WriteLine("Department successfully updated!"); }
                catch (InvalidLimitException ex) { Console.WriteLine(ex.Message); goto new_employee_limit; }
                catch (IdenticalNameException ex) { Console.WriteLine(ex.Message); goto new_department_name; }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); goto update_department_id; }
                break;


            case (int)Helper.ConsoleMenu.AddEmployee: // ok
            name_response:
                Console.WriteLine("Enter employee name: ");
                string nameResponse = Console.ReadLine();
                if (!Helper.NameValidation(nameResponse)) { Console.WriteLine("Name field can not be empty,contain symbols or include digits"); goto name_response; }

            surname_response:
                Console.WriteLine("Enter employee surname");
                string surnameResponse = Console.ReadLine();
                if (!Helper.NameValidation(surnameResponse)) { Console.WriteLine("Surname field can not be empty, contain symbols or include digits"); goto surname_response; }

            salary_response:
                Console.WriteLine("Enter employee Salary");
                double doubleSalaryResponse;
                string salaryResponse = Console.ReadLine();
                if (!Helper.DoubleSalaryValidation(salaryResponse, out doubleSalaryResponse)) { goto salary_response; }

            employee_company:
                Console.WriteLine("Enter your company name: ");
                try { newCompany.GetAll(); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                string companyNameResponse = Console.ReadLine();
                if (!Helper.EntityNameValidation(companyNameResponse)) { Console.WriteLine("Company name field can not be empty,contain symbols that are not allowed or include digits"); goto employee_company; }
                try { newCompany.GetAllDepartmentsByName(companyNameResponse); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                Console.WriteLine();
            department_id_input:
                Console.WriteLine("Enter department ID: ");
                int departmentIdResponse;
                string depIdResponse = Console.ReadLine();
                if (!Helper.IntTypeValidation(depIdResponse, out departmentIdResponse)) { Console.WriteLine("You must enter a valid ID!"); goto department_id_input; }
                try { if (!newDepartment.doesDepartmentBelongToCompany(companyNameResponse, departmentIdResponse)) ; }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message); goto department_id_input;
                }

                try { newEmployee.Create(nameResponse, surnameResponse, doubleSalaryResponse, companyNameResponse, departmentIdResponse); Console.WriteLine("Employee Successfully added!"); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                catch (Corporation.Infrastructure.Utilities.Exceptions.CapacityLimitException ex) { Console.WriteLine(ex.Message); break; }
                break;

            case (int)Helper.ConsoleMenu.GetListOfAllEmployees:
                try { newEmployee.GetAll(); }
                catch (NullOrEmptyException ex) { Console.WriteLine(ex.Message); }
                break;

            case (int)Helper.ConsoleMenu.GetListOfEmployeesByDepID:
            company_id_user:
                Console.WriteLine("Enter Company ID: ");
                try { newCompany.GetAll(); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                int companyIdResponse;
                string compIdResponse = Console.ReadLine();
                if (!Helper.IntTypeValidation(compIdResponse, out companyIdResponse)) { Console.WriteLine("Please enter correct format for Company ID"); }
                try { newCompany.GetAllDepartmentsByID(companyIdResponse); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); goto company_id_user; }
            department_id_user:
                Console.WriteLine("Enter Department ID: ");
                int department_id_response;
                string dep_id_response = Console.ReadLine();
                if (!Helper.IntTypeValidation(dep_id_response, out department_id_response)) { Console.WriteLine("Please input valid Department Id "); goto department_id_user; }
                try { newEmployee.GetAllByDepartmentId(department_id_response); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); goto department_id_user; }
                break;

            case (int)Helper.ConsoleMenu.GetListOfEmployeesByCompanyName:
                try
                {
                    newCompany.GetAll(); Console.WriteLine("Enter your Company name");
                    string employeeCompanyNameResponse = Console.ReadLine();
                    newEmployee.GetAllByCompanyName(employeeCompanyNameResponse);
                }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                break;

            case (int)Helper.ConsoleMenu.SearchEmployeeByName:
            search_employee_name:
                try
                {
                    Console.WriteLine("Please enter the name or surname of the employee you are looking for.");
                    string searchResponse = Console.ReadLine();
                    newEmployee.GetByName(searchResponse);
                }
                catch (InvalidNameInput ex) { Console.WriteLine(ex.Message); goto search_employee_name; }
                catch (NotFound ex) { Console.WriteLine(ex.Message); break; }
                break;

            case (int)Helper.ConsoleMenu.RemoveEmployee:
            remove_employee_company:
                try { newCompany.GetAll(); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
                Console.WriteLine("Enter your company name");
                string removeEmployeeCompanyResponse = Console.ReadLine();
                if (!Helper.EntityNameValidation(removeEmployeeCompanyResponse)) { Console.WriteLine("Please enter correct format for company name"); goto remove_employee_company; }
                try { newCompany.GetAllDepartmentsByName(removeEmployeeCompanyResponse); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); break; }
            remove_employee_department:
                Console.WriteLine("Enter employees department ID");
                int removeEmployeeDepIdResponse;
                string remove_employee_dep_response = Console.ReadLine();
                if (!Helper.IntTypeValidation(remove_employee_dep_response, out removeEmployeeDepIdResponse)) { Console.WriteLine("Enter valid format for Department ID"); goto remove_employee_department; }
                try { newEmployee.GetAllByDepartmentId(removeEmployeeDepIdResponse); }
                catch (NonExistentEntityException ex) { Console.WriteLine(ex.Message); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            remove_employee_id_response:
                Console.WriteLine("Please provide the ID of the Employee that you want to remove.");
                int removeEmployeeIdResponse;
                string remove_employee_id_response = Console.ReadLine();
                if (!Helper.IntTypeValidation(remove_employee_id_response, out removeEmployeeIdResponse)) { Console.WriteLine("Please enter correct format for the Unique ID"); goto remove_employee_id_response; }
                try { newEmployee.RemoveEmployee(removeEmployeeIdResponse); Console.WriteLine("Employee Removed!1"); }
                catch (LastIndexFullException ex) { Console.WriteLine(ex.Message); break; }
                break;
        }
    }
    else
    {
        Console.WriteLine("Ensure that the input follows the correct format.");
    }
}