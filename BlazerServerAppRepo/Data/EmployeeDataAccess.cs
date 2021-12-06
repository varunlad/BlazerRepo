using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlazerServerAppRepo.Data
{
    public class EmployeeDataAccess
    {
        string connectionString = "server=localhost;User=root;password=sujata@10;database=Emp_Wage_DB";
        public IEnumerable<EmployeeModel> GetAllEmployee()
        {
            List<EmployeeModel> lstCustomer = new List<EmployeeModel>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("sp_GetAllEmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    EmployeeModel employee = new EmployeeModel();
                    employee.EmployeeId = Convert.ToInt32(rdr["Empid"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.ProfileImage = rdr["ProfileImage"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                    employee.StartDate = (DateTime)(rdr["StartDate"] == DBNull.Value ? default(DateTime) : rdr["StartDate"]);
                    employee.Notes = rdr["Note"].ToString();
                    lstCustomer.Add(employee);
                }
                con.Close();
            }
            return lstCustomer;
        }
        public void AddEmployee(EmployeeModel model)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("sp_AddEmp", con);
                //Ename is requred
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ename", model.Name);
                cmd.Parameters.AddWithValue("@epath", model.ProfileImage);
                cmd.Parameters.AddWithValue("@egender", model.Gender);
                cmd.Parameters.AddWithValue("@edept", model.Department);
                cmd.Parameters.AddWithValue("@salary", model.Salary);
                cmd.Parameters.AddWithValue("@startdate", model.StartDate);
                cmd.Parameters.AddWithValue("@notes", model.Notes);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public EmployeeModel GetEmployeeData(int? id)
        {
            EmployeeModel employee = new EmployeeModel();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {

                MySqlCommand cmd = new MySqlCommand("sp_GetEmpByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@eID", id);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(rdr["Empid"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.ProfileImage = rdr["ProfileImage"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                    employee.StartDate = (DateTime)(rdr["StartDate"] == DBNull.Value ? default(DateTime) : rdr["StartDate"]);
                    employee.Notes = rdr["Note"].ToString();
                }
            }
            return employee;
        }
        //To Delete the record on a particular Customer  
        public void DeleteEmployee(int? empid)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("sp_DeleteEmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employeeid", empid);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
