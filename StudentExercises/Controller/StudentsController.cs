using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace StudentExercises.Controller
{
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public StudentsController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        /*
         * We're going to re-use the GET all method for Students because if a user wants to see 
           *all* the students with a specific cohortId, it should still trigger the GET *all* method.
         
         *[FromQuery]
            - Lets the GET method know that the parameter 'cohortId' is coming from a query string parameter in the URL

         *WHERE 1=1
            - Our first SQL statement needs to have a WHERE clause so we can add more SQL to the first statement
            - The easiest thing to do to make sure that the WHERE clause is always true, 
              is to say WHERE 1=1 (because 1 will always equal 1).

         *The Conditionals
            - If the 'cohortId' from the query string parameter is NOT null, then add the AND SQL statement 
              to our first SQL statement and create a SQL parameter for the cohortId.
                - The next conditional is exactly the same, but for the 'lastName' query string parameter.
        */
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int? cohortId, [FromQuery]string lastName)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                        Id, 
                                        FirstName, 
                                        LastName, 
                                        SlackHandle, 
                                        CohortId 
                                        FROM Student
                                        WHERE 1=1";

                    if (cohortId != null)
                    {
                        cmd.CommandText += " AND CohortId = @cohortId";
                        cmd.Parameters.Add(new SqlParameter("@cohortId", cohortId));
                    }

                    if (lastName != null)
                    {
                        cmd.CommandText += " AND LastName LIKE @lastName";
                        cmd.Parameters.Add(new SqlParameter("@lastName", "%" + lastName + "%"));
                    }



                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Student> allStudents = new List<Student>();

                    while (reader.Read())
                    {
                        Student stu = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            CohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                        };

                        allStudents.Add(stu);
                    }
                    reader.Close();

                    return Ok(allStudents);
                }
            }
        }
    }
}
