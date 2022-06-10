using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Final_Project.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Controler
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdataController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EdataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select userid, email, date, income, income_name, expense, expense_name from income_exp";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon1 = new SqlConnection(sqlDataSource))
            {
                myCon1.Open();
                using (SqlCommand myCommand1 = new SqlCommand(query, myCon1))
                {
                    myReader = myCommand1.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon1.Close();
                }
            }
            return new JsonResult(table);
        }


        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post(Edata ed)
        {


            string query1 = "insert into income_exp values(@userid, @email, @date, @income, @incomename, @expense, @expensename)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommands = new SqlCommand(query1, myCon))
                {
                    myCommands.Parameters.AddWithValue("@userid", ed.userid);
                    myCommands.Parameters.AddWithValue("@email", ed.email);
                    myCommands.Parameters.AddWithValue("@date", ed.date);
                    myCommands.Parameters.AddWithValue("@income", ed.income);
                    myCommands.Parameters.AddWithValue("@incomename", ed.income_name);
                    myCommands.Parameters.AddWithValue("@expense", ed.expense);
                    myCommands.Parameters.AddWithValue("@expensename", ed.expense_name);

                    myReader = myCommands.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return Ok("Added Successfully");
        }

        // PUT api/<ValuesController>/5

    }
}
