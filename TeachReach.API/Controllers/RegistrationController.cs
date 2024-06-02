using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using TeachReach.TeachReach.Domain.Entities;
using BCrypt.Net;
using TeachReach.TeachReach.Application.RequestModels.StudentRequestModels;
using TeachReach.TeachReach.Application.RequestModels.TeacherRequestModels;

namespace TeachReach.TeachReach.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("teacher-registration")]
        public ActionResult<string> TeacherRegistration(TeacherRegisterDto teacher)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(teacher.Password);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    // Insert into Teachers table
                    string insertTeacherQuery = @"
                        INSERT INTO Teachers(FirstName, LastName, Email, Password, Bio, City, PhoneNumber, Experience, DateOfBirth, HourlyRate, ProfilePictureUrl) 
                        VALUES(@FirstName, @LastName, @Email, @Password, @Bio, @City, @PhoneNumber, @Experience, @DateOfBirth, @HourlyRate, @ProfilePictureUrl); 
                        SELECT SCOPE_IDENTITY()";

                    SqlCommand cmd = new SqlCommand(insertTeacherQuery, con, transaction);
                    cmd.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", teacher.LastName);
                    cmd.Parameters.AddWithValue("@Email", teacher.Email);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    cmd.Parameters.AddWithValue("@Bio", (object)teacher.Bio ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", (object)teacher.City ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PhoneNumber", teacher.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Experience", teacher.Experience);
                    cmd.Parameters.AddWithValue("@DateOfBirth", teacher.DateOfBirth);
                    cmd.Parameters.AddWithValue("@HourlyRate", teacher.HourlyRate);
                    cmd.Parameters.AddWithValue("@ProfilePictureUrl", (object)teacher.ProfilePictureUrl ?? DBNull.Value);

                    int teacherId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Insert into TeacherSubject table
                    foreach (var subjectId in teacher.SubjectId)
                    {
                        cmd = new SqlCommand("INSERT INTO SubjectTeacher( SubjectsId, TeachersId) VALUES(@SubjectsId, @TeachersId)", con, transaction);
                        cmd.Parameters.AddWithValue("@SubjectsId", subjectId);
                        cmd.Parameters.AddWithValue("@TeachersId", teacherId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    return Ok("Data inserted");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest("Error: " + ex.Message);
                }
            }
        }



    [HttpPost]
        [Route("teacher-login")]
        public string TeacherLogin(TeacherLoginDto teacher)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT Password FROM Teachers WHERE Email = @Email", con);
            da.SelectCommand.Parameters.AddWithValue("@Email", teacher.Email);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                string hashedPasswordFromDB = dt.Rows[0]["Password"].ToString();

                if (BCrypt.Net.BCrypt.Verify(teacher.Password, hashedPasswordFromDB))
                {
                    return "Valid Teacher";
                }
            }

            return "Invalid Teacher";
        }


        [HttpPost]
        [Route("student-registration")]
        public string Registration(Student student)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(student.Password);

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Students(FirstName, LastName, Email, Password) VALUES(@FirstName, @LastName, @Email, @Password)", con);

            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
            cmd.Parameters.AddWithValue("@LastName", student.LastName);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return "Data inserted";
            }
            else
            {
                return "Error";
            }
        }

        [HttpPost]
        [Route("student-login")]
        public string login(StudentLoginDto student)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT Password FROM Students WHERE Email = @Email", con);
            da.SelectCommand.Parameters.AddWithValue("@Email", student.Email);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                string hashedPasswordFromDB = dt.Rows[0]["Password"].ToString();

                if (BCrypt.Net.BCrypt.Verify(student.Password, hashedPasswordFromDB))
                {
                    return "Valid User";
                }
            }

            return "Invalid User";
        }
    }
}
