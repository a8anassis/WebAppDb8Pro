using System.ComponentModel.DataAnnotations;

namespace WebAppDb.DTO
{
    public class StudentUpdateDTO : BaseDTO
    {
        [Required(ErrorMessage = "Firstname is required.")]
        [MinLength(1, ErrorMessage = "Firstname cannot be empty.")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        [MinLength(1, ErrorMessage = "Lastname cannot be empty.")]
        public string? Lastname { get; set; }
        public StudentUpdateDTO() : base()
        {
        }
        public StudentUpdateDTO(int id, string? firstname, string? lastname) : base(id)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
    }

    //public record StudentUpdateDTO(

    //    int Id,

    //    [property: Required(ErrorMessage = "Firstname is required.")]
    //    [property: MinLength(1, ErrorMessage = "Firstname cannot be empty.")]
    //    string? Firstname,

    //    [property: Required(ErrorMessage = "Lastname is required.")]
    //    [property: MinLength(1, ErrorMessage = "Lastname cannot be empty.")]
    //    string? Lastname
    //) : BaseDTO(Id)
    //{
    //    public StudentUpdateDTO() : this(0, string.Empty, string.Empty)
    //    {
    //    }
    //}


}
