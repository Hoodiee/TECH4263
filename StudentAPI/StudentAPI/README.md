

# Build an In-Memory Student API

## What you are building

A Web API that runs on your computer and provides 3 endpoints:

1. **Create a student** (stores in memory)
2. **Get all students**
3. **Get one student by ID**

“In-memory” means:
✅ Data exists while the API is running
❌ Data disappears when you stop the API

---

## Step 1 — Create the Web API project in Visual Studio

1. Open **Visual Studio**
2. Click **Create a new project**
3. Search for and select **ASP.NET Core Web API**
4. Click **Next**
5. Enter:

   * **Project name:** `StudentAPI`
   * **Location:** choose your folder
6. Click **Next**
7. In the settings screen:

   * **Framework:** choose **.NET 8.0 (LTS)** (or the newest LTS available)
   * ✅ Check **Enable OpenAPI support** (this enables Swagger)
   * Uncheck **Enable Controllers** (we will use Minimal APIs, so we don’t need the MVC controller structure)

8. Click **Create**

---

## Step 2 — Run the template once (sanity check)

1. Press **F5** (or click ▶ Run)
2. Your browser should open automatically.
3. If Swagger opens, good. If not, manually go to:

   * `https://localhost:####/swagger`

> Tip: The port `####` is different on different machines/projects. Visual Studio shows it when you run.

---

## Step 3 — Create the Student model class

### 3.1 Add a folder

1. In **Solution Explorer**
2. Right-click the project `StudentAPI`
3. Click **Add → New Folder**
4. Name it: `Models`

### 3.2 Add `Student.cs`

1. Right-click **Models**
2. Click **Add → Class**
3. Name: `Student.cs`
4. Paste this code:

```csharp
namespace StudentAPI.Models
{
    public class Student
    {
        private static int _nextId = 1; // auto-increment for demo

        public int Id { get; set; }              // server assigns
        public string Name { get; set; } = "";   // required
        public int Age { get; set; }
        public string Major { get; set; } = "";

        public Student(string name, int age, string major)
        {
            Id = _nextId++;       // assign unique ID
            Name = name;
            Age = age;
            Major = major;
        }
    }
}
```

✅ What this does:

* Every time you create a new `Student`, it automatically gets a unique ID.
* The ID increases: 1, 2, 3, ...

---

## Step 4 — Edit `Program.cs` to add in-memory storage + endpoints

Open **Program.cs** and build the API.

### 4.1 Add the `using` statement

At the top, add:

```csharp
using StudentAPI.Models;
```

### 4.2 Ensure Swagger services exist

You should have these lines in the builder section:

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

### 4.3 Create the app and enable Swagger

After `var app = builder.Build();`, you want:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

### 4.4 Add HTTPS redirection (fine to keep)

```csharp
app.UseHttpsRedirection();
```

### 4.5 Add the in-memory “database”

Add:

```csharp
var students = new List<Student>(); // in-memory storage
```

This is the “database” for now.

---

## Step 5 — Add the three endpoints

### Endpoint 1: Create Student (POST)

Add:

```csharp
app.MapPost("/createstudent", (string name, int age, string major) =>
{
    var student = new Student(name, age, major);
    students.Add(student);

    return Results.Created($"/createstudent/{student.Id}", student);
})
.WithName("CreateStudent")
.WithOpenApi();
```

✅ What it does:

* Creates a student object
* Adds it to the `students` list
* Returns:

  * **201 Created**
  * the created student JSON including its generated `Id`

> Important detail: This endpoint expects values as **query parameters** (name, age, major). Swagger will show them as fields to fill in.

---

### Endpoint 2: Get all Students (GET)

Add:

```csharp
app.MapGet("/getstudents", () =>
{
    return Results.Ok(students);
})
.WithName("GetStudents")
.WithOpenApi();
```

✅ What it does:

* Returns **200 OK**
* Returns the list as JSON (array)

---

### Endpoint 3: Get Student by ID (GET)

Add:

```csharp
app.MapGet("/getstudent/{id}", (int id) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);
    if (student == null)
        return Results.NotFound();

    return Results.Ok(student);
})
.WithName("GetStudentById")
.WithOpenApi();
```

✅ What it does:

* If student exists: **200 OK** + student JSON
* If not found: **404 Not Found**

---

## Step 6 — Your final `Program.cs` should look like this (full reference)

Use this as a checklist to ensure nothing is missing:

```csharp
using StudentAPI.Models;

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

var students = new List<Student>();

app.MapPost("/createstudent", (string name, int age, string major) =>
{
    var student = new Student(name, age, major);
    students.Add(student);

    return Results.Created($"/createstudent/{student.Id}", student);
})
.WithName("CreateStudent")
.WithOpenApi();

app.MapGet("/getstudents", () =>
{
    return Results.Ok(students);
})
.WithName("GetStudents")
.WithOpenApi();

app.MapGet("/getstudent/{id}", (int id) =>
{
    var student = students.FirstOrDefault(s => s.Id == id);
    if (student == null)
        return Results.NotFound();

    return Results.Ok(student);
})
.WithName("GetStudentById")
.WithOpenApi();

app.Run();
```

---

## Step 7 — Run and test in Swagger (recommended)

### 7.1 Run the API

* Press **F5** in Visual Studio.

### 7.2 Open Swagger

* Go to: `https://localhost:####/swagger`

### 7.3 Test Create Student

* Expand **POST /createstudent**
* Click **Try it out**
* Fill:

  * name: `John Doe`
  * age: `20`
  * major: `Computer Science`
* Click **Execute**

Expected:

* Status: **201 Created**
* Response JSON includes `id`, `name`, `age`, `major`

### 7.4 Test Get Students

* Expand **GET /getstudents**
* Execute
  Expected: list contains the student you created

### 7.5 Test Get Student by ID

* Expand **GET /getstudent/{id}**
* Enter `1` (or the ID you saw)
* Execute
  Expected: that student JSON

---
