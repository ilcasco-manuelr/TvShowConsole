namespace TvShow.Models;
/// <summary>
/// Model for Response Data
/// </summary>
public class TvShowModelResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? OfficialSite { get; set; }
    public string[] Genres { get; set; }

    public string? Summary { get; set; }
}