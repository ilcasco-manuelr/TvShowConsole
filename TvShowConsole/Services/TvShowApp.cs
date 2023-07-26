using TvShow.Models;
using TvShow.Repositories;

namespace TvShow.Services;
public class TvShowApp
{
    private readonly ITvShowRepository _repository;

    public TvShowApp(ITvShowRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Main Function to show options
    /// </summary>
    public async void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to TV Show List!");
            Console.WriteLine("\nAvailable Commands:\n");
            Console.WriteLine(" list".PadRight(15) + " View list of TV shows");
            Console.WriteLine(" info <Id>".PadRight(15) + " View Information of a specific TV Show");
            Console.WriteLine(" add <Id>".PadRight(15) + " Add Information to DB from tvmaze.com");
            Console.WriteLine(" favorites".PadRight(15) + " View list of favorite TV Shows");
            Console.WriteLine(" mark <Id>".PadRight(15) + " Mark/Unmark TV Show as favorite");
            Console.WriteLine(" exit".PadRight(15) + " Exit the application");

            Console.Write("\nEnter a command: ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            var commandParts = input.Split(' ', 2);
            var command = commandParts[0].ToLower();
            int tvShowId;
            var task = new List<Task>();
            //validation of input command 
            switch (command)
            {
                case "list":
                    task.Add(ShowTvShows());
                    break;

                case "info":
                    if (commandParts.Length == 2 && int.TryParse(commandParts[1], out tvShowId))
                    {
                        task.Add(ShowTvShowById(tvShowId));
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Usage: info <Id>");
                    }
                    break;

                case "add":
                    if (commandParts.Length == 2 && int.TryParse(commandParts[1], out tvShowId))
                    {
                        task.Add(AddTvShow(tvShowId));
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Usage: add <Id>");
                    }
                    break;

                case "favorites":
                    task.Add(ShowFavorites());
                    break;

                case "mark":
                    if (commandParts.Length == 2 && int.TryParse(commandParts[1], out tvShowId))
                    {
                        task.Add(MarkAsFavorite(tvShowId));
                    }
                    else
                    {
                        Console.WriteLine("Invalid command. Usage: mark <Id>");
                    }
                    break;

                case "exit":
                    return;

                default:
                    Console.WriteLine("Unrecognized command.");
                    break;
            }
            await Task.WhenAll(task);
            Console.WriteLine("\n\n==> Press any key to return to the menu <==");
            Console.ReadKey();
        }
    }
    /// <summary>
    /// Function to get all TV show from DB
    /// </summary>
    public async Task ShowTvShows()
    {
        try
        {
            var tvShows = await _repository.GetAll();

            Console.WriteLine("\nList of TV shows:\n");
            Console.WriteLine($"Id".PadRight(4) + "  Name");
            foreach (var tvShow in tvShows)
            {
                var favoriteSymbol = tvShow.IsFavorite ? "*" : " ";
                Console.WriteLine($"[{tvShow.IdSerie}]".PadRight(4) + $"{favoriteSymbol} {tvShow.Name}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error displaying TV Shows, please retry later ==> {e.Message} ");
        }
    }
    /// <summary>
    /// Function to get information from specific TV Show
    /// </summary>
    /// <param name="tvShowId"></param>
    public async Task ShowTvShowById(int tvShowId)
    {
        try
        {
            TvShowModel tvShow = await _repository.GetById(tvShowId);
            if (tvShow == null)
            {
                Console.WriteLine(" ====== No TV show found with that ID =====");
                return;
            }
            var favorite = tvShow.IsFavorite ? "Yes" : "No";
            Console.WriteLine("\n====== TV Show Information ======\n");
            ShowInfo(tvShow);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error displaying TV Show Id [{tvShowId}], please retry later ==> {e.Message} ");
        }
    }
    /// <summary>
    /// Fucntion to show all TV Shows marked as favorites
    /// </summary>
    /// <returns></returns>
    public async Task ShowFavorites()
    {
        try
        {
            List<TvShowModel> allTvShow = await _repository.GetAll();
            List<TvShowModel> favorites = allTvShow.Where(tv => tv.IsFavorite).ToList();

            Console.WriteLine("\nFavorite TV Shows:");
            foreach (TvShowModel favorite in favorites)
            {
                Console.WriteLine($"[{favorite.IdSerie}] {favorite.Name}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error displaying TV Show Favorites, please retry later ==> {e.Message} ");
        }
    }
    /// <summary>
    /// Function to mark/unmark a TV show favorite
    /// </summary>
    /// <param name="tvShowId"></param>
    /// <returns></returns>
    public async Task MarkAsFavorite(int tvShowId)
    {
        try
        {
            TvShowModel tvShow = await _repository.GetById(tvShowId);

            if (tvShow == null)
            {
                Console.WriteLine("No TV show found with that ID.");
                return;
            }

            tvShow.IsFavorite = !tvShow.IsFavorite;
            _repository.MarkAsFavorite(tvShow.IdSerie, tvShow.IsFavorite);
            Console.WriteLine($"The TV show [{tvShow.IdSerie}] {tvShow.Name} has been marked as {(tvShow.IsFavorite ? "favorite" : "not favorite")}.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error marking TV Show with Id {tvShowId}, please retry later ==> {e.Message} ");
        }
    }
    /// <summary>
    /// Function to add a new TV Show from API
    /// </summary>
    /// <param name="tvShowId"></param>
    /// <returns></returns>
    public async Task AddTvShow(int tvShowId)
    {
        try
        {
            TvShowModel tvShow = await _repository.GetById(tvShowId);
            if (tvShow != null)
            {
                Console.WriteLine($"TV Show [{tvShow.IdSerie}] {tvShow.Name} is already saved in DB.");
                return;
            }

            tvShow = _repository.AddById(tvShowId);
            Console.WriteLine("\n====== TV Show Added ======\n");
            ShowInfo(tvShow);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error adding TV Show with Id {tvShowId}, please retry later ==> {e.Message} ");
        }
    }

    public void ShowInfo(TvShowModel tvShow)
    {
        Console.WriteLine("Id:".PadRight(15) + $" {tvShow.IdSerie}");
        Console.WriteLine("Name:".PadRight(15) + $" {tvShow.Name}");
        Console.WriteLine("Is Favorite:".PadRight(15) + $" {tvShow.IsFavorite}");
        Console.WriteLine("Official Site:".PadRight(15) + $" {tvShow.OfficialSite}");
        Console.WriteLine("Genres:".PadRight(15) + $" {string.Join(", ", tvShow!.Genres)}");
        Console.WriteLine("Summary:".PadRight(15) + $" {tvShow.Summary}");
    }
}