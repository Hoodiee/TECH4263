# Building a Simple API Client in C#

This guide walks through how to build a lightweight console client that ingests a REST API using `HttpClient` and `System.Net.Http.Json`.

## Prerequisites

- [.NET 6+](https://dotnet.microsoft.com/download)
- A running REST API (local or remote)
- Basic familiarity with C#

---

## Project Setup

Create a new console project:

```bash
dotnet new console -n EquipmentClient
cd EquipmentClient
```

No extra NuGet packages are needed — `System.Net.Http.Json` ships with .NET 6+.

---

## Defining Your Model

Create a `Models/` folder and add a class that mirrors the shape of the API's JSON payload.

```csharp
// Models/Equipment.cs
namespace EquipmentClient.Models;

public record Equipment(string Name, string Category, string Status, string Location)
{
    public int Id { get; init; }
}
```

The property names must match the JSON keys returned or expected by the API (case-insensitive by default).

---

## Creating the HttpClient

Instantiate a single `HttpClient` for the lifetime of the application. Re-creating it on every request is a common mistake that can exhaust socket connections.

```csharp
var client = new HttpClient();
```

For production apps, use `IHttpClientFactory` via dependency injection instead.

---

## Sending Data (POST)

`PostAsJsonAsync` serializes your object to JSON and sets the `Content-Type: application/json` header automatically.

```csharp
var drill = new Equipment("Drill", "Power Tools", "Available", "Warehouse A");
var response = await client.PostAsJsonAsync("https://localhost:7226/createequipment", drill);

if (response.IsSuccessStatusCode)
    Console.WriteLine($"Created: {drill.Name}");
else
    Console.WriteLine($"Failed: {response.StatusCode}");
```

---

## Retrieving Data (GET)

`ReadFromJsonAsync<T>` deserializes the JSON response body directly into your model type.

```csharp
var response = await client.GetAsync("https://localhost:7226/getequipments");

if (response.IsSuccessStatusCode)
{
    var equipments = await response.Content.ReadFromJsonAsync<List<Equipment>>();
    foreach (var eq in equipments!)
        Console.WriteLine($"{eq.Id}: {eq.Name} - {eq.Status}");
}
```

---

## Full Example

```csharp
using EquipmentClient.Models;
using System.Net.Http.Json;

var client = new HttpClient();

// Seed some data
var items = new List<Equipment>
{
    new("Drill",        "Power Tools",    "Available", "Warehouse A"),
    new("Oscilloscope", "Test Equipment", "In Use",    "Lab B"),
    new("Multimeter",   "Test Equipment", "Available", "Lab A"),
};

foreach (var item in items)
{
    var post = await client.PostAsJsonAsync("https://localhost:7226/createequipment", item);
    Console.WriteLine(post.IsSuccessStatusCode ? $"✓ {item.Name}" : $"✗ {item.Name}");
}

// Fetch everything back
var get = await client.GetAsync("https://localhost:7226/getequipments");
if (get.IsSuccessStatusCode)
{
    var list = await get.Content.ReadFromJsonAsync<List<Equipment>>();
    list!.ForEach(e => Console.WriteLine($"{e.Id}: {e.Name} - {e.Category} - {e.Status} - {e.Location}"));
}
```

---

## Key Methods at a Glance

| Method | Use case |
|---|---|
| `PostAsJsonAsync(url, obj)` | Serialize and POST an object |
| `GetAsync(url)` | Send a GET request |
| `PutAsJsonAsync(url, obj)` | Serialize and PUT an object |
| `DeleteAsync(url)` | Send a DELETE request |
| `ReadFromJsonAsync<T>()` | Deserialize a response body |
| `response.IsSuccessStatusCode` | Check for 2xx status codes |

---

## Tips

- **Always check `IsSuccessStatusCode`** before reading the response body to avoid exceptions on error responses.
- **Use `await` consistently** — mixing sync and async HTTP calls can cause deadlocks.
- **Don't swallow errors silently** — log `response.StatusCode` and `response.ReasonPhrase` when a request fails to make debugging easier.
- **Configure a `BaseAddress`** on `HttpClient` if all your requests go to the same host, so you only need to specify relative paths in each call.

```csharp
client.BaseAddress = new Uri("https://localhost:7226/");
var response = await client.GetAsync("getequipments"); // relative path
```