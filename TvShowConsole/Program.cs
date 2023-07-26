using TvShow.Repositories;
using TvShow.Services;

namespace TvShow;
class Program
{
    private const string DatabaseFileName = "tvshows.db";
    private static readonly string DatabaseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", DatabaseFileName);
    static void Main(string[] args)
    {

        Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        if (!File.Exists(DatabaseFilePath))
        {
            // If database not exist, create
            using (var contextcreate = new TvShowContext())
            {
                contextcreate.Database.EnsureCreated();
            }
        }

        var context = new TvShowContext();
        var apiClient = new TvShowApiClient();
        var repository = new TvShowRepository(context, apiClient);
        var tvShowApp = new TvShowApp(repository);
        tvShowApp.Run();
    }
}
