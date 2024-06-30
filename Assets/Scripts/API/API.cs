using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GameDevWare.Serialization;
using UnityEngine;

internal class API
{
    private readonly HttpClient _client;
    private readonly string _baseAddress;

    internal API(string baseAddress)
    {
        _baseAddress = "http://" + baseAddress + ":5000/";
        _client = new HttpClient { BaseAddress = new Uri(_baseAddress) };
    }

    internal async Task Register(string username, string pass)
    {
        string json = JsonUtility.ToJson(new PostedUser { username = username, pass = pass });
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("auth/register", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new APIException(Deserialize(response));
        }
    }

    internal async Task<IEnumerable<string>> Login(string username, string pass)
    {
        string json = JsonUtility.ToJson(new PostedUser { username = username, pass = pass });
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("auth/login", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new APIException(Deserialize(response));
        }

        return response.Headers.GetValues("Set-Cookie");
    }

    internal async Task Authenticate(IEnumerable<string> cookies)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "auth");
        request.Headers.Add("Cookie", string.Join(";", cookies));

        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new APIException(Deserialize(response));
        }
    }

    internal async Task<List<ReadUser>> Users(IEnumerable<string> cookies)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "users");
        request.Headers.Add("Cookie", string.Join(";", cookies));

        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new APIException(Deserialize(response));
        }

        string json = await response.Content.ReadAsStringAsync();
        return Json.Deserialize<List<ReadUser>>(json);
    }

    internal async Task Logout(IEnumerable<string> cookies)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "auth/logout");
        request.Headers.Add("Cookie", string.Join(";", cookies));

        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new APIException(Deserialize(response));
        }
    }

    private string Deserialize(HttpResponseMessage response)
    {
        try
        {
            return Json.Deserialize<ErrorMessage>(response.Content.ReadAsStringAsync().Result).message;
        }
        catch (Exception ex)
        {
            Debug.Log(response.Content.ReadAsStringAsync().Result);
            throw ex;
        }
    }
}