using StudentAPI.Models;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


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


app.MapPost("/students", async (CreateStudentDto dto) =>
{
    using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();

    var sql = @"
        INSERT INTO Students (Name, Major, GPA)
        OUTPUT INSERTED.Id
        VALUES (@Name, @Major, @GPA);
    ";

    using var command = new SqlCommand(sql, connection);
    command.Parameters.AddWithValue("@Name", dto.Name);
    command.Parameters.AddWithValue("@Major", dto.Major);
    command.Parameters.AddWithValue("@GPA", dto.GPA);

    var newId = (int)(await command.ExecuteScalarAsync())!;

    var response = new StudentResponseDto
    {
        Id = newId,
        Name = dto.Name,
        Major = dto.Major,
        GPA = dto.GPA
    };

    return Results.Created($"/students/{newId}", response);
})
.WithName("CreateStudent")
.WithOpenApi();

app.MapGet("/students", async () =>
{
    var list = new List<StudentResponseDto>();

    using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();

    var sql = "SELECT Id, Name, Major, GPA FROM Students;";

    using var command = new SqlCommand(sql, connection);
    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync())
    {
        list.Add(new StudentResponseDto
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Major = reader.GetString(reader.GetOrdinal("Major")),
            GPA = reader.GetDouble(reader.GetOrdinal("GPA"))
        });
    }

    return Results.Ok(list);
})
.WithName("GetStudents")
.WithOpenApi();

app.MapGet("/students/{id:int:min(1)}", async (int id) =>
{
    using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();

    var sql = "SELECT Id, Name, Major, GPA FROM Students WHERE Id = @Id;";

    using var command = new SqlCommand(sql, connection);
    command.Parameters.AddWithValue("@Id", id);

    using var reader = await command.ExecuteReaderAsync();

    if (!await reader.ReadAsync())
        return Results.NotFound();

    var dto = new StudentResponseDto
    {
        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        Name = reader.GetString(reader.GetOrdinal("Name")),
        Major = reader.GetString(reader.GetOrdinal("Major")),
        GPA = reader.GetDouble(reader.GetOrdinal("GPA"))
    };

    return Results.Ok(dto);
})
.WithName("GetStudentById")
.WithOpenApi();


app.Run();


