namespace WebAppDb.DTO
{
    public class StudentReadOnlyDTO : BaseDTO
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public StudentReadOnlyDTO() : base()
        {
        }
        public StudentReadOnlyDTO(int id, string? firstname, string? lastname) : base(id)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
    }

    //public record StudentReadOnlyDTO(
    //    int Id,
    //    string? Firstname,
    //    string? Lastname
    //) : BaseDTO(Id)
    //{
    //    public StudentReadOnlyDTO() : this(0, string.Empty, string.Empty)
    //    {
    //    }
    //}
}
