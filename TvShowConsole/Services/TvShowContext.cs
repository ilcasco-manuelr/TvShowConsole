using Microsoft.EntityFrameworkCore;
using TvShow.Models;

namespace TvShow.Services;
public class TvShowContext : DbContext
{
    private const string DatabaseFileName = "tvshows.db";
    private static readonly string DatabaseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", DatabaseFileName);

    public DbSet<TvShowModel> TvShows { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DatabaseFilePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Initial Seed
        modelBuilder.Entity<TvShowModel>().ToTable("TvShows");
        modelBuilder.Entity<TvShowModel>().HasData(
            new TvShowModel { Id = 1, IdSerie = 1, Name = "Under the Dome", IsFavorite = false, Genres = "Drama, Science-Fiction, Thriller", Summary = "<p><b>Under the Dome</b> is the story of a small town that is suddenly and inexplicably sealed off from the rest of the world by an enormous transparent dome. The town's inhabitants must deal with surviving the post-apocalyptic conditions while searching for answers about the dome, where it came from and if and when it will go away.</p>", OfficialSite = "http://www.cbs.com/shows/under-the-dome/" },
            new TvShowModel { Id = 2, IdSerie = 2, Name = "Person of Interest", IsFavorite = false, Genres = "Action, Crime, Science-Fiction", Summary = "<p>You are being watched. The government has a secret system, a machine that spies on you every hour of every day. I know because I built it. I designed the Machine to detect acts of terror but it sees everything. Violent crimes involving ordinary people. People like you. Crimes the government considered \"irrelevant\". They wouldn't act so I decided I would. But I needed a partner. Someone with the skills to intervene. Hunted by the authorities, we work in secret. You'll never find us. But victim or perpetrator, if your number is up, we'll find you.</p>", OfficialSite = "http://www.cbs.com/shows/person_of_interest/" },
            new TvShowModel { Id = 3, IdSerie = 3, Name = "Bitten", IsFavorite = false, Genres = "Drama, Horror, Romance", Summary = "<p>Based on the critically acclaimed series of novels from Kelley Armstrong. Set in Toronto and upper New York State, <b>Bitten</b> follows the adventures of 28-year-old Elena Michaels, the world's only female werewolf. An orphan, Elena thought she finally found her \"happily ever after\" with her new love Clayton, until her life changed forever. With one small bite, the normal life she craved was taken away and she was left to survive life with the Pack.</p>", OfficialSite = "http://bitten.space.ca/" },
            new TvShowModel { Id = 4, IdSerie = 4, Name = "Arrow", IsFavorite = false, Genres = "Drama, Action, Science-Fiction", Summary = "<p>After a violent shipwreck, billionaire playboy Oliver Queen was missing and presumed dead for five years before being discovered alive on a remote island in the Pacific. He returned home to Starling City, welcomed by his devoted mother Moira, beloved sister Thea and former flame Laurel Lance. With the aid of his trusted chauffeur/bodyguard John Diggle, the computer-hacking skills of Felicity Smoak and the occasional, reluctant assistance of former police detective, now beat cop, Quentin Lance, Oliver has been waging a one-man war on crime.</p>", OfficialSite = "http://www.cwtv.com/shows/arrow" },
            new TvShowModel { Id = 5, IdSerie = 5, Name = "True Detective", IsFavorite = false, Genres = "Drama, Crime, Thriller", Summary = "<p>Touch darkness and darkness touches you back. <b>True Detective</b> centers on troubled cops and the investigations that drive them to the edge. Each season features a new cast and a new case.</p><p><i><b>True Detective</b></i> is an American anthology crime drama television series created and written by Nic Pizzolatto. </p>", OfficialSite = "http://www.hbo.com/true-detective" }
        );
    }
}
