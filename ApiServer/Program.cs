using ApiServer.Extensions;
using ApiServer.Middleware;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();

builder.Services.ConfigureIdentity(builder.Configuration);

builder.Services.AddAuthentication();
builder.Services.ConfigureApplicationCookie(o =>
{
    o.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = (int)403;
        return Task.CompletedTask;
    };
    o.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = (int)401;
        return Task.CompletedTask;
    };
});
builder.Services.AddAuthorizationBuilder();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// .NET 8 Auth: https://www.youtube.com/watch?v=sZnu-TyaGNk
// Configure Swagger: https://dev.to/eduardstefanescu/aspnet-core-swagger-documentation-with-bearer-authentication-40l6


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("api/account").MapIdentityApi<User>();

app.MapPost("api/account/logout", async (SignInManager<User> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

//app.MapGet("/account/name", (ClaimsPrincipal user) =>
//{
//    var email = user.FindFirstValue(ClaimTypes.Email);
//    //var name = user.Identity!.Name;
//    return Results.Json(new { Email = email });
//}).RequireAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();