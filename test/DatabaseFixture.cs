using System;
using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Test
{
    public class DatabaseFixture : IDisposable
    {
        public KeepDbContext context;

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<KeepDbContext>()
                .UseInMemoryDatabase(databaseName: "NoteDB")
                .Options;

            //Initializing DbContext with InMemory
            context = new KeepDbContext(options);

            // Insert seed data into the database using one instance of the context
            SeedData.PopulateTestData(context);
        }
        public void Dispose()
        {
            context = null;
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
