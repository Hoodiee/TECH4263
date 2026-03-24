using EquipmentAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var equipments = new List<Equipment>();

// =======================
// POST /equipments
// =======================
app.MapPost("/equipments", (CreateEquipmentDto dto) =>
{
    var equipment = new Equipment(
        dto.Name,
        dto.Category,
        dto.Status,
        dto.Location
    );

    equipments.Add(equipment);

    return Results.Created($"/equipments/{equipment.Id}", new EquipmentResponseDto
    {
        Id = equipment.Id,
        Name = equipment.Name,
        Category = equipment.Category,
        Status = equipment.Status,
        Location = equipment.Location
    });
})
.WithName("CreateEquipment")
.WithOpenApi();

// =======================
// GET /equipments
// =======================
app.MapGet("/equipments", () =>
{
    var result = equipments.Select(e => new EquipmentResponseDto
    {
        Id = e.Id,
        Name = e.Name,
        Category = e.Category,
        Status = e.Status,
        Location = e.Location
    });

    return Results.Ok(result);
})
.WithName("GetEquipments")
.WithOpenApi();

// =======================
// GET /equipments/{id}
// =======================
app.MapGet("/equipments/{id:int:min(1)}", (int id) =>
{
    var equipment = equipments.FirstOrDefault(e => e.Id == id);

    if (equipment == null)
        return Results.NotFound();

    return Results.Ok(new EquipmentResponseDto
    {
        Id = equipment.Id,
        Name = equipment.Name,
        Category = equipment.Category,
        Status = equipment.Status,
        Location = equipment.Location
    });
})
.WithName("GetEquipmentById")
.WithOpenApi();

app.Run();


