using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ApiGatewayClient
{
    private readonly HttpClient _httpClient;

    public ApiGatewayClient(string baseUri)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUri)
        };
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(data);
    }

    public async Task PostAsync<T>(string endpoint, T data)
    {
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        // Puedes procesar la respuesta si es necesario
    }

    public async Task PutAsync<T>(string endpoint, T data)
    {
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        // Puedes procesar la respuesta si es necesario
    }

    public async Task DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
        // Puedes procesar la respuesta si es necesario
    }

    // Métodos adicionales para otros endpoints según sea necesario
}
