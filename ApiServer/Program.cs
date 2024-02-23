using ApiServer.Extensions;
using ApiServer.Middleware;
using Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAuthentication();//.AddCookie("Identity.Bearer");
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    //o =>
    //{
    //    o.AddSecurityDefinition("oath2", new OpenApiSecurityScheme
    //    {
    //        In = ParameterLocation.Header,
    //        Name = "Authorization",
    //        Type = SecuritySchemeType.ApiKey
    //    });
    //    o.OperationFilter<SecurityRequirementsOperationFilter>();
    //});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/account").MapIdentityApi<User>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();