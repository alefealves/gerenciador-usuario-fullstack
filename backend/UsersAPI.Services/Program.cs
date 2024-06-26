using UsersAPI.Infra.Ioc.Extensions;
using UsersAPI.Infra.IoC.Extensions;
using UsersAPI.Services.Extensions;
using UsersAPI.Services.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerDoc();
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddCorsPolicy();
builder.Services.AddDependencyInjection();
builder.Services.AddAutoMapperConfig();
builder.Services.AddDbContextConfig(builder.Configuration);
builder.Services.AddRabbitMQ(builder.Configuration);
//builder.Services.AddEmailMessage(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwaggerDoc();
app.UseAuthentication();
app.UseAuthorization();
app.UseCorsPolicy();
app.MapControllers();
app.Run();

public partial class Program { }