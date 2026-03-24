using EquipmentAPI.Models;

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

//app.MapPost("/createequipment", (string name, string category, string status, string location) => { ... })
//   .WithName("CreateEquipment")
//   .WithOpenApi();


//app.MapGet("/getequipments", () => { ... })
//   .WithName("GetEquipments")
//   .WithOpenApi();

//app.MapGet("/getequipment/{id}", (int id) => { ... })
//   .WithName("GetEquipmentById")
//   .WithOpenApi();

var equipments = new List<Equipment>();

app.MapPost("/createequipment", (Equipment eq) =>
{
    equipments.Add(eq);
    return Results.Created($"/getequipment/{eq.Id}", eq);
}).WithName("CreateEquipment").WithOpenApi();

app.MapGet("/getequipments", () => Results.Ok(equipments)).WithName("GetEquipments").WithOpenApi();

app.MapGet("/getequipment/{id}", (int id) =>
{
    var eq = equipments.FirstOrDefault(e => e.Id == id);
    return eq != null ? Results.Ok(eq) : Results.NotFound();
}).WithName("GetEquipmentById").WithOpenApi();


app.Run();


