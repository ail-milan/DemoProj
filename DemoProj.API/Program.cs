using DemoProj.DataLayer;
using JsonApiDotNetCore.Configuration;
using log4net.Repository;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Define CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(
    name: "AllowAll",
    builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Configure controllers
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}).AddXmlDataContractSerializerFormatters();

// Set HTTP Context data accessor
builder.Services.AddHttpContextAccessor();

// SQL Server config
builder.Services.AddDbContext<DemoContext>();

// Json:API config
builder.Services.AddJsonApi<DemoContext>();

// Config Lowercase URLs for API Routes
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// log4net Configiguration
ILoggerRepository log4netRepository = log4net.LogManager.GetRepository(Assembly.GetEntryAssembly());
log4net.Config.XmlConfigurator.Configure(log4netRepository, new FileInfo("log4net.config"));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

// JsonApiDotNetCore middleware
app.UseJsonApi();
app.MapControllers();

app.Run();
