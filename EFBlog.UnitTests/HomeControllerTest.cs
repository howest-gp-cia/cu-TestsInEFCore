using EFBlog.Domain.Entities;
using EFBlog.Domain.Repositories;
using EFCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EFBlog.UnitTests
{

    /*
     * Moq: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.1
     */
    public class HomeControllerTest
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfBlogs()
        {
            // Arrange
            var mockRepo = new Mock<BlogService>();
            mockRepo.Setup(repo => repo.All())
            .ReturnsAsync(GetTestBlogs());
            var controller = new HomeController(null, mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Blog>>(
                viewResult.ViewData.Model);
            Assert.Equal(4, model.Count());
        }

        [Fact]
        public async Task IndexPost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockRepo = new Mock<BlogService>();
            mockRepo.Setup(repo => repo.All())
            .ReturnsAsync(GetTestBlogs());
            var controller = new HomeController(null, mockRepo.Object);
            controller.ModelState.AddModelError("BlogUrl", "Blog Url can not be empty");
            var newBlog = new Blog { BlogId = 10, Url = "" };

            // Act
            var result = await controller.Add(newBlog);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }


        private List<Blog> GetTestBlogs()
        {
            var blogs = new List<Blog>();
            blogs.Add(new Blog()
            {
                BlogId = 1,
                Url = "https://devblogs.microsoft.com/aspnet/"
            });
            blogs.Add(
                new Blog
                {
                    BlogId = 2,
                    Url = "https://devblogs.microsoft.com/aspnet/category/aspnetcore/"
                });
            blogs.Add(
                new Blog
                {
                    BlogId = 3,
                    Url = "https://wakeupandcode.com/aspnetcore/"
                });
            blogs.Add(new Blog
            {
                BlogId = 4,
                Url = "https://www.stevejgordon.co.uk/"
            });
            return blogs;
        }
    }
}
