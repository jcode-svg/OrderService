using Microsoft.Extensions.Options;
using OrderService.Domain.Aggregates.OrderAggregate.DTO.Request;
using OrderService.Domain.DTOs;
using OrderService.Infrastructure.HttpAdapter.Contract;
using OrderService.SharedKernel;
using OrderService.SharedKernel.ConfigurationModels;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace OrderService.Infrastructure.HttpAdapter.Implementation;

public class ProductPlatform : IProductPlatform
{
    private readonly HttpClient _httpClient;
    private readonly ProductPLatformSettings _config;
    public ProductPlatform(HttpClient httpClient, IOptions<ProductPLatformSettings> config)
    {
        _config = config.Value ?? throw new ArgumentNullException(nameof(config));
        _httpClient = httpClient; _httpClient.BaseAddress = new Uri(_config.BaseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Add(new
        MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.Timeout = new TimeSpan(0, 0, 5000);
    }

    public async Task<ResponseWrapper<IEnumerable<ProductDTO>>> GetAllProduct()
    {
        ResponseWrapper<IEnumerable<ProductDTO>> response;

        var httpResponse = await _httpClient.GetAsync($"api/Products/AllProducts");

        Stream data = await httpResponse.Content.ReadAsStreamAsync();

        if (httpResponse.IsSuccessStatusCode)
        {
            response = await JsonSerializer.DeserializeAsync<ResponseWrapper<IEnumerable<ProductDTO>>>(data);
        }
        else
        {
            response = new ResponseWrapper<IEnumerable<ProductDTO>>
            {
                IsSuccessful = false,
                Message = "Could not retrieve products at the moment, please try again later",
            };
        }

        return response;
    }

    public async Task<ResponseWrapper<ProductDTO>> GetProduct(string id)
    {
        ResponseWrapper<ProductDTO> response;

        var httpResponse = await _httpClient.GetAsync($"api/products/product/{id}");

        Stream data = await httpResponse.Content.ReadAsStreamAsync();

        if (httpResponse.IsSuccessStatusCode)
        {
            response = await JsonSerializer.DeserializeAsync<ResponseWrapper<ProductDTO>>(data);
        }
        else
        {
            response = new ResponseWrapper<ProductDTO>
            {
                IsSuccessful = false,
                Message = "Could not retrieve product at the moment, please try again later",
            };
        }

        return response;
    }
}
