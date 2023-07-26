using Microsoft.EntityFrameworkCore;
using TvShow.Models;
using TvShow.Services;

namespace TvShow.Repositories;
public class TvShowRepository : ITvShowRepository
{
    private readonly TvShowContext _context;
    private readonly TvShowApiClient _apiClient;

    public TvShowRepository(TvShowContext context, TvShowApiClient apiClient)
    {
        _context = context;
        _apiClient = apiClient;
    }

    //Get all TV shows in DB
    public async Task<List<TvShowModel>> GetAll() => await _context.TvShows.OrderBy(tv => tv.IdSerie).ToListAsync();

    // Get a specific TV Show by Id
    public async Task<TvShowModel> GetById(int id) => await _context.TvShows.FirstOrDefaultAsync(tvShow => tvShow.IdSerie == id);

    // Add a new TV show into DB from API
    public TvShowModel AddById(int id)
    {
        try
        {
            var tvShowInfo = _apiClient.GetTvShowById(id);
            //Validate id the tv show exists in API and get a result
            if (tvShowInfo != null)
            {
                //If exists then save some fields from API to our DB
                var tvShow = new TvShowModel
                {
                    IdSerie = tvShowInfo.Result.Id,
                    Genres = string.Join(",", tvShowInfo.Result.Genres),
                    IsFavorite = false,
                    Name = tvShowInfo.Result.Name,
                    OfficialSite = tvShowInfo.Result.OfficialSite,
                    Summary = tvShowInfo.Result.Summary
                };

                _context.TvShows.Add(tvShow);
                _context.SaveChanges();

                return tvShow;
            }
            //The specific ID not exists from API
            else
            {
                Console.WriteLine($"TV Show with Id [{id}] Not Found in api.tvmaze.com");
                return null;
            }
        }
        //An error getting information from API
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurs, please contact the admin ==> {ex.Message}");
            return null;
        }
    }

    public async Task MarkAsFavorite(int id, bool isFavorite)
    {
        //Get the tv show by specific ID
        var tvShow = await _context.TvShows.FirstOrDefaultAsync(tvShow => tvShow.IdSerie == id);

        //If exist then change the favorite field
        if (tvShow != null)
        {
            tvShow.IsFavorite = isFavorite;
            _context.SaveChangesAsync();
        }
    }
}
