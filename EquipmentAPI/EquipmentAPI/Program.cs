using EquipmentAPI.Data;
using EquipmentAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// ─────────────────────────────────────────────────────────────
// POST /equipments  (EF Core)
// ─────────────────────────────────────────────────────────────
app.MapPost("/equipments", async (CreateEquipmentDto dto, AppDbContext context) =>
{
    var entity = new Equipment
    {
        Name = dto.Name,
        Category = dto.Category,
        Status = dto.Status,
        Location = dto.Location
    };

    context.Equipments.Add(entity);
    await context.SaveChangesAsync();

    var response = new EquipmentResponseDto
    {
        Id = entity.Id,
        Name = entity.Name,
        Category = entity.Category,
        Status = entity.Status,
        Location = entity.Location
    };

    return Results.Created($"/equipments/{entity.Id}", response);
})
.WithName("CreateEquipment")
.WithOpenApi();


// ─────────────────────────────────────────────────────────────
// GET /equipments  (EF Core)
// ─────────────────────────────────────────────────────────────
app.MapGet("/equipments", async (AppDbContext context) =>
{
    var list = await context.Equipments
        .Select(e => new EquipmentResponseDto
        {
            Id = e.Id,
            Name = e.Name,
            Category = e.Category,
            Status = e.Status,
            Location = e.Location
        })
        .ToListAsync();

    return Results.Ok(list);
})
.WithName("GetEquipments")
.WithOpenApi();


// ─────────────────────────────────────────────────────────────
// GET /equipments/{id}  (EF Core)
// ─────────────────────────────────────────────────────────────
app.MapGet("/equipments/{id:int:min(1)}", async (int id, AppDbContext context) =>
{
    var e = await context.Equipments.FindAsync(id);

    if (e is null)
        return Results.NotFound();

    var dto = new EquipmentResponseDto
    {
        Id = e.Id,
        Name = e.Name,
        Category = e.Category,
        Status = e.Status,
        Location = e.Location
    };

    return Results.Ok(dto);
})
.WithName("GetEquipmentById")
.WithOpenApi();


app.Run();
