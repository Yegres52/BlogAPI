using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.OpenApi.Models;

namespace BlogAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlogApi(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        var sqliteConnection = new SqliteConnectionStringBuilder(connectionString);
        if (!Path.IsPathRooted(sqliteConnection.DataSource))
            sqliteConnection.DataSource = Path.GetFullPath(Path.Combine(
                environment.ContentRootPath, sqliteConnection.DataSource));

        services.AddBlogData(sqliteConnection.ToString());
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            };
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Blog API",
                Version = "v1",
                Description = "REST API для пользователей, статей, тегов и комментариев."
            }));

        return services;
    }
}
