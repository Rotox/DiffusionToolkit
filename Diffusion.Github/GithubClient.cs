using System.Net;
using System.Text.Json;

namespace Diffusion.Github;

public class GithubClient : IDisposable
{
    private readonly HttpClient _client;
    private readonly string _user;
    private readonly string _repo;
    private const string _userAgent = "GithubClient/1.0";

    public GithubClient(string user, string repo)
    {
        var handler = new HttpClientHandler();
        handler.AllowAutoRedirect = false;
        _client = new HttpClient(handler);

        _client.DefaultRequestHeaders.Add("Accept", "*/*");
        _client.DefaultRequestHeaders.Add("User-Agent", _userAgent);

        _user = user;
        _repo = repo;
    }

    public Task<IEnumerable<Release>?> GetReleases()
    {
        return GetReleases(CancellationToken.None);
    }

    public Task<IEnumerable<Tag>?> GetTags()
    {
        return GetTags(CancellationToken.None);
    }

    public Task<Stream> DownloadAsync(string url)
    {
        return DownloadAsync(url, CancellationToken.None);
    }

    public async Task<IEnumerable<Release>?> GetReleases(CancellationToken token)
    {
        var json = await GetStringWithRedirectAsync(new Uri($"https://api.github.com/repos/{_user}/{_repo}/releases"), token);
        return JsonSerializer.Deserialize<IEnumerable<Release>>(json);
    }

    public async Task<IEnumerable<Tag>?> GetTags(CancellationToken token)
    {
        var json = await GetStringWithRedirectAsync(new Uri($"https://api.github.com/repos/{_user}/{_repo}/tags"), token);
        return JsonSerializer.Deserialize<IEnumerable<Tag>>(json);
    }

    private async Task<string> GetStringWithRedirectAsync(Uri uri, CancellationToken token, int redirectCount = 0)
    {
        var response = await _client.GetAsync(uri, token);

        try
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Moved:
                case HttpStatusCode.Found:
                case HttpStatusCode.RedirectKeepVerb:
                case HttpStatusCode.PermanentRedirect:
                {
                    if (redirectCount >= 3)
                    {
                        throw new Exception($"GitHub request to {uri} exceeded the maximum of 3 redirects.");
                    }

                    var location = response.Headers.Location;

                    if (location == null)
                    {
                        throw new Exception($"GitHub request to {uri} returned status {(int)response.StatusCode} ({response.StatusCode}) with no Location header.");
                    }

                    var target = new Uri(uri, location);

                    if (target.Scheme != Uri.UriSchemeHttps)
                    {
                        throw new Exception($"GitHub request to {uri} redirected to a non-HTTPS URL ({target}), which was rejected.");
                    }

                    return await GetStringWithRedirectAsync(target, token, redirectCount + 1);
                }
                case HttpStatusCode.OK:
                    return await response.Content.ReadAsStringAsync(token);
                default:
                    throw new Exception($"GitHub request to {uri} failed with status {(int)response.StatusCode} ({response.StatusCode}).");
            }
        }
        finally
        {
            response.Dispose();
        }
    }

    public async Task<Stream> DownloadAsync(string url, CancellationToken token)
    {
        _client.DefaultRequestHeaders.Clear();
        _client.DefaultRequestHeaders.Add("Accept", "application/octet-stream");
        _client.DefaultRequestHeaders.Add("User-Agent", _userAgent);

        var response = await _client.GetAsync(new Uri(url, UriKind.Absolute));
            
        switch (response.StatusCode)
        {
            case HttpStatusCode.Moved:
            {
                var redirect = response.Headers.GetValues("Location").First();
                return await DownloadAsync(redirect, token);
            }
            case HttpStatusCode.Found:
            {
                var redirect = response.Headers.GetValues("Location").First();
                return await DownloadAsync(redirect, token);
            }
            case HttpStatusCode.OK:
                return await response.Content.ReadAsStreamAsync(token);
            default:
                throw new Exception("");
        }
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}