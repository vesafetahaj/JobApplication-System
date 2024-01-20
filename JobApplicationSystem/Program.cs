using JobApplicationSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Repositories;
using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Models;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<JobApplicationSystemContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IEmployerService, EmployerService>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IInterviewService, InterviewService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddScoped<IRoleRepository<AspNetRole>, RoleRepository>();
builder.Services.AddScoped<IEmployerRepository<Employer>, EmployerRepository>();
builder.Services.AddScoped<IApplicantRepository<Applicant>, ApplicantRepository>();
builder.Services.AddScoped<IAdministratorRepository<Administrator>, AdministratorRepository>();
builder.Services.AddScoped<IJobRepository<Job>, JobRepository>();
builder.Services.AddScoped<IApplicationRepository<Application>, ApplicationRepository>();
builder.Services.AddScoped<IInterviewRepository<Interview>, InterviewRepository>();
builder.Services.AddScoped<IFeedbackRepository<Feedback>, FeedbackRepository>();




builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Employer", "Applicant" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

app.Run();
