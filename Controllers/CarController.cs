using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Car_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private string connectionString = string.Empty;
        public CarController()
        {
            connectionString = "Server=BHAVNAWKS727;Database=Car;Trusted_Connection= True";
        }

        // GET: api/<CarController>
        [HttpGet]
        public IActionResult Get()
        {
            using var con = new SqlConnection(connectionString);
            con.Open();
            var cars = con.Query<Car>("select * from Cars").ToList();
            return Ok(cars);
        }

        // GET api/<CarController>/5
        [HttpGet("{Id}")]
        public IActionResult Get(string Id)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();
        
            var cars = con.Query<Car>($"select * from Cars where Id={Id}").ToList();
            return Ok(cars);
        }


        // POST api/<CarController>
        [HttpPost]
        public IActionResult Post([FromBody] viewModelCar value)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();

            var Query = "insert into Cars(Name, Price) values(@name, @price)";
            var dp = new DynamicParameters();
            dp.Add("@name", value.Name, System.Data.DbType.AnsiString, System.Data.ParameterDirection.Input, 255);
            dp.Add("@price", value.Price);
            int res = con.Execute(Query, dp);
            if (res > 0)
            {
                return Ok("Inserted successfully");
            }
            return Ok("Not Inserted");
        }

        // PUT api/<CarController>/5
        [HttpPut("{id}/{price}")]
        public IActionResult Put(int id, int price)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();

            var cars = con.Query<Car>($"Update Cars set Price={price} where Id={id}").ToList();
            return Ok("Updated successfully");
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();

            var cars = con.Query<Car>($"delete from Cars where Id={id}").ToList();
            return Ok("deleted successfully");
        }
    }
}
