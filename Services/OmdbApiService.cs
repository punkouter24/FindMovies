using FindMovies.Models;
using System.Net.Http.Json;

namespace FindMovies.Services
{
    public class OmdbApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "24d49563"; // Your OMDB API key

        public OmdbApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Movie>> SearchMovies(string title, int pageNumber)
        {
            try
            {
                OmdbApiResponse? response = await _httpClient.GetFromJsonAsync<OmdbApiResponse>($"https://www.omdbapi.com/?s={title}&page={pageNumber}&apikey={_apiKey}");
                return response?.Search ?? new List<Movie>();
            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP-specific exceptions, e.g., network errors
                Console.WriteLine($"HTTP Request Error: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                Console.WriteLine($"Error in SearchMovies: {ex.Message}");
            }
            return new List<Movie>();
        }


        public async Task<List<Movie>> SearchMoviesByYear(string year)
        {
            try
            {
                OmdbApiResponse? response = await _httpClient.GetFromJsonAsync<OmdbApiResponse>($"https://www.omdbapi.com/?y=2005&s=xxx&apikey={_apiKey}");
                return response?.Search ?? new List<Movie>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchMoviesByYear: {ex.Message}");
                return new List<Movie>();
            }
        }

        public class OmdbApiResponse
        {
            public required List<Movie> Search { get; set; }
        }
    }
}
