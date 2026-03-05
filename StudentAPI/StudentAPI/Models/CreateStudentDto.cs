namespace StudentAPI.Models
{
    public class CreateStudentDto
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Major { get; set; } = string.Empty;
    }
}
