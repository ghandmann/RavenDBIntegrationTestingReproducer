using System.Threading.Tasks;
using Raven.TestDriver;
using Xunit;

namespace SomeProject.Tests
{
    public class MovieTests : RavenTestDriver
    {
        public class Movie {
            public string Id { get; set; }
            public string Title { get; set; }
        }

        [Fact]
        public async Task StoreAndLoadMovie()
        {
            ConfigureServer(new TestServerOptions() {
                CommandLineArgs = new System.Collections.Generic.List<string> { "--RunInMemory=true", },
                FrameworkVersion = null,
            });

            using(var store = GetDocumentStore())
            {
                using(var session = store.OpenAsyncSession())
                {
                    await session.StoreAsync(new Movie { Id = "1", Title = "Some Movie"});
                    await session.SaveChangesAsync();
                }

                using(var session = store.OpenAsyncSession())
                {
                    var movie = await session.LoadAsync<Movie>("1");
                    Assert.NotNull(movie);
                }
            }
        }

        [Fact]
        public async Task StoreAndLoadMovie_Duplicated()
        {
            ConfigureServer(new TestServerOptions() {
                CommandLineArgs = new System.Collections.Generic.List<string> { "--RunInMemory=true", },
                FrameworkVersion = null,
            });
            
            using(var store = GetDocumentStore())
            {
                using(var session = store.OpenAsyncSession())
                {
                    await session.StoreAsync(new Movie { Id = "1", Title = "Some Movie"});
                    await session.SaveChangesAsync();
                }

                using(var session = store.OpenAsyncSession())
                {
                    var movie = await session.LoadAsync<Movie>("1");
                    Assert.NotNull(movie);
                }
            }
        }
    }
}
