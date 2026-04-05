using EquipmentAPI.Models;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// =======================
// POST /equipments
// =======================
app.MapPost("/equipments", async (CreateEquipmentDto dto) =>
{
    using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();

    var sql = @"
        INSERT INTO Equipments (Name, Category, Status, Location)
        OUTPUT INSERTED.Id
        VALUES (@Name, @Category, @Status, @Location);
    ";

    using var command = new SqlCommand(sql, connection);
    command.Parameters.AddWithValue("@Name", dto.Name);
    command.Parameters.AddWithValue("@Category", dto.Category);
    command.Parameters.AddWithValue("@Status", dto.Status);
    command.Parameters.AddWithValue("@Location", dto.Location);

    var newId = (int)(await command.ExecuteScalarAsync())!;

    var response = new EquipmentResponseDto
    {
        Id = newId,
        Name = dto.Name,
        Category = dto.Category,
        Status = dto.Status,
        Location = dto.Location
    };

    return Results.Created($"/equipments/{newId}", response);
})
.WithName("CreateEquipment")
.WithOpenApi();




// =======================
// GET /equipments
// =======================
app.MapGet("/equipments", async () =>
{
    var list = new List<EquipmentResponseDto>();

    using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();

    var sql = "SELECT Id, Name, Category, Status, Location FROM Equipments;";

    using var command = new SqlCommand(sql, connection);
    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync())
    {
        list.Add(new EquipmentResponseDto
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Category = reader.GetString(reader.GetOrdinal("Category")),
            Status = reader.GetString(reader.GetOrdinal("Status")),
            Location = reader.GetString(reader.GetOrdinal("Location"))
        });
    }

    return Results.Ok(list);
})
.WithName("GetEquipments")
.WithOpenApi();

// =======================
// GET /equipments/{id}
// =======================
app.MapGet("/equipments/{id:int:min(1)}", async (int id) =>
{
    using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();

    var sql = "SELECT Id, Name, Category, Status, Location FROM Equipments WHERE Id = @Id;";

    using var command = new SqlCommand(sql, connection);
    command.Parameters.AddWithValue("@Id", id);

    using var reader = await command.ExecuteReaderAsync();

    if (!await reader.ReadAsync())
        return Results.NotFound();

    var dto = new EquipmentResponseDto
    {
        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        Name = reader.GetString(reader.GetOrdinal("Name")),
        Category = reader.GetString(reader.GetOrdinal("Category")),
        Status = reader.GetString(reader.GetOrdinal("Status")),
        Location = reader.GetString(reader.GetOrdinal("Location"))
    };

    return Results.Ok(dto);
})
.WithName("GetEquipmentById")
.WithOpenApi();

app.Run();


