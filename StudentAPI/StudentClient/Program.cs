// See https://aka.ms/new-console-template for more information

using System.Net.Http;
using System.Net.Http.Json;

using var httpClient = new HttpClient();


var resp1 = await httpClient.PostAsync("https://localhost:7297/createstudent?name=John&age=20&major=CS", null);
var resp2 = await httpClient.PostAsync("https://localhost:7297/createstudent?name=Jane&age=22&major=Math", null);



var getStudentsResponse = await httpClient.GetAsync("https://localhost:7297/getstudents");

Console.WriteLine($"Get Students Response Status: {getStudentsResponse.StatusCode}");
Console.WriteLine($"Get Students Response Body: {await getStudentsResponse.Content.ReadAsStringAsync()}");

Console.WriteLine("Press any key to close...");
Console.ReadKey();