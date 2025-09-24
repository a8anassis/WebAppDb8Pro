using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppDb.Models;
using WebAppDb.Services;

namespace WebAppDb.Pages.Students
{
    public class DeleteModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = [];

        private readonly IStudentService studentService;

        public DeleteModel(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public void OnGet(int id)
        {
            try
            {
                studentService.DeleteStudent(id);
                Response.Redirect("/Students/getall");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ErrorArray.Add(new Error { Message = ex.Message });
            }
        }
    }
}
