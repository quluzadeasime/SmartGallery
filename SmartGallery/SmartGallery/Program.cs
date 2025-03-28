using App.DAL.Presistence;
using Microsoft.Extensions.Options;
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

//var languageFilePath = Path.Combine(Directory.GetCurrentDirectory(), "language.json");
//if (!file.exists(languagefilepath))
//{
//    throw new filenotfoundexception($"language.json fayl? tap?lmad?: {languagefilepath}");
//}
//builder.configuration.addjsonfile(languagefilepath, optional: false, reloadonchange: true);

//builder.Configuration.AddJsonFile("C:\\Users\\Asime\\Desktop\\SmartGallery\\SmartGallery\\SmartGallery\\langauge.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("C:\\Users\\receb\\OneDrive\\Desktop\\SmartGallery\\SmartGallery\\SmartGallery\\langauge.json", optional: false, reloadOnChange: true);
builder.Services.Configure<LanguageSettings>(builder.Configuration);
builder.Services.Configure<AccountErrorMessages>(builder.Configuration.GetSection("AccountErrorMessages"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AccountErrorMessages>>().Value);


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