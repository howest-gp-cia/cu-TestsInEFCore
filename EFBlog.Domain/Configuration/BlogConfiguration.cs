using EFBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFBlog.Domain.Configuration
{

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blog");
            

            builder.HasData
            (
                new Blog
                {
                    BlogId = 1,
                    Url = "https://devblogs.microsoft.com/aspnet/"
                },
                new Blog
                {
                    BlogId = 2,
                    Url = "https://devblogs.microsoft.com/aspnet/category/aspnetcore/"
                },
                new Blog
                {
                    BlogId = 3,
                    Url = "https://wakeupandcode.com/aspnetcore/"
                },
                new Blog
                {
                    BlogId = 4,
                    Url = "https://www.stevejgordon.co.uk/"
                }
            );
        }
    }
}
