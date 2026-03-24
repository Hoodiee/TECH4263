namespace StudentAPI.Models
{
    public class Student
    {   
        private static int _nextId = 1; // simple auto-increment for demo purposes
        public int Id { get; set; }              // server assigns
        public string Name { get; set; } = "";   // required
        public int Age { get; set; }             // simple field
        public string Major { get; set; } = "";  // optional/simple

        public Student(string name, int age, string major) 
        {
            Id = _nextId++; // auto-assign unique ID
            Name = name;
            Age = age;
            Major = major;
        }
    }
}
