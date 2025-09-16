using LMS.Services;
using LMS.Services.Interfaces;
using LMS.Mapper;
using System.Text.Json.Serialization;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

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
builder.Services.AddScoped<IGooseMenuService, GooseMenuService>();

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

// 6. Quartz Job Scheduling
builder.Services.AddQuartz(q =>
{
    // Use Microsoft DI JobFactory
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Schedule the job
    var jobKey = new JobKey("AutoUpdateLeaveSummary");
    q.AddJob<AutoUpdateLeaveSummaryJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("AutoUpdateLeaveSummaryTrigger")
        .WithCronSchedule("0 0 0 * * ?") // Runs every day at midnight
    );
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// 7. Swagger UI only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS API V1");
        c.RoutePrefix = string.Empty; // Open Swagger at root URL
    });
}

// 8. Middleware
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
