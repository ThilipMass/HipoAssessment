using Microsoft.EntityFrameworkCore;
using HIPO.Infrastructure;
using HIPO.Core;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Setup Serilog from configuration
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(); // ✅ THIS IS CRUCIAL
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Strict;
});
builder.Services.AddAntiforgery();
// Add services...
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // This line is **required**
builder.Services.AddDbContext<HipoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HipoDbConnection")));
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMemoryCache();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

// Run the seeder
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<HipoDbContext>();
//     dbContext.Database.Migrate(); // Apply migrations
//     await HipoDbSeeder.SeedHipoQuestionsAsync(dbContext);
// }
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HipoDbContext>();
    dbContext.Database.Migrate(); // Apply migrations

    await HipoDbSeeder.SeedHipoQuestionsAsync(dbContext);            // Seed questions
    await HipoDbSeeder.SeedHipoAssessmentOptionsAsync(dbContext);   // Seed assessment options
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CustomPolicy");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();