using EFBlog.Domain.Entities;
using EFBlog.Domain.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace EFBlog.IntegrationTests.SqlLite
{
    public class BlogServiceTests
    {
        [Fact]
        public void Add_writes_to_database()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<BloggingContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new BloggingContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new BloggingContext(options))
                {
                    var service = new BlogService(context);
                    service.Add("https://example.com");
                    context.SaveChanges();
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new BloggingContext(options))
                {
                    // seeding contains 4 so total is now 5
                    Assert.Equal(5, context.Blogs.Count());
                    Assert.Equal("https://example.com", 
                                 context.Blogs.FirstOrDefault(b => b.BlogId == 5).Url);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public void Find_searches_url()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<BloggingContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new BloggingContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Insert data into the database using instance of the context
                using (var context = new BloggingContext(options))
                {
                    context.Blogs.Add(new Blog { Url = "https://example.com/cats" });
                    context.Blogs.Add(new Blog { Url = "https://example.com/catfish" });
                    context.Blogs.Add(new Blog { Url = "https://example.com/dogs" });
                    context.SaveChanges();
                }

                // Use a clean instance of the context to run the test
                using (var context = new BloggingContext(options))
                {
                    var service = new BlogService(context);
                    var result = service.Find("cat");
                    Assert.Equal(3, result.Count());

                    // not needed in this test, just to check our logic
                    // 4 records with EnsureCreated + 3 added records
                    // Assert.Equal(7, context.Blogs.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
