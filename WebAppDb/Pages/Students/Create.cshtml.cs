using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppDb.DTO;
using WebAppDb.Models;
using WebAppDb.Services;

namespace WebAppDb.Pages.Students
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public StudentInsertDTO StudentInsertDTO { get; set; } = new();
        public List<Error> ErrorArray { get; set; } = [];
        
        private readonly IStudentService studentService;

        public CreateModel(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public void OnGet()
        {
            // return Page();
        }

        public void onPost()
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return; // return Page();
            }
            
            try 
            {
                StudentReadOnlyDTO? studentReadOnlyDTO = studentService.InsertStudent(StudentInsertDTO);
                Response.Redirect("/Students/getall");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ErrorArray.Add(new Error { Message = ex.Message });
                return; // return Page();
            }
        }   
    }
}
