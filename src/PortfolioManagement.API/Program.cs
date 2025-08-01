using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI with comprehensive documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Portfolio Management API",
        Version = "v1",
        Description = "Comprehensive Portfolio Management System for Loan Origination with Investor KYC, Investment Products, Priority Allocation System, and Profit Distribution. Built specifically for the KSA market with Shariah compliance and Arabic language support.",
        Contact = new OpenApiContact
        {
            Name = "Portfolio Management Team",
            Email = "support@portfoliomanagement.sa",
        },
        License = new OpenApiLicense
        {
            Name = "Proprietary License",
        }
    });

    // Include XML comments for better documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Tag definitions for better organization
    c.TagActionsBy(api => new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] });
    c.DocInclusionPredicate((name, api) => true);
    
    // Custom schema IDs
    c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
});

// Add CORS for frontend integration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder
            .WithOrigins(
                "http://localhost:3000", // React dev server
                "http://localhost:5173", // Vite dev server
                "https://preview-minimalist-newsletter-form-kzmqurflb6z6j6kgtruc.vusercontent.net" // Provided frontend URL
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add Entity Framework (will be configured later)
// builder.Services.AddDbContext<PortfolioManagementDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services (interfaces will be implemented in Infrastructure layer)
// builder.Services.AddScoped<IInvestorService, InvestorService>();
// builder.Services.AddScoped<IFinancingProductService, FinancingProductService>();
// builder.Services.AddScoped<IInvestmentService, InvestmentService>();
// builder.Services.AddScoped<IProfitDistributionService, ProfitDistributionService>();
// builder.Services.AddScoped<IReportsService, ReportsService>();
// builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add API versioning
builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = Microsoft.AspNetCore.Mvc.ApiVersionReader.Combine(
        new Microsoft.AspNetCore.Mvc.QueryStringApiVersionReader("version"),
        new Microsoft.AspNetCore.Mvc.HeaderApiVersionReader("X-Version")
    );
    config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

// Configure JSON options for Arabic text support
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio Management API v1");
        c.RoutePrefix = string.Empty; // Make Swagger UI the root page
        c.DocumentTitle = "Portfolio Management API Documentation";
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        c.EnableDeepLinking();
        c.EnableFilter();
        c.ShowExtensions();
        c.EnableValidator();
        
        // Custom CSS for better appearance
        c.InjectStylesheet("/swagger-ui/custom.css");
        
        // Additional UI configuration
        c.ConfigObject.AdditionalItems.Add("requestSnippetsEnabled", true);
        c.ConfigObject.AdditionalItems.Add("syntaxHighlight", new { activated = true, theme = "agate" });
    });
}

// Security headers
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
    
    await next();
});

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowSpecificOrigins");

// Authentication & Authorization (will be implemented later)
// app.UseAuthentication();
// app.UseAuthorization();

// Custom middleware for request/response logging
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Request: {Method} {Path} from {RemoteIp}", 
        context.Request.Method, 
        context.Request.Path, 
        context.Connection.RemoteIpAddress);
    
    await next();
});

app.MapControllers();

// Serve static files for custom Swagger CSS
app.UseStaticFiles();

// Health check endpoint
app.MapGet("/health", () => new
{
    Status = "Healthy",
    Timestamp = DateTime.UtcNow,
    Environment = app.Environment.EnvironmentName,
    Version = "1.0.0"
});

// API information endpoint
app.MapGet("/api", () => new
{
    Title = "Portfolio Management API",
    Version = "1.0.0",
    Description = "Comprehensive Portfolio Management System for KSA market",
    Features = new[]
    {
        "Investor KYC Onboarding",
        "Investment Products Marketplace",
        "Priority Allocation System",
        "Profit Distribution Tracking",
        "Shariah Compliance",
        "Arabic Language Support",
        "Tax & Zakat Reporting",
        "Real-time Analytics"
    },
    SupportedCurrencies = new[] { "SAR" },
    SupportedLanguages = new[] { "en", "ar" },
    Documentation = "/swagger"
});

app.Run();
