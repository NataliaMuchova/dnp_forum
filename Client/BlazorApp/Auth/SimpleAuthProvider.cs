using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Security.Claims;
using System.Net.Http.Json;
using System.Text.Json;
using DTOs;

namespace BlazorApp.Auth;

public class SimpleAuthProvider : AuthenticationStateProvider
{
  
    private readonly HttpClient httpClient;
    private ClaimsPrincipal currentClaimsPrincipal;

    public SimpleAuthProvider(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(currentClaimsPrincipal ?? new()));
    }
    public async Task Login(string userName, string password)
    {
        var response = await httpClient.PostAsJsonAsync("auth", 
        new LoginRequest(userName, password));
    
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        UserDto userDto = JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        List<Claim> claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, userDto.UserName),
        new Claim("Id", userDto.Id.ToString()),
        // new Claim("DateOfBirth", userDto.DateOfBirth.ToString("yyyy-MM-dd")),
        // new Claim("Role", userDto.Role),
        // new Claim("Email", userDto.Email)
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");

        currentClaimsPrincipal = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));

    }
    public void Logout()
    {
        currentClaimsPrincipal = new();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal))); 
    }
}
