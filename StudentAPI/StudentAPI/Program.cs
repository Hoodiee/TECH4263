using StudentAPI.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var students = new List<Student>(); // In-memory list to store students for demo purposes


app.MapPost("/students", (CreateStudentDto dto) =>
{
    var student = new Student(dto.Name, dto.Age, dto.Major);
    students.Add(student);
    return Results.Created($"/students/{student.Id}", new StudentResponseDto
    {
        Id = student.Id,
        Name = student.Name,
        Major = student.Major
    });
})
.WithName("CreateStudents")
.WithOpenApi();

app.MapGet("/students", () =>
{
    var result = students.Select(s => new StudentResponseDto
    {
        Id = s.Id,
        Name = s.Name,
        Major = s.Major
    });
    return Results.Ok(result);
})
.WithName("GetStudentss")
.WithOpenApi();

app.MapGet("/students/{id:int:min(1)}", (int id) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);
    if (student == null) return Results.NotFound();

    return Results.Ok(new StudentResponseDto
    {
        Id    = student.Id,
        Name  = student.Name,
        Major = student.Major
    });
})
.WithName("GetStudentByIds")
.WithOpenApi();

app.Run();


