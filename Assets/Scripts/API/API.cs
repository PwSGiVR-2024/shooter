using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class API
{
    private readonly HttpClient _client;
    private readonly string _baseAddress;

    public API(string baseAddress)
    {
        _baseAddress = baseAddress;
        _client = new HttpClient { BaseAddress = new Uri(_baseAddress) };
    }

    public async Task Register(string username, string pass)
    {
        string json = JsonUtility.ToJson(new User { username = username, pass = pass });
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("auth/register", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new APIException(response.ReasonPhrase);
        }
    }

    public async Task<IEnumerable<string>> Login(string username, string pass)
    {
        string json = JsonUtility.ToJson(new User { username = username, pass = pass });
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("auth/login", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new APIException(response.ReasonPhrase);
        }

        return response.Headers.GetValues("Set-Cookie");
    }

    public async Task Authenticate(IEnumerable<string> cookies)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "auth");
        request.Headers.Add("Cookie", string.Join(";", cookies));

        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new APIException(response.ReasonPhrase);
        }
    }
}