using Microsoft.EntityFrameworkCore;
using Quartz;
using TaskSchedular.CronJob;
using TaskSchedular.Helpers;
using TaskSchedular.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var services = builder.Services;


services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("CRONJOBSERVICE_JOB");
    q.AddJob<BootCronJobService>(opts => opts.WithIdentity(jobKey));
    // Create a trigger for the job
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("CRONJOBSERVICE_TRIGGER") // give the trigger a unique name
        .WithDailyTimeIntervalSchedule(a => { a.WithIntervalInHours(1); }));
});
services.AddQuartzHostedService(q =>
{
    q.WaitForJobsToComplete = true;
});


new Config(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
