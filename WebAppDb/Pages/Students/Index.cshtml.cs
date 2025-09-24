using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppDb.DTO;
using WebAppDb.Models;
using WebAppDb.Services;

namespace WebAppDb.Pages.Students
{
    public class IndexModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = [];

        private readonly IStudentService studentService;
        public List<StudentReadOnlyDTO> StudentsReadOnlyDTO { get; set; } = [];

        public IndexModel(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public IActionResult OnGet()
        {
            try
            {
                StudentsReadOnlyDTO = studentService.GetAllStudents();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ErrorArray.Add(new Error { Message = ex.Message });

            }
            return Page();
        }
    }
}
