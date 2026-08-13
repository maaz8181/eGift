namespace eGift.Admin.Helpers;

public class WebClientHelper
{
    private readonly HttpClient _httpClient;

    public WebClientHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string url,
        TRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(url, request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse?> PutAsync<TRequest, TResponse>(
        string url,
        TRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync(url, request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task DeleteAsync(string url)
    {
        var response = await _httpClient.DeleteAsync(url);

        response.EnsureSuccessStatusCode();
    }
}