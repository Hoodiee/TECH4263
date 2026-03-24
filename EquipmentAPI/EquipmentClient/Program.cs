// See https://aka.ms/new-console-template for more information
using EquipmentClient.Models;

using System.Net.Http.Json;


var client = new HttpClient();

var drill = new Equipment("Drill", "Power Tools", "Available", "Warehouse A");
var osiclloscope = new Equipment("Oscilloscope", "Test Equipment", "In Use", "Lab B");
var multimeter = new Equipment("Multimeter", "Test Equipment", "Available", "Lab A");

var equipments = new List<Equipment> { drill, osiclloscope, multimeter };

foreach (var eq in equipments)
{
    var response = await client.PostAsJsonAsync("https://localhost:7226/createequipment", eq);
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine($"Created equipment: {eq.Name}");
    }
    else
    {
        Console.WriteLine($"Failed to create equipment: {eq.Name}");
    }
}

var resp = await client.GetAsync("https://localhost:7226/getequipments");

if (resp.IsSuccessStatusCode)
{
    var eqList = await resp.Content.ReadFromJsonAsync<List<Equipment>>();
    Console.WriteLine("All Equipments:");
    eqList.ForEach(e => Console.WriteLine($"{e.Id}: {e.Name} - {e.Category} - {e.Status} - {e.Location}"));
}
else
{
    Console.WriteLine("Failed to retrieve equipments.");
}




