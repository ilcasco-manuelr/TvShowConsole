using TvShow.Models;
namespace TvShow.Repositories;

public interface ITvShowRepository
{
    Task<List<TvShowModel>> GetAll();
    Task<TvShowModel> GetById(int id);
    TvShowModel AddById(int id);
    Task MarkAsFavorite(int id, bool isFavorite);
}
