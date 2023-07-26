using TvShow.Repositories;
using TvShow.Services;

namespace TestTvShows
{
    public class TestTvShow
    {
        private readonly TvShowApp _appTvShow;
        private readonly ITvShowRepository _repository;
        private readonly TvShowContext _context;
        private readonly TvShowApiClient _client;

        public TestTvShow()
        {
            _context = new TvShowContext();
            _client = new TvShowApiClient();
            _repository = new TvShowRepository(_context, _client);
            _appTvShow = new TvShowApp(_repository);
        }

        [Fact]
        public async void TestGetAll()
        {
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            await _appTvShow.ShowTvShows();

            string output = consoleOutput.ToString();
            Assert.Contains("List of TV shows", output);
        }

        [Fact]
        public async void TestGetById()
        {
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            await _appTvShow.ShowTvShowById(1);

            string output = consoleOutput.ToString();
            Assert.Contains("TV Show Information", output);
        }

        [Fact]
        public async void TestShowFavorites()
        {
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            await _appTvShow.ShowFavorites();

            string output = consoleOutput.ToString();
            Assert.Contains("Favorite TV Shows", output);
        }

        [Fact]
        public async void TestMarkFavorite()
        {
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            await _appTvShow.MarkAsFavorite(1);

            string output = consoleOutput.ToString();
            Assert.Contains("marked", output);
        }

        [Fact]
        public async void TestAddTvShow()
        {
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            await _appTvShow.AddTvShow(100);

            string output = consoleOutput.ToString();
            Assert.Contains("TV Show Added", output);
        }
    }
}