using App.DAL.Presistence;
using Smart.API;
using Smart.Business;
using Smart.Core.Exceptions.Commons;
using Smart.DAL;
using Smart.Shared.Implementations;
using Smart.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddDataAccess(builder.Configuration)
                .AddBusiness(builder.Configuration);
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Configuration.AddJsonFile("language.json", optional: false, reloadOnChange: true);
builder.Services.Configure<LanguageSettings>(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await AutomatedMigration.MigrateAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.AddMiddlewares();

app.MapControllers();

app.Run();
