using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Core.Domain.Enums;
using Core.DTO.v1_0.AddressTables;
using Core.DTO.v1_0.Entities;
using Core.DTO.v1_0.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Shared.Helpers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Test.Integration.API;

[Collection("NonParallel")]
public class ApiHappyFlowTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private string NickName { get; } = "test";
    private string Email { get; } = "test@test.ee";
    private string Pass { get; } = "Kala.maja0";

    public ApiHappyFlowTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact]
    public async Task IndexRequiresLogin()
    {
        // Act
        var response = await _client.GetAsync("/api/v1/Avatar/GetAll");
        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // [Fact]
    public async Task RegisterNewUser()
    {
        var response = await _client.PostAsJsonAsync("/api/v1/identity/Account/Register", new
        {
            nickName = NickName,
            email = Email,
            password = Pass,
        });
        // var contentStr = await response.Content.ReadAsStringAsync();
        // response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Login()
    {
        await RegisterNewUser();
        var response = await _client.PostAsJsonAsync("/api/v1/identity/Account/Login", new
        {
            email = Email,
            password = Pass,
        });
        var contentStr = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        var loginData = JsonSerializer.Deserialize<JWTResponse>(contentStr, JsonHelper.CamelCase);
        
        Assert.NotNull(loginData);
        Assert.NotNull(loginData.Jwt);
        Assert.True(loginData.Jwt.Length > 0);
        
        var msg = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Avatar/GetAll");
        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
        msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        response = await _client.SendAsync(msg);
        
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateNewAvatar()
    {
        await RegisterNewUser();
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/identity/Account/Login", new
        {
            email = Email,
            password = Pass,
        });
        var loginContentStr = await loginResponse.Content.ReadAsStringAsync();
        loginResponse.EnsureSuccessStatusCode();
        var loginData = JsonSerializer.Deserialize<JWTResponse>(loginContentStr, JsonHelper.CamelCase);
        
        var msg = new HttpRequestMessage(HttpMethod.Post, "/api/v1/Avatar/AddAvatar");
        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
        msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        msg.Content = JsonContent.Create(new
        {
            Sex = Enum.GetName(ESex.Male)
        });
        var response = await _client.SendAsync(msg);
        
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateItem()
    {
        await RegisterNewUser();
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/identity/Account/Login", new
        {
            email = Email,
            password = Pass,
        });
        var loginContentStr = await loginResponse.Content.ReadAsStringAsync();
        loginResponse.EnsureSuccessStatusCode();
        var loginData = JsonSerializer.Deserialize<JWTResponse>(loginContentStr, JsonHelper.CamelCase);
        
        var msg = new HttpRequestMessage(HttpMethod.Post, "/api/v1/Item/AddItem");
        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
        msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        msg.Content = JsonContent.Create(new
        {
            Name = "Donut",
            Description = "Food",
            IsConsumable = true,
            Price = 10,
            ItemRarity = ERarity.Common
        });
        
        var response = await _client.SendAsync(msg);
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task BuyItem()
    {
        await RegisterNewUser();
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/identity/Account/Login", new
        {
            email = Email,
            password = Pass,
        });
        var loginContentStr = await loginResponse.Content.ReadAsStringAsync();
        loginResponse.EnsureSuccessStatusCode();
        var loginData = JsonSerializer.Deserialize<JWTResponse>(loginContentStr, JsonHelper.CamelCase);

        await CreateNewAvatar();
        await CreateItem();
        
        var msg1 = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Item/GetAll");
        msg1.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
        msg1.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response1 = await _client.SendAsync(msg1);
        var data = await response1.Content.ReadAsStringAsync();
        var item = JsonConvert.DeserializeObject<List<Item>>(data)!.First();
        Assert.NotNull(item);
        
        var msg2 = new HttpRequestMessage(HttpMethod.Post, "/api/v1/Item/BuyItem");
        msg2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
        msg2.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        msg2.Content = JsonContent.Create(item.Id);

        var response2 = await _client.SendAsync(msg2);
        response2.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CheckViaOwns()
    {
        await RegisterNewUser();
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/identity/Account/Login", new
        {
            email = Email,
            password = Pass,
        });
        var loginContentStr = await loginResponse.Content.ReadAsStringAsync();
        loginResponse.EnsureSuccessStatusCode();
        var loginData = JsonSerializer.Deserialize<JWTResponse>(loginContentStr, JsonHelper.CamelCase);

        await BuyItem();
        
        var msg = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Owns/GetAll");
        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
        msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        var response = await _client.SendAsync(msg);
        response.EnsureSuccessStatusCode();
    }

    // [Fact]
    // public async Task UseItemAndCheckViaOwns()
    // {
    //     await RegisterNewUser();
    //     var loginResponse = await _client.PostAsJsonAsync("/api/v1/identity/Account/Login", new
    //     {
    //         email = Email,
    //         password = Pass,
    //     });
    //     var loginContentStr = await loginResponse.Content.ReadAsStringAsync();
    //     loginResponse.EnsureSuccessStatusCode();
    //     var loginData = JsonSerializer.Deserialize<JWTResponse>(loginContentStr, JsonHelper.CamelCase);
    //
    //     await BuyItem();
    //     
    //     var msg = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Owns/GetAll");
    //     msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
    //     msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //
    //     var response = await _client.SendAsync(msg);
    //     var data = await response.Content.ReadAsStringAsync();
    //     var owns = JsonConvert.DeserializeObject<List<Owns>>(data)!.First();
    //     
    //     var msg2 = new HttpRequestMessage(HttpMethod.Post, "/api/v1/Item/UseItem");
    //     msg2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
    //     msg2.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //     msg2.Content = JsonContent.Create(owns.Id);
    //     
    //     var response2 = await _client.SendAsync(msg2);
    //     response2.EnsureSuccessStatusCode();
    //     
    //     var msg3 = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Owns/GetAll");
    //     msg3.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
    //     msg3.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    //
    //     var response3 = await _client.SendAsync(msg3);
    //     Assert.Equal(HttpStatusCode.NotFound, response3.StatusCode);
    // }
}