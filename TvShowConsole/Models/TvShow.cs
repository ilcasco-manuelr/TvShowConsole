using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvShow.Models;
/// <summary>
/// Database Model
/// </summary>
public class TvShowModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int IdSerie { get; set; }
    public string? Name { get; set; }
    public string? OfficialSite { get; set; }
    public string? Genres { get; set; }
    public string? Summary { get; set; }
    public bool IsFavorite { get; set; }
}
