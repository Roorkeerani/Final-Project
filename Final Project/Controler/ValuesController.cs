using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Final_Project.Models;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Controler
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // GET: api/<ValuesController>
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select First_name, Middle_name, Last_name, mob_no, E_mail, Password from pinfo";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post(Department dep)
        {


            string query1 = "insert into pinfo values(@firstname, @middlename, @lastname, @email, @mobile, @password)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query1, myCon))
                {
                    myCommand.Parameters.AddWithValue("@firstname", dep.First_name);
                    myCommand.Parameters.AddWithValue("@middlename", dep.Middle_name);
                    myCommand.Parameters.AddWithValue("@lastname", dep.Last_name);
                    myCommand.Parameters.AddWithValue("@email", dep.E_mail);
                    myCommand.Parameters.AddWithValue("@mobile", dep.mob_no);
                    myCommand.Parameters.AddWithValue("@password", dep.Password);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return Ok("Added Successfully");
        }



        // PUT api/<ValuesController>/5
        [HttpPut]
        public IActionResult Put(Department dep)
        {

            //string query = "insert into pinfo values(@firstname, @middlename, @lastname, @email, @mobile, @password)";
            string query = "update pinfo set First_name=@fn where id = @id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@fn", dep.First_name);
                    myCommand.Parameters.AddWithValue("@id", dep.id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return Ok("updated Successfully");
        }

        // DELETE api/<ValuesController>/5
        [HttpPost]
        [Route("LoginData")]
        public JsonResult LoginData(Login data)
        {
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;

            try
            {
                using (SqlConnection mycon = new SqlConnection(sqlDataSource))
                {
                    mycon.Open();
                    using (SqlCommand myCommand = new SqlCommand(Query.loginQuery, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@Email", data.Email);
                        myCommand.Parameters.AddWithValue("@password", data.Password);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new JsonResult(table.Rows.Count);

        }
    }
}