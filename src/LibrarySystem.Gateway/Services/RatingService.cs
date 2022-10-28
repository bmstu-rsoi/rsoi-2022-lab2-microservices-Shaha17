using LibrarySystem.Gateway.DTO;
using LibrarySystem.RatingSystem.DTO;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Quic;

namespace LibrarySystem.Gateway.Services;

public class RatingService
{
    private readonly HttpClient _httpClient;

    public RatingService(string ratingServiceHost)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri($"http://{ratingServiceHost}/");
    }

    public async Task<UserRatingResponse?> GetUserRatingAsync(string username)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, "api/v1/rating");
        req.Headers.Add("X-User-Name", username);
        using var res = await _httpClient.SendAsync(req);
        var response = await res.Content.ReadFromJsonAsync<UserRatingResponse>();
        return response;
    }

    public async Task<UserRatingResponse?> ChangeUserRating(string username, int value)
    {
        using var req = new HttpRequestMessage(HttpMethod.Patch, "api/v1/rating");
        req.Headers.Add("X-User-Name", username);
        req.Content = JsonContent.Create(new ChangeUserRatingRequest()
        {
            Value = value
        });
        using var res = await _httpClient.SendAsync(req);
        var response = await res.Content.ReadFromJsonAsync<UserRatingResponse>();
        return response;
    }
}