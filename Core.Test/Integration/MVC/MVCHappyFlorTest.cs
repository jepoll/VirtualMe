using System.Collections;
using System.Net;
using System.Net.Http.Headers;
using AngleSharp.Html.Dom;
using Core.Domain.Enums;
using Core.DTO.v1_0.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Shared.Test.Helpers;

namespace Core.Test.Integration.MVC;

[Collection("NonParallel")]
public class MVCHappyFlorTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private string NickName { get; } = "test";
    private string Email { get; } = "test@test.ee";
    private string Pass { get; } = "Kala.maja0";
    
    public MVCHappyFlorTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    // [Fact]
    public async Task RegisterNewUserWithAvatar()
    {
        const string registerUri = "/Identity/Account/Register";
        var getRegisterResponse = await _client.GetAsync(registerUri);
        getRegisterResponse.EnsureSuccessStatusCode();

        var getRegisterContent = await HtmlHelpers.GetDocumentAsync(getRegisterResponse);
        
        var formRegister = (IHtmlFormElement) getRegisterContent.QuerySelector("#registerForm")!;
        var formRegisterValues = new Dictionary<string, string>
        {
            ["Input_NickName"] = NickName,
            ["Input_Email"] = Email,
            ["Input_Password"] = Pass,
            ["Input_ConfirmPassword"] = Pass,
        };
        
        var postRegisterResponse = await _client.SendAsync(formRegister, formRegisterValues);

        Assert.True(postRegisterResponse.StatusCode is HttpStatusCode.Found or HttpStatusCode.OK);

        const string protectedUri = "";
        var getResponse = await _client.GetAsync(protectedUri);
        //Redirects to avatarCreationPage
        Assert.True(getResponse.StatusCode is HttpStatusCode.Found or HttpStatusCode.OK);

        var getAvatarCreationForm = await _client.GetAsync($"/Avatar/Create/");
        getRegisterResponse.EnsureSuccessStatusCode();
        var getContent = await HtmlHelpers.GetDocumentAsync(getAvatarCreationForm);
        var formAvatar = (IHtmlFormElement) getContent.QuerySelector("#avatarForm")!;
        var formAvatarValues = new Dictionary<string, string>
        {
            ["Select_Sex"] = ESex.Male.ToString()
        };
        var postAvatarResponse = await _client.SendAsync(formAvatar, formAvatarValues);
        
        Assert.Equal(HttpStatusCode.Found, postAvatarResponse.StatusCode);
    }

    [Fact]
    public async Task CreateActivityType()
    {
        var getLoginResponse = await _client.GetAsync("/Identity/Account/Login");
        getLoginResponse.EnsureSuccessStatusCode();
        var getLoginContent = await HtmlHelpers.GetDocumentAsync(getLoginResponse);
        
        var formLogin = (IHtmlFormElement) getLoginContent.QuerySelector("#account")!;
        var formLoginValues = new Dictionary<string, string>
        {
            ["Input_Email"] = "Admin@eesti.ee",
            ["Input_Password"] = "Kala.maja1",
        };
        var postLoginResponse = await _client.SendAsync(formLogin, formLoginValues);
        Assert.Equal(HttpStatusCode.Found, postLoginResponse.StatusCode);

        
        var getActivityTypeCreateResponse = await _client.GetAsync("/Admin/ActivityType/Create");
        getActivityTypeCreateResponse.EnsureSuccessStatusCode();
        var getActivityTypeCreateContent = await HtmlHelpers.GetDocumentAsync(getActivityTypeCreateResponse);

        var formActivityTypeCreate = (IHtmlFormElement) getActivityTypeCreateContent.QuerySelector("#activityTypeCreate")!;
        var formActivityTypeCreateValues = new Dictionary<string, string>
        {
            ["ActivityType.Name"] = "Test",
            ["ActivityType.DaysToComplete"] = "0",
            ["ActivityType.HoursToComplete"] = "0",
            ["ActivityType.MinutesToComplete"] = "1",
            ["ActivityType.Exp"] = "200",
            ["ActivityType.Value"] = "200",
            ["ActivityType.Money"] = "5"
        };
        var postActivityTypeCreateResponse =
            await _client.SendAsync(formActivityTypeCreate, formActivityTypeCreateValues);
        Assert.Equal(HttpStatusCode.Found, postActivityTypeCreateResponse.StatusCode);
    }

    [Fact]
    public async Task CreateActivity()
    {
        await CreateActivityType();
        
        var getLoginResponse = await _client.GetAsync("/Identity/Account/Login");
        getLoginResponse.EnsureSuccessStatusCode();
        var getLoginContent = await HtmlHelpers.GetDocumentAsync(getLoginResponse);
        
        var formLogin = (IHtmlFormElement) getLoginContent.QuerySelector("#account")!;
        var formLoginValues = new Dictionary<string, string>
        {
            ["Input_Email"] = Email,
            ["Input_Password"] = Pass,
        };
        var postLoginResponse = await _client.SendAsync(formLogin, formLoginValues);
        Assert.Equal(HttpStatusCode.Found, postLoginResponse.StatusCode);

        var getActivityCreateResponse = await _client.GetAsync("/Activity/Create");
        getActivityCreateResponse.EnsureSuccessStatusCode();
        var getActivityCreateContent = await HtmlHelpers.GetDocumentAsync(getActivityCreateResponse);

        var formActivityCreate = (IHtmlFormElement)getActivityCreateContent.QuerySelector("#activityCreate")!;
        var formActivityCreateValues = new Dictionary<string, string>
        {
            // ["Activity.ActivityTypeId"] = "1", // Предполагается, что значение "1" существует в Model.Types
            // ["Activity.Stat"] = "Strength", // Предполагается, что значение "Strength" существует в Model.Stats
            ["Activity.StrengthLimit"] = "10",
            ["Activity.DexterityLimit"] = "5",
            ["Activity.IntelligenceLimit"] = "8",
            ["Activity.LevelLimit"] = "3",
            ["Activity.StressGain"] = "2",
            ["Activity.StaminaDrain"] = "4",
            ["Activity.Name"] = "Morning Jog",
            ["Activity.Description"] = "A light morning jog to start the day."
        };
        var postActivityCreateResponse = await _client.SendAsync(formActivityCreate, formActivityCreateValues);
        Assert.Equal(HttpStatusCode.Found, postLoginResponse.StatusCode);

        // var getActivities = await _client.GetAsync($"Activity");
        // getLoginResponse.EnsureSuccessStatusCode();
        // var getActivitiesContent = await HtmlHelpers.GetDocumentAsync(getActivities);
    }

    // [Fact]
    public async Task CreateItem()
    {
        await RegisterNewUserWithAvatar();
        
        var getLoginResponse = await _client.GetAsync("/Identity/Account/Login");
        getLoginResponse.EnsureSuccessStatusCode();
        var getLoginContent = await HtmlHelpers.GetDocumentAsync(getLoginResponse);
        
        var formLogin = (IHtmlFormElement) getLoginContent.QuerySelector("#account")!;
        var formLoginValues = new Dictionary<string, string>
        {
            ["Input_Email"] = Email,
            ["Input_Password"] = Pass,
        };
        var postLoginResponse = await _client.SendAsync(formLogin, formLoginValues);
        Assert.Equal(HttpStatusCode.Found, postLoginResponse.StatusCode);

        var getItemCreateResponse = await _client.GetAsync("/Item/Create");
        getItemCreateResponse.EnsureSuccessStatusCode();
        var getItemCreateContent = await HtmlHelpers.GetDocumentAsync(getItemCreateResponse);
        
        var formItemCreate = (IHtmlFormElement) getItemCreateContent.QuerySelector("#itemCreate")!;
        var formItemCreateValues = new Dictionary<string, string>
        {
            ["Item.Name"] = "TestItem",
            ["Item.Description"] = "TestItemDescription",
            ["Item.Rarity"] = ERarity.Common.ToString(),
            ["Item.Price"] = "5",
            ["Item.IsConsumable"] = "true",
        };
        var postItemCreate = await _client.SendAsync(formItemCreate, formItemCreateValues);
        Assert.Equal(HttpStatusCode.Found, postItemCreate.StatusCode);
    }

    [Fact]
    public async Task BuyItem()
    {
        await CreateItem();
        
        var getLoginResponse = await _client.GetAsync("/Identity/Account/Login");
        getLoginResponse.EnsureSuccessStatusCode();
        var getLoginContent = await HtmlHelpers.GetDocumentAsync(getLoginResponse);
        
        var formLogin = (IHtmlFormElement) getLoginContent.QuerySelector("#account")!;
        var formLoginValues = new Dictionary<string, string>
        {
            ["Input_Email"] = Email,
            ["Input_Password"] = Pass,
        };
        var postLoginResponse = await _client.SendAsync(formLogin, formLoginValues);
        Assert.Equal(HttpStatusCode.Found, postLoginResponse.StatusCode);

        var getItemsResponse = await _client.GetAsync("/Item");
        getItemsResponse.EnsureSuccessStatusCode();
        var getItemsContent = await HtmlHelpers.GetDocumentAsync(getItemsResponse);
        
        var buyItemLink = getItemsContent.QuerySelectorAll("a").First(e => e.InnerHtml.Contains("Buy"));
        var itemId = buyItemLink.Id!.Replace("buyItem-", "");

        var getBuyItem = await _client.GetAsync("/Item/BuyItem?itemId=" + itemId);
        Assert.Equal(HttpStatusCode.Found, postLoginResponse.StatusCode);
    }
}