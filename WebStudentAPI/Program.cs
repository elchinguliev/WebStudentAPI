using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using WebStudentAPI.Data;
using WebStudentAPI.Formatters;
using WebStudentAPI.Repositories.Abstract;
using WebStudentAPI.Repositories.Concrete;
using WebStudentAPI.Services.Abstract;
using WebStudentAPI.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, new VCardInputFormatter());
    options.OutputFormatters.Insert(0,new TextCsvOutputFormatter());
    options.InputFormatters.Insert(0,new TextCsvInputFormatter());
    options.OutputFormatters.Insert(0, new VCardOutputFormatter());
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
var connection = builder.Configuration.GetConnectionString("myconn");
builder.Services.AddDbContext<StudentDBContext>(opt =>
{
    opt.UseSqlServer(connection);
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    options => {
        options.LoginPath = "/Account/SignIn";
        options.LogoutPath = "/Account/SignOut";
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseMiddleware<WebStudentAPI.MiddleWares.AuthenticationMiddlewaree>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
