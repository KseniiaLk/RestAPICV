using Microsoft.EntityFrameworkCore;
using RestAPICV.Data;
using RestAPICV.EndPoints;
using RestAPICV.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<RestAPICVContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// HttpClient 
builder.Services.AddHttpClient();

// Services
builder.Services.AddScoped<PersonalInfoService>();
builder.Services.AddScoped<EducationService>();
builder.Services.AddScoped<WorkExperienceService>();
builder.Services.AddScoped<GitHubService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Register endpoints
app.RegisterPersonalInfoEndpoints();
app.RegisterEducationEndpoints();
app.RegisterWorkExperienceEndpoints();
app.RegisterGitHubEndpoints();

app.Run(); 