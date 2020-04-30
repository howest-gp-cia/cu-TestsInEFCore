using EFBlog.Domain.Entities;
using EFBlog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace EFBlog.IntegrationTests.InMemory
{
    public class BlogServiceTests
    {
        [Fact]
        public void Add_writes_to_database()
        {
            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            // Seed the database
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

        [Fact]
        public void Find_searches_url()
        {
            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName: "Find_searches_url")
                .Options;

            // Seed the database
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
    }
}
