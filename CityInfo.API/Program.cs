//the main method in this class is the starting point of our application. 
//.Net0.6 & C#10, this is a new language feature named the toplevel statement feature. 

using CityInfo.API;
using CityInfo.API.DbContexts;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

// thirdparty Provider logging Serilog Configure
//1.serilog  nuget Package
//2.serilog.sinks.file
//3.serilog.sinks.console

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)//Create city info file.txt every day
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);//bulder,This is the builtin dependency injection container, 
//This call, behind the scenes, calls into CreateDefaultBuilder, and that registers different logging providers for us. 
//builder.Logging.ClearProviders();//all the configured providers should be cleared,resulting in nothing being logged anymore.  
//// manually add a console logger. For that, we call into builder.Logging.AddConsole, and let's give that another try.  
//builder.Logging.AddConsole();

//  All we need to do now is tell ASP.NET Core that it should use
//  Serilog instead of the builtin logger. To enable that, we call into builder.Host.UseSerilog. 


builder.Host.UseSerilog();
// Add services to the container.
//Services should be seen as a broad concept.A service is a component that is intended for common consumption in an application.  
//now we can remove builder.Logging.ClearProviders(); and builder.Logging.AddConsole();
builder.Services.AddControllers(options =>
{
    //options.InputFormatters.Add(formatter => JsonContent); // ie we need to change the default output or input formatter, the rule is that the 
    //options.OutputFormatters.Add(formatter => JsonContent);

    options.ReturnHttpNotAcceptable = true;// show statues 406 Not Acceptable
}).AddNewtonsoftJson() //effectively replaces the default JSON input and output formatters with 
    .AddXmlDataContractSerializerFormatters();//This adds XML input and output formatters.So with this one line of code, we have just added support for XML to our API. 


//This c all registers services that are typically required when building APIs, 
//like support for controllers, model binding, data annotations, and formatters. 
//Services.AddMvc(),AddMvcCore(),AddControllersWithViews()

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//both have to do with the Swagger documentation. These statements register the required services on the container that are needed by Swashbuckle's Swagger implementation.  
//Once all these services have been registered and potentially configured the web application can be built. , 
builder.Services.AddEndpointsApiExplorer();//
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);

    setupAction.AddSecurityDefinition("CityInfoApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "CityInfoApiBearerAuth" }
            }, new List<string>() }
    });

});//
#region Files Manuplation FileExtensionContentTypeProvider
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();// Load all Files Type just know that this statement allows us to inject a FileExtensionContentTypeProvider in other parts of our code.  
#endregion

// registered a concrete type, LocalMailService. 
//That will make it hard to work with a testing
//service in a different environment. 
#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();// grayed out at release
#endif
//These methods are AddTransient, to add a service with a transient lifetime,
//Transient lifetime services are created each time they are requested.
//This lifetime works best for lightweight, stateless services. 
//AddScoped, to add one with a scope lifetime,
//Scoped lifetime services are created once per request. 
//and AddSingleton, to add one with a singleton lifetime. 
//And singleton lifetime services are created the first time they are requested. 
//Every subsequent request will use the same instance. 

//inject CitiDataStore
builder.Services.AddSingleton<CitiesDataStore>();
#region Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CityInfoContext>(dbContextOptions =>
dbContextOptions.UseSqlServer(connectionString));
//builder.Services.AddDbContext<CityInfoContext>(
//    dbContextOptions => dbContextOptions.UseSqlServer(
//     builder.Configuration["ConnectionString:CityInfoDBConnectionString"]));//not best practice
#endregion
builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Authentcation Bearer
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))


        };
    }
    );

//add policy
builder.Services.AddAuthorization(options =>
    options.AddPolicy("MustBeFromAntwerp", policy =>
    {
        policy.RequireAuthenticatedUser();
        //city we want to make policy for it
        policy.RequireClaim("city", "Antwerp");
    })
);
//virsioning
builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    setupAction.ReportApiVersions = true;
});
var app = builder.Build();//This results in an object of type WebApplication. 
//Middleware
//which are software components that are assembled into an application pipeline to handle requests and responses.  
//// Configure the HTTP request pipeline.
///3 Types of Enviroment Development,Staging and Production
///
//Mind you, an environment is not the same as a type of build. You might roll out a debug build to a staging environment, for example. 


// Configure thr HTTP request pipeline
if (app.Environment.IsDevelopment()) //This checks whether the current environment is a development environment. 
{
    //programmatically access the environment value, we can use the IWebHostEnvironment service. This provides the core abstraction for working with environments.It's provided by the ASP.NET Core hosting layer and available via app.Environment.     
    //swager should be added in enviroment development
    app.UseSwagger();//important to call UseSwagger() before UseSwaggerUi(),page middleware to the request pipelin.
                     //The Swagger middleware itself takes care of generating a specification. 
    app.UseSwaggerUI();//page middleware to the request pipeline.
                       //The Swagger UI middleware takes that specification and generates a nice documentation UI from it.  
}//disapear in Production Enviroment

app.UseHttpsRedirection();//HTTP calls being redirected to HTTPS.

app.UseRouting();
//Authintaction Middleware
app.UseAuthentication();

app.UseAuthorization();//authorization being set up  or mappings to other parts of our code being set up. 

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();
//app.Run(async (context) =>
//    {
//        await context.Response.WriteAsync("Hello World");//write helloe world in all web pages a request delegate to be exact, 
//    });

app.Run();

// Formatters and Content Negotiation (application/json,application/xml)