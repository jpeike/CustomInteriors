using Core;
using Infrastructure;
using CustomInteriors.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        string configPath = Path.Combine(AppContext.BaseDirectory, "BaseDefaultSettings.config");

        string env = builder.Environment.EnvironmentName switch
        {
            "Development" => "Dev",
            "Staging" => "Test",
            "Production" => "Prod",
            _ => "Dev"
        };

        builder.Configuration.AddXmlConfig(configPath, env);

        // 1. Add JWT bearer authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["CognitoAuthority"];
                options.Audience = builder.Configuration["CognitoUserPoolId"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["CognitoAuthority"],

                    ValidateAudience = false,

                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        string? clientIdClaim = context.Principal?.Claims
                            .FirstOrDefault(c => c.Type == "client_id")?.Value;

                        if (clientIdClaim != builder.Configuration["CognitoUserPoolId"])
                        {
                            context.Fail("Invalid client_id");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        // 2. Add role-based authorization policies
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.AdminOnly, policy =>
                policy.RequireClaim("cognito:groups", Roles.Admin));
            options.AddPolicy(Policies.ManagerOrAdmin, policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim("cognito:groups", Roles.Admin) ||
                    context.User.HasClaim("cognito:groups", Roles.Manager)));
        });

        // Add services to the container.
        builder.Services.AddScoped<IAddressRepository, AddressRepository>();
        builder.Services.AddScoped<IAddressService, AddressService>();

        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();

        builder.Services.AddScoped<IEmailRepository, EmailRepository>();
        builder.Services.AddScoped<IEmailService, EmailService>();

        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        builder.Services.AddScoped<IInvoiceService, InvoiceService>();

        builder.Services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
        builder.Services.AddScoped<IInvoiceItemService, InvoiceItemService>();

        builder.Services.AddScoped<IJobRepository, JobRepository>();
        builder.Services.AddScoped<IJobService, JobService>();

        builder.Services.AddScoped<IJobAssignmentRepository, JobAssignmentRepository>();
        builder.Services.AddScoped<IJobAssignmentService, JobAssignmentService>();

        builder.Services.AddScoped<IJobInvoiceRepository, JobInvoiceRepository>();
        builder.Services.AddScoped<IJobInvoiceService, JobInvoiceService>();

        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();

        builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();
        builder.Services.AddScoped<IPhoneService, PhoneService>();

        builder.Services.AddScoped<IQuoteRequestRepository, QuoteRequestRepository>();
        builder.Services.AddScoped<IQuoteRequestService, QuoteRequestService>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        // todo fix environment naming stuff
        // if in integration testing environment, db setup is handled by a web app factory
        if (!builder.Environment.IsEnvironment("IntegrationTesting"))
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                string aspnetEnv = builder.Environment.EnvironmentName; // "Development", "Test", "Production"

                string? connectionString = aspnetEnv switch
                {
                    "Test" => builder.Configuration.GetConnectionString("E2ETestConnection"), // Playwright DB
                    "Development" => builder.Configuration.GetConnectionString("DefaultConnection"), // Dev DB
                    "Production" => builder.Configuration.GetConnectionString("DefaultConnection"), // Prod DB (usually same key)
                    _ => builder.Configuration.GetConnectionString("DefaultConnection")
                };

                options.UseSqlServer(connectionString);
            });
        }

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                policy =>
                {
                    policy.WithOrigins(builder.Configuration["FrontendHost"] ?? "")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //  Serve Vue static files
        app.UseDefaultFiles(); // Looks for index.html
        app.UseStaticFiles(); // Serves js/css/images

        app.UseHttpsRedirection();

        app.UseCors("AllowFrontend");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}