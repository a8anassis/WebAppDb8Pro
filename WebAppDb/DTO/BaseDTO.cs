using System.ComponentModel.DataAnnotations;

namespace WebAppDb.DTO
{
    public abstract class BaseDTO
    {
        [Required(ErrorMessage = "The {0} is required.")]
        public int Id { get; set; }

        public BaseDTO()
        {

        }
        public BaseDTO(int id)
        {
            Id = id;
        }
    }

    //public abstract record BaseDTO(

    //    [property: Required(ErrorMessage = "The {0} is required.")] 
    //    int Id
    //)
    //{
    //    public BaseDTO() : this(0)
    //    {
    //    }
    //}
}
