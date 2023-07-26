using Newtonsoft.Json;
using TvShow.Models;
/// <summary>
/// Class to connect to API 
/// for now only gets information for specific TV show
/// </summary>
public class TvShowApiClient
{
    private readonly HttpClient _httpClient;

    //Class constructor 
    public TvShowApiClient()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://api.tvmaze.com/");
    }

    public async Task<TvShowModelResponse> GetTvShowById(int id)
    {
        // wait for the response from https://api.tvmaze.com/shows/{id}
        HttpResponseMessage response = await _httpClient.GetAsync($"shows/{id}");
        if (response.IsSuccessStatusCode)
        {
            //Transform JSON to a valid Model
            string json = await response.Content.ReadAsStringAsync();
            var tvShowInfo = JsonConvert.DeserializeObject<TvShowModelResponse>(json);
            return tvShowInfo;
        }
        // Error getting information from API
        else
        {
            Console.WriteLine($"Error connecting to API, please retry later ==> {response.ReasonPhrase} ");
            return null;
        }
    }
}
