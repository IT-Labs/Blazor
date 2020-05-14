using System;
using System.Collections.Generic;
using Xunit;
using Bunit;
using BlazorApp.Client.Pages;
using Microsoft.Extensions.DependencyInjection;
using BlazorApp.Client.Pages.Movies;
using Telerik.JustMock;
using BlazorApp.Client.Interfaces;
using BlazorApp.Shared.Requests.Movies;
using Core.Shared.Response;
using BlazorApp.Shared.Entities;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests : ComponentTestFixture
    {
        [Fact]
        public void TestCounter()
        {
            // Arrange
            var cut = RenderComponent<Counter>();
            cut.Find("p").MarkupMatches("<p>Current count: 0</p>");

            // Act
            var element = cut.Find("button");
            element.Click();

            //Assert
            cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
        }

        [Fact]
        public void TestMovies_NoData()
        { 
            // Arrange
            var moviesServiceMock = Mock.Create<IMoviesService>();
            Mock.Arrange(() => moviesServiceMock.GetMultiple(Arg.IsAny<GetMoviesRequest>()))
                .Returns(new TaskCompletionSource<PagedResponse<Movie>>().Task);
            Services.AddSingleton<IMoviesService>(moviesServiceMock);
            Services.AddLogging();

            var renderedComponent = RenderComponent<Movies>();

            Assert.Equal("Create new movie", renderedComponent.Find("a").FirstChild.TextContent);
            Assert.Equal("Loading...", renderedComponent.Find("em").FirstChild.TextContent);
        }

        [Fact]
        public void TestMovies_PredefinedData()
        {
            // Arrange
            var moviesServiceMock = Mock.Create<IMoviesService>();

            var movie = new Movie()
            {
                Title = "Inception",
                ReleaseDate = new DateTime(2010, 7, 16),
                Image = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_SY1000_CR0,0,675,1000_AL_.jpg"
            };

            var response = new PagedResponse<Movie>()
            {
                Status = System.Net.HttpStatusCode.OK,
                Payload = new List<Movie> { movie },
                Meta = new Core.Shared.Meta
                {
                    CurrentPage = 1,
                    TotalCount = 1
                }
            };

            Mock.Arrange(() => moviesServiceMock.GetMultiple(Arg.IsAny<GetMoviesRequest>()))
                .Returns(Task.FromResult<PagedResponse<Movie>>(response));
            Services.AddSingleton<IMoviesService>(moviesServiceMock);
            Services.AddLogging();
            //Services.AddDevExpressBlazor();

            // Act - render the FetchData component
            var renderedComponent = RenderComponent<Movies>();

            // Assert
            Assert.Equal("Create new movie", renderedComponent.Find("a").FirstChild.TextContent);

            var body = renderedComponent.Find("tbody").TextContent;
            Assert.True(body.Contains(movie.Title));
            Assert.True(body.Contains(movie.ReleaseDate.ToString()));
        }
    }
    
}
