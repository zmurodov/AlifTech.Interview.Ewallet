using System.Reflection;
using AlifTech.Interview.Ewallet.Auth;
using AlifTech.Interview.Ewallet.Data;
using AlifTech.Interview.Ewallet.Filters;
using AlifTech.Interview.Ewallet.Repositories;
using AlifTech.Interview.Ewallet.Repositories.Interfaces;
using AlifTech.Interview.Ewallet.Services;
using AlifTech.Interview.Ewallet.Services.Interfaces;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerDigestAuthHeadersFilter>();
    
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(DigestAuthenticationDefaults.AuthenticationScheme)
    .AddDigestAuthentication();

builder.Services.AddScoped<Database>();
builder.Services.AddScoped<IDapperDbContext, DapperDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<IWalletTypeRepository, WalletTypeRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWalletService, WalletService>();

builder.Services.AddTransient<IDigestGenerator, DigestGenerator>();

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
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();