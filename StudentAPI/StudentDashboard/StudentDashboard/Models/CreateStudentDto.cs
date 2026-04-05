using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDashboard.Models;

public class CreateStudentDto
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Major { get; set; } = string.Empty;
}