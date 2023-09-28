using PowerplantCodingChallenge.API.Services;
using Serilog;

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.WriteTo.Console()
	.CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(); 

// Add services to the container.

builder.Services.AddControllers(options =>
{
	options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
	.AddXmlDataContractSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPowerplanCalculationService, PowerplanCalculationService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
