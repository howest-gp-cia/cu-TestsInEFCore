using Xunit;
using EFBlog.Domain.Entities;

namespace EFBlog.UnitTests
{
    public class BlogTest
    {
        [Fact]
        public void ShortenUrl_EmptyUrl_ReturnsEmptyString()
        {
            // Arrange
            Blog blog = new Blog { BlogId = 1, Url = "" };

            // Act
            string actual = blog.ShortenUrl();
            string expected = "";

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShortenUrl_Url_ReturnsShortenedUrl()
        {
            // Arrange
            Blog blog = new Blog { BlogId = 1, Url = "https://devblogs.microsoft.com/aspnet/" };

            // Act
            string actual = blog.ShortenUrl();
            string expected = "https://de";

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShortenUrl_Url_GivenCharacterCount_ReturnsShortenedUrl()
        {
            // Arrange
            int charactercount = 15;
            Blog blog = new Blog { BlogId = 1, Url = "https://devblogs.microsoft.com/aspnet/" };

            // Act
            string actual = blog.ShortenUrl(charactercount);
            string expected = "https://devblog";

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShortenUrl_Url_CharacterCountToHigh_ReturnsUrl()
        {
            // Arrange
            int charactercount = 100;
            Blog blog = new Blog { BlogId = 1, Url = "https://devblogs.microsoft.com/aspnet/" };

            // Act
            string actual = blog.ShortenUrl(charactercount);
            string expected = "https://devblogs.microsoft.com/aspnet/";

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveUrlProtocol_Urlhttp_ReturnsUrlWithoutProtocol()
        {
            // Arrange
            Blog blog = new Blog { BlogId = 1, Url = "http://devblogs.microsoft.com/aspnet/" };

            // Act
            string actual = blog.RemoveUrlProtocol();
            string expected = "devblogs.microsoft.com/aspnet/";

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveUrlProtocol_Urlhttps_ReturnsUrlWithoutProtocol()
        {
            // Arrange
            Blog blog = new Blog { BlogId = 1, Url = "https://devblogs.microsoft.com/aspnet/" };

            // Act
            string actual = blog.RemoveUrlProtocol();
            string expected = "devblogs.microsoft.com/aspnet/";

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveUrlProtocol_UrlNoProtocol_ReturnsUrl()
        {
            // Arrange
            Blog blog = new Blog { BlogId = 1, Url = "devblogs.microsoft.com/aspnet/" };

            // Act
            string actual = blog.RemoveUrlProtocol();
            string expected = "devblogs.microsoft.com/aspnet/";

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
