using LMS.Services;
using LMS.Services.Interfaces;
using LMS.Mapper; // ✅ AutoMapper Profile namespace
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers + JSON Settings
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// 2. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Service Registrations
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPersonalService, PersonalService>();
builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddScoped<IEmploymentService, EmploymentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ILeaveManagementService, LeaveManagementService>();

// 4. AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 5. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Swagger UI only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
