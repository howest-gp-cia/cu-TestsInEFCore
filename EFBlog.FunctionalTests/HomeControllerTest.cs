using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EFBlog.FunctionalTests
{
    public class HomeControllerTest: IClassFixture<WebTestFixture>
    {
        public HomeControllerTest(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Index")]
        [InlineData("/Home/Privacy")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            // Arrange


            // Act
            var response = await Client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("/Home/aaaa")]
        [InlineData("/Home/bbbb")]
        public async Task Get_WrongEndpointsHasNoSuccesCode(string url)
        {
            // Arrange


            // Act
            var response = await Client.GetAsync(url);

            // Assert
            // Status Code 200-299  
            Assert.Throws<HttpRequestException>(() => response.EnsureSuccessStatusCode());
        }


        [Fact]
        public async Task Get_Home_ShowsBlogs()
        {
            // Arrange 
            var response = string.Empty;

            // Act
            var result = await Client.GetAsync("/Home/");
            if (result.IsSuccessStatusCode)
            {
                response = await result.Content.ReadAsStringAsync();
            }
            

            // Assert
            Assert.Contains("https://devblogs.microsoft.com/aspnet/", response);
        }


        static async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(u, c);
                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            }
            return response;
        }
    }
}
