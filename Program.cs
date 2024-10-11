using Microsoft.OpenApi.Models;
using PizzaStore.DB;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
if (builder.Environment.IsDevelopment())
{
    _ = builder.Services.AddSwaggerGen(static c =>
    {
        c.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Title = "Todo API",
                Description = "Keep track of your tasks",
                Version = "v1",
            }
        );
    });
}

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI(static c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
    });
}

app.MapGet("/", static () => "Hello World!");
app.MapGet("/pizzas/{id}", static (int id) => PizzaDB.GetPizza(id));
app.MapGet("/pizzas", static () => PizzaDB.GetPizzas());

app.MapPost("/pizzas", static (Pizza pizza) => PizzaDB.CreatePizza(pizza));

app.MapPut("/pizzas", static (Pizza pizza) => PizzaDB.UpdatePizza(pizza));

app.MapDelete("/pizzas/{id}", static (int id) => PizzaDB.RemovePizza(id));

app.Run();
