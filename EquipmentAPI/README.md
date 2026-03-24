## 1) Create the `Equipment` class (Model) — properties only (no full code)

Create a new folder **Models** and add a class named **Equipment**. Give it the same “feel” as your Student model:

**Required properties**

* **Id** *(int)*

  * Server-assigned unique identifier.
  * Use a simple auto-increment approach (e.g., a private static counter) like your StudentAPI.
* **Name** *(string)*

  * Required (ex: “3D Printer”, “Oscilloscope”).
* **Category** *(string)*

  * Ex: “Manufacturing”, “Electronics”, “Networking”.
* **Status** *(string)*

  * Ex: “Available”, “InUse”, “Repair”.
* **Location** *(string)*

  * Ex: “Lab A”, “Room 101”, “Storage”.

**Constructor behavior**

* Constructor should accept `name, category, status, location`
* Assign **Id automatically** inside the constructor (incrementing counter), same idea as Student.

---

## 2) Create the 3 APIs (endpoints): purpose + signatures only

You will store equipment in-memory with a list, similar to `students = new List<Student>()`.

### API A — Create Equipment

**Purpose:** Create a new equipment item and store it in the in-memory list.

**Signature (Minimal API style):**

```csharp
app.MapPost("/createequipment", (string name, string category, string status, string location) => { ... })
   .WithName("CreateEquipment")
   .WithOpenApi();
```

Because these are simple parameters, Swagger will show them as inputs (query/form-style depending on caller).
---

### API B — Get All Equipment

**Purpose:** Return the full list of equipment items.

**Signature:**

```csharp
app.MapGet("/getequipments", () => { ... })
   .WithName("GetEquipments")
   .WithOpenApi();
```

---

### API C — Get Equipment by Id

**Purpose:** Return a single equipment item by `id` (or 404 if not found).

**Signature:**

```csharp
app.MapGet("/getequipment/{id}", (int id) => { ... })
   .WithName("GetEquipmentById")
   .WithOpenApi();
```

---

## 3) Five test cases (use Swagger UI)

Swagger lets you expand an endpoint, click **Try it out**, fill inputs, then **Execute**. ([Microsoft Learn][2])

1. **Create valid equipment (happy path)**

* Call `POST /createequipment` with:

  * name = “3D Printer”
  * category = “Manufacturing”
  * status = “Available”
  * location = “Lab A”
* Expected: **201 Created**, response shows equipment with an auto-assigned **Id**.

2. **Create another equipment (Id increments)**

* Create “Oscilloscope”, “Electronics”, “InUse”, “Room 101”
* Expected: **201 Created**, Id should be different than the first item.

3. **Get all equipment after creating 2 items**

* Call `GET /getequipments`
* Expected: **200 OK**, array length = 2, contains both items.

4. **Get equipment by valid id**

* Call `GET /getequipment/{id}` using an existing id (e.g., 1)
* Expected: **200 OK**, returns the correct item.

5. **Get equipment by invalid id**

* Call `GET /getequipment/{id}` with a non-existent id (e.g., 999)
* Expected: **404 Not Found**.

---

## 4) Push your changes to *your fork* (VS 2022 workflow)

Assuming students already **forked** your repo and **cloned** their fork in Visual Studio:

1. In **Visual Studio 2022**, open **Git Changes**

   * (View → Git Changes, or the Git tab/side panel)

2. **Stage** files

   * Add the new model file(s) and updated `Program.cs`

3. Write a commit message, e.g.

   * `Lab 1: In-memory Equipment API Complete`.

4. Click **Commit All** (or **Commit All and Push**) ([Microsoft Learn][3])

5. If you didn’t push in step 4:

   * Click **Push** in Git Changes (pushes to their `origin`, which is their fork)
6. Verify on GitHub that the commit appears in their fork’s main branch.
7. Submit the Repository URL of their fork to the assignment submission form.

