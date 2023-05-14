using System.Reflection;
using AlifTech.Interview.Ewallet.Data;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Database>();

builder.Services.AddLogging(x => x.AddFluentMigratorConsole());
builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(c => c.AddPostgres()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
        .ScanIn(Assembly.GetExecutingAssembly()).For.All());

var app = builder.Build();

app.MigrateDatabase(builder.Configuration["Database:Default"]);

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();