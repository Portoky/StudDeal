using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudDeal_API_Beta.Database;
using StudDeal_API_Beta.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StudDealDb>(opt => opt.UseInMemoryDatabase("StudDealDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();
var app = builder.Build();
app.UseOpenApi();
app.UseSwaggerUi();

app.MapGet("/restaurants" ,async (StudDealDb db) =>
{
    return await db.Restaurants.ToListAsync();
});

app.MapGet("/restaurant/{id}", async (int id, StudDealDb db) =>
{
    Restaurant restaurant = await db.Restaurants.FindAsync(id);

    if (restaurant == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(restaurant);
});

app.MapPost("/restaurant", async (Restaurant restaurant, StudDealDb db) =>
{
    db.Restaurants.Add(restaurant);
    await db.SaveChangesAsync();

    return Results.Ok(restaurant);
});

app.MapPut("/restaurant", async (Restaurant restaurant, StudDealDb db) =>
{
    Restaurant? oldRestaurant = await db.Restaurants.FindAsync(restaurant.Id);

    if(oldRestaurant == null)
    {
        return Results.NotFound();
    }

    oldRestaurant.Name = restaurant.Name;
    oldRestaurant.Address = restaurant.Address;
    oldRestaurant.ImageUrl = restaurant.ImageUrl;

    return  Results.Ok(oldRestaurant);
});

app.MapDelete("restaurant/{id}", async (int id, StudDealDb db) =>
{
    Restaurant? restaurant = await db.Restaurants.FindAsync(id);
    if (restaurant == null)
    {
        return Results.NotFound();
    }

    db.Restaurants.Remove(restaurant);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
