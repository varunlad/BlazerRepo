using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazerServerAppRepo.Data
{
    public class EmployeeServices
    {
        EmployeeDataAccess empDAL = new EmployeeDataAccess();
        public List<EmployeeModel> GetEmploess()
        {
            List<EmployeeModel> emps = empDAL.GetAllEmployee().ToList();
            return emps;
        }
        public string Create(EmployeeModel objCustomer)
        {
            empDAL.AddEmployee(objCustomer);
            return "Added Successfully";
        }
    }
}
