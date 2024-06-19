using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using APIwebRBA.Data;
using APIwebRBA.Data.Models;
using System;

namespace APIwebRBA.Controllers;

public static class BurgerEndpoints
{
    public static void MapBurgerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Burger").WithTags(nameof(Burguer));

        group.MapGet("/", async (APIwebRBAContext db) =>
        {
            return await db.Burguers.ToListAsync();
        })
        .WithName("GetAllBurgers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Burguer>, NotFound>> (int burgerId, APIwebRBAContext db) =>
        {
            return await db.Burguers.AsNoTracking()
                .FirstOrDefaultAsync(model => model.BurguerId == burgerId)
                is Burguer model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBurgerById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int burgerId, Burguer burger, APIwebRBAContext db) =>
        {
            var affected = await db.Burguers
                .Where(model => model.BurguerId == burgerId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.BurguerId, burger.BurguerId)
                    .SetProperty(m => m.Name, burger.Name)
                    .SetProperty(m => m.WithCheese, burger.WithCheese)
                    .SetProperty(m => m.Precio, burger.Precio));
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBurger")
        .WithOpenApi();

        group.MapPost("/", async (Burguer burger, APIwebRBAContext db) =>
        {
            db.Burguers.Add(burger);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Burger/{burger.BurguerId}", burger);
        })
        .WithName("CreateBurger")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int burgerId, APIwebRBAContext db) =>
        {
            var affected = await db.Burguers
                .Where(model => model.BurguerId == burgerId)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBurger")
        .WithOpenApi();
    }
}

