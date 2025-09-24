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

        public void Update(Student student)
        {
            string sql = "UPDATE Students SET Firstname = @firstname, Lastname = @lastname WHERE Id = @id";

            using SqlConnection connection = DButil.GetConnection();
            connection.Open();

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@firstname", student.Firstname);
            command.Parameters.AddWithValue("@lastname", student.Lastname);
            command.Parameters.AddWithValue("@id", student.Id);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Students WHERE Id = @id";

            using SqlConnection connection = DButil.GetConnection();
            connection.Open();

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public Student? GetById(int id)
        {
            Student? studentToReturn = null;
            string sql = "SELECT * FROM Students WHERE Id = @id";

            using SqlConnection connection = DButil.GetConnection();
            connection.Open();

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            using SqlDataReader reader = command.ExecuteReader();

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

        public List<Student> GetAll()
        {
            string sql = "SELECT * FROM Students";
            List<Student> students = [];

            using SqlConnection connection = DButil.GetConnection();
            connection.Open();
            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                Student student = new()
                {
                    Id = (int)reader["Id"],
                    Firstname = (string)reader["Firstname"],
                    Lastname = (string)reader["Lastname"]
                };
                students.Add(student);
            }
            return students;
        }

        

        

        
    }
}
