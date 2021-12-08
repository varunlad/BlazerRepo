using EmpModel;
using System.Collections.Generic;

namespace EmployeeServices
{
    public interface IEmployeeService
    {
        string Create(EmployeeModel objCustomer);
        string DeleteEmployee(EmployeeModel employee);
        List<EmployeeModel> GetEmploess();
        EmployeeModel GetEmployeeData(int id);
        string UpdateEmployee(EmployeeModel objEmp);
    }
}