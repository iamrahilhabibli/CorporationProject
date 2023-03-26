using System.Xml.Linq;

namespace Corporation.Infrastructure.Utilities;

public static class Helper
{
    public enum ConsoleMenu
    {
        Exit,
        CreateCompany,
        GetCompanyList,
        CreateDepartment,
        GetListOfAllDepartments,
        GetListOfDepartmentsByCompanyID,
        GetListOfDepartmentsByCompanyName,
        UpdateDepartment,
        AddEmployee,
        GetListOfAllEmployees,
        GetListOfEmployeesByDepID,
        GetListOfEmployeesByCompanyName,
        SearchEmployeeByName,
        RemoveEmployee,
    };
    public static bool NameValidation(string nameResponse)
    {
        if (string.IsNullOrWhiteSpace(nameResponse) || nameResponse.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(nameResponse, "[^a-zA-Z0-9 -]"))
        {
            return false;
        }
        return true;
    }
    public static bool EmployeeLimitValidation(string employeeLimitResponse, out int employeeResponse)
    {
        if (!int.TryParse(employeeLimitResponse, out employeeResponse) || employeeResponse <= 0)
        {
            Console.WriteLine("Employee limit can only contain integers and can not be set to or lower than zero");
            return false;
        }
        return true;
    }
    public static bool DoubleSalaryValidation(string userResponse, out double response)
    {
        if (!double.TryParse(userResponse, out response) || response < 0)
        {
            Console.WriteLine("Incorrect format for Salary"); // show the correct format
            return false;
        }
        return true;
    }
    public static bool EntityNameValidation(string entityName)
    {
        if (string.IsNullOrWhiteSpace(entityName) || entityName.All(char.IsDigit) || System.Text.RegularExpressions.Regex.IsMatch(entityName, "[^a-zA-Z0-9 &-]"))
        {
            return false;
        }
        return true;
    }
    public static bool IntTypeValidation(string userResponse, out int response)
    {
        if (!int.TryParse(userResponse, out response))
        {
            return false;
        }
        return true;
    }
}

