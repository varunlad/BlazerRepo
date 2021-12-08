using EmpModel;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EmpDataAccess
{
    public class EmployeeDataAccess : IEmployeeDataAccess
    {

        private readonly IConfiguration _config;
        public string connectionString { get; set; } = "Emp_Wage_DB";
        public EmployeeDataAccess(IConfiguration config)
        {
            _config = config;
        }
        //string connectionString = "server=localhost;User=root;password=sujata@10;database=Emp_Wage_DB";

        public IEnumerable<EmployeeModel> GetAllEmployee()
        {
            string ConnectionStrings = _config.GetConnectionString(connectionString);
            List<EmployeeModel> lstCustomer = new List<EmployeeModel>();
            using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
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
            string ConnectionStrings = _config.GetConnectionString(connectionString);
            using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
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
            string ConnectionStrings = _config.GetConnectionString(connectionString);
            using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
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
            string ConnectionStrings = _config.GetConnectionString(connectionString);
            using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
            {
                MySqlCommand cmd = new MySqlCommand("sp_DeleteEmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employeeid", empid);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UpdateEmployee(EmployeeModel Emp)
        {
            string ConnectionStrings = _config.GetConnectionString(connectionString);
            using (MySqlConnection con = new MySqlConnection(ConnectionStrings))
            {
                MySqlCommand cmd = new MySqlCommand("sp_EditEmp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eId", Emp.EmployeeId);
                cmd.Parameters.AddWithValue("@ename", Emp.Name);
                cmd.Parameters.AddWithValue("@esalary", Emp.Salary);
                cmd.Parameters.AddWithValue("@estartdate", Emp.StartDate);
                cmd.Parameters.AddWithValue("@enote", Emp.Notes);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
