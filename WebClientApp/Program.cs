var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(); // добавляем сервисы CORS

var app = builder.Build();

// настраиваем CORS
app.UseCors(builder => builder.AllowAnyOrigin());

//app.MapGet("/", () => new Person() { Id=1,Name="user1"}); // auto serialize to json

app.MapGet("/{id?}", (int? id) =>
{
    if (id is null)
        return Results.BadRequest(new { Message = "Некорректные данные в запросе" });
    else if (id != 1)
        return Results.NotFound(new { Message = $"Объект с id={id} не существует" });
    else
        return Results.Json(new Person() { Id="2",Name="user2"}); 
        // в общем виде, когда не нужна дополнительная информация в ответе
});


app.MapPost("/data", async (HttpContext httpContext) =>
{
    using StreamReader reader = new StreamReader(httpContext.Request.Body);
    string name = await reader.ReadToEndAsync();
    return $"Получены данные: {name}";
});

app.MapPost("/create", (Person person) =>
{
    // устанавливает id у объекта Person
    person.Id = Guid.NewGuid().ToString();
                     // отправляем обратно объект Person

    //return Results.Json(person);
    return person;
});


app.Run();

public class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
}