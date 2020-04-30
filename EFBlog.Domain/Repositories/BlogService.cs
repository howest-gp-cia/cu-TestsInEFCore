using EFBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFBlog.Domain.Repositories
{
    public class BlogService
    {
        private BloggingContext _context;

        // needed for Moq
        public BlogService()
        {
            
        }

        public BlogService(BloggingContext context)
        {
            _context = context;
        }

        public async Task Add(string url)
        {
            var blog = new Blog { Url = url };
            await _context.Blogs.AddAsync(blog);
            _context.SaveChanges();
        }



        public virtual async Task<IEnumerable<Blog>> All()
        {
            return await _context.Blogs
                .OrderBy(b => b.Url)
                .ToListAsync();
        }

        public IEnumerable<Blog> Find(string term)
        {
            return _context.Blogs
                .Where(b => b.Url.Contains(term))
                .OrderBy(b => b.Url)
                .ToList();
        }
    }
}
