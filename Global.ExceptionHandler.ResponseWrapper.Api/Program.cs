using Global.ExceptionHandler.ResponseWrapper.Extensions;
using Global.ExceptionHandler.ResponseWrapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var middlewareSettings = builder.Configuration.GetSection("MiddlewareSettings").Get<MiddlewareSettings>();

builder.Services.AddControllers();
builder.Services.ConfigureCustomModelStateValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddResponseWrapper();

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseGlobalErrorHandlerMiddleware();
app.UseLoggingMiddleware();
if (middlewareSettings.UsePaginationResponseWrapperMiddleware)
    app.UsePaginationResponseWrapperMiddleware();
else
    app.UseResponseWrapperMiddleware();

app.UseModelStateValidationMiddleware();

//app.UseGlobalErrorHandlerAndResponseWrapperAndModelValidationMiddleware();



app.MapControllers();

app.Run();
