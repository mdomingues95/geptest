using geptest.Context;
using geptest.Services;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

{
    var services = builder.Services;
    var env = builder.Environment;


    services.AddDbContext<SqlLiteContext>();
    services.AddCors();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

     services.AddScoped<IProductService, ProductService>();

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "G&P Stock API", 
            Description = "Teste pratico Miller Domingues", Version = "v1" });
    });


    //  services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // configure DI for application services
   
}

var app = builder.Build();

// configure HTTP request pipeline
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "G&P Stock API");
        c.RoutePrefix = "";
    });

    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
  //  app.UseMiddleware<ErrorHandlerMiddleware>();

    app.MapControllers();
}

app.Run("http://localhost:4000");