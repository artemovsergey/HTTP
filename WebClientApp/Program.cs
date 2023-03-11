var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(); // ��������� ������� CORS

var app = builder.Build();

// ����������� CORS
app.UseCors(builder => builder.AllowAnyOrigin());

//app.MapGet("/", () => new Person() { Id=1,Name="user1"}); // auto serialize to json

app.MapGet("/{id?}", (int? id) =>
{
    if (id is null)
        return Results.BadRequest(new { Message = "������������ ������ � �������" });
    else if (id != 1)
        return Results.NotFound(new { Message = $"������ � id={id} �� ����������" });
    else
        return Results.Json(new Person() { Id="2",Name="user2"}); 
        // � ����� ����, ����� �� ����� �������������� ���������� � ������
});


app.MapPost("/data", async (HttpContext httpContext) =>
{
    using StreamReader reader = new StreamReader(httpContext.Request.Body);
    string name = await reader.ReadToEndAsync();
    return $"�������� ������: {name}";
});

app.MapPost("/create", (Person person) =>
{
    // ������������� id � ������� Person
    person.Id = Guid.NewGuid().ToString();
                     // ���������� ������� ������ Person

    //return Results.Json(person);
    return person;
});


app.Run();

public class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
}