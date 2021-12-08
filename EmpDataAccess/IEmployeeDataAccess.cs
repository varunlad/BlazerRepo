using EmpModel;
using System.Collections.Generic;

namespace EmpDataAccess
{
    public interface IEmployeeDataAccess
    {
        string connectionString { get; set; }

        void AddEmployee(EmployeeModel model);
        void DeleteEmployee(int? empid);
        IEnumerable<EmployeeModel> GetAllEmployee();
        EmployeeModel GetEmployeeData(int? id);
        void UpdateEmployee(EmployeeModel Emp);
    }
}