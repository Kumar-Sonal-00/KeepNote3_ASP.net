using System;
using DAL;
using KeepNote;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Test
{
    public class CustomWebApplicationFactory<TStartup>: WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (KeepNoteContext) using an in-memory database for testing.
                services.AddDbContext<KeepDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryNoteDB");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var noteDb = scopedServices.GetRequiredService<KeepDbContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    noteDb.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with some specific test data.
                        SeedData.PopulateTestData(noteDb);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
    [CollectionDefinition("Db collection")]
    public class DbCollection : ICollectionFixture<CustomWebApplicationFactory<Startup>>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
