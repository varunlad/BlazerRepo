using EmpDataAccess;
using EmpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeDataAccess employeeDataAccess;
        public EmployeeService(IEmployeeDataAccess employeeDataAccess)
        {
            this.employeeDataAccess = employeeDataAccess;
        }
        public List<EmployeeModel> GetEmploess()
        {
            List<EmployeeModel> emps = employeeDataAccess.GetAllEmployee().ToList();
            return emps;
        }
        public string Create(EmployeeModel objCustomer)
        {
            employeeDataAccess.AddEmployee(objCustomer);
            return "Added Successfully";
        }
        public EmployeeModel GetEmployeeData(int id)
        {
            EmployeeModel employee = employeeDataAccess.GetEmployeeData(id);
            return employee;
        }
        public string DeleteEmployee(EmployeeModel employee)
        {
            employeeDataAccess.DeleteEmployee(employee.EmployeeId);
            return "Delete Successfully";
        }
        public string UpdateEmployee(EmployeeModel objEmp)
        {
            employeeDataAccess.UpdateEmployee(objEmp);
            return "Update Successful";
        }
    }
}
