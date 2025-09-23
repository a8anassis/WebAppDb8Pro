using Microsoft.Data.SqlClient;
using WebAppDb.Models;
using WebAppDb.Services.DBHelper;

namespace WebAppDb.DAO
{
    public class StudentDAOImpl : IStudentDAO
    {
        public Student? Insert(Student student)
        {
            Student? studentToReturn = null;
            int insertedId = 0;

            string sql1 = "INSERT INTO Students (Firstname, Lastname) VALUES (@firstname, @lastname); " +
                "SELECT SCOPE_IDENTITY();";

         
            using SqlConnection connection = DButil.GetConnection();
            connection.Open();

            using SqlCommand command1 = new(sql1, connection);

            command1.Parameters.AddWithValue("@firstname", student.Firstname);
            command1.Parameters.AddWithValue("@lastname", student.Lastname);

            var insertedObject = command1.ExecuteScalar();
            if (insertedObject != null)
            {
                if (!int.TryParse(insertedObject.ToString(), out insertedId))
                {
                    throw new Exception("Error in inserted id");
                }
            }

            string sql2 = "SELECT * FROM Students WHERE Id = @studentId";
            using SqlCommand command2 = new(sql2, connection);
            command2.Parameters.AddWithValue("@studentId", insertedId);

            using SqlDataReader reader = command2.ExecuteReader();
            if (reader.Read())
            {
                studentToReturn = new Student()
                {
                    Id = (int)reader["Id"],
                    Firstname = (string)reader["Firstname"],
                    Lastname = (string)reader["Lastname"]
                };
            }
            return studentToReturn;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public Student? GetById(int id)
        {
            throw new NotImplementedException();
        }

        

        public void Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
