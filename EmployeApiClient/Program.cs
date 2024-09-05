using ConnectAndSell.Common;
using EmployeeApiConsumer.CustomeMiddlewares;
using EmployeeApiConsumer.Helpers;
using EmployeeApiConsumer.Services;
using EmployeeApiConsumer.Services.FilterData;
using EmployeeConsumer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<IEmployeeServices,EmployeeServices>();
builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IChatService,ChatService>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<JwtHeaderHelper>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<FilterDataService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddScoped<HttpHelper>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddMvc();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

#region jobs
//var scheduleFactory = SchedulerBuilder.Create().Build();
//var scheduler = await scheduleFactory.GetScheduler();


//var job = JobBuilder.Create<HealthCheckJob>()
//    .WithIdentity(name: "BackgroundJob", group: "JobGroup")
//    .Build();

//var trigger = TriggerBuilder.Create()
//    .WithIdentity(name: "RepeatingTrigger", group: "JobGroup")
//    .WithCronSchedule(configuration["CornExpression"])
//    .Build();


//await scheduler.ScheduleJob(job, trigger);

//await scheduler.Start();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}


app.UseStatusCodePagesWithRedirects("/Comman/Error?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<IpAddressMiddleware>();
app.UseMiddleware<CorelationIdMiddleware>();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapHub<ChatHub>("/chathub");
app.Run();
