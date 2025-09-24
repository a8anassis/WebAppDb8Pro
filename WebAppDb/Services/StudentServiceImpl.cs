using AutoMapper;
using System.Transactions;
using WebAppDb.DAO;
using WebAppDb.DTO;
using WebAppDb.Exceptions;
using WebAppDb.Models;

namespace WebAppDb.Services
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly IStudentDAO studentDAO;
        private readonly IMapper mapper;
        private readonly ILogger<StudentServiceImpl> logger;

        public StudentServiceImpl(IStudentDAO studentDAO, IMapper mapper, ILogger<StudentServiceImpl> logger)
        {
            this.studentDAO = studentDAO;
            this.mapper = mapper;
            this.logger = logger;
        }

        public StudentReadOnlyDTO? InsertStudent(StudentInsertDTO studentInsertDTO)
        {
            StudentReadOnlyDTO studentReadOnlyDTO;
            try
            {
                using TransactionScope scope = new TransactionScope();
                Student student = mapper.Map<Student>(studentInsertDTO);
                Student? insertedStudent = studentDAO.Insert(student);
                studentReadOnlyDTO = mapper.Map<StudentReadOnlyDTO>(insertedStudent);
                logger.LogInformation("Student {Firstname} {Lastname} inserted successfully",
                    studentInsertDTO.Firstname, studentInsertDTO.Lastname);
                scope.Complete();
                return studentReadOnlyDTO;
            }
            catch (TransactionException ex)
            {
                logger.LogError("Student Insertion failed for {Firstname} {Lastname}. {ErrorMessage}",
                    studentInsertDTO.Firstname, studentInsertDTO.Lastname, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError("Student {Firstname} {Lastname} not inserted. {ErrorMessage}",
                                    studentInsertDTO.Firstname, studentInsertDTO.Lastname, ex.Message);
                throw;
            }
        }

        public void UpdateStudent(StudentUpdateDTO studentUpdateDTO)
        {
            try
            {
                using TransactionScope scope = new TransactionScope();

                if (studentDAO.GetById(studentUpdateDTO.Id) == null)
                {
                    throw new StudentNotFoundException($"Student with id {studentUpdateDTO.Id} not found.");
                }
                Student student = mapper.Map<Student>(studentUpdateDTO);
                studentDAO.Update(student);
                logger.LogInformation("Student {Firstname} {Lastname} updated successfully",
                    studentUpdateDTO.Firstname, studentUpdateDTO.Lastname);
                scope.Complete();
            }
            catch (StudentNotFoundException ex)
            {
                logger.LogError("Student Update failed for id {Id} {Firstname} {Lastname}. {ErrorMessage}",
                    studentUpdateDTO.Id, studentUpdateDTO.Firstname, studentUpdateDTO.Lastname, ex.Message);
                throw;
            }
            catch (TransactionException ex)
            {
                logger.LogError("Student Insertion failed for {Firstname} {Lastname}. {ErrorMessage}",
                    studentUpdateDTO.Firstname, studentUpdateDTO.Lastname, ex.Message);
                throw;
            }
        }

        public void DeleteStudent(int id)
        {
            try
            {
                using TransactionScope scope = new TransactionScope();
                if (studentDAO.GetById(id) == null)
                {
                    throw new StudentNotFoundException($"Student with id {id} not found.");
                }
                studentDAO.Delete(id);
                logger.LogInformation("Student with id {Id} deleted successfully", id);
                scope.Complete();
            }
            catch (TransactionException ex) // auto rollback 
            {
                logger.LogError("Student Deletion failed for id {Id}. {ErrorMessage}",
                    id, ex.Message);
                throw;
            }
            catch (StudentNotFoundException ex)
            {
                logger.LogError("Student with id {Id} not found. {ErrorMessage}",
                    id, ex.Message);
                throw;
            }
        }

        public StudentReadOnlyDTO GetStudent(int id)
        {
            StudentReadOnlyDTO studentReadOnlyDTO;
            Student? student;

            try
            {
                student = studentDAO.GetById(id);
                if (student == null)
                {
                    throw new StudentNotFoundException($"Student with id {id} not found.");
                }
                studentReadOnlyDTO = mapper.Map<StudentReadOnlyDTO>(student);
                logger.LogInformation("Student with id {Id} fetched successfully", id);
                return studentReadOnlyDTO;
            }
            catch (StudentNotFoundException ex)
            {
                logger.LogError("Student with id {Id} not found. {ErrorMessage}",
                    id, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError("Error while fetching student with id {Id}. {ErrorMessage}",
                                    id, ex.Message);
                throw;
            }
        }

        public List<StudentReadOnlyDTO> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        

        

        
    }
}
