using App.DAL.Presistence;
using Smart.API;
using Smart.Business;
using Smart.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container.
builder.Services
    .AddDataAccess(builder.Configuration)
    .AddBusiness();
builder.Services
    .AddJwt(builder.Configuration);
builder.Services
    .AddSwagger();
builder.Services
    .AddControllers();
builder.Services
    .AddEndpointsApiExplorer();

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
