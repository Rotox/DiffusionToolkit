using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diffusion.Github;

namespace Diffusion.Common;

public class UpdateChecker
{
    public GithubClient Client { get; }
    private CancellationTokenSource _cts;

    public void Cancel()
    {
        _cts.Cancel();
    }

    public CancellationToken CancellationToken => _cts.Token;

    public Release LatestRelease { get; private set; }

    public UpdateChecker()
    {
        _cts = new CancellationTokenSource();
        Client = new GithubClient("Rotox", "DiffusionToolkit");
    }

    private async Task<Release> GetLatestRelease()
    {
        var releases = await Client.GetReleases(_cts.Token);

        return releases.OrderByDescending(r => r.published_at).FirstOrDefault();
    }

    public async Task<bool> CheckForUpdate(string? path = null)
    {
        LatestRelease = await GetLatestRelease();

        if (LatestRelease == null) return false;

        var localVersion = SemanticVersionHelper.GetLocalVersion(path);

        SemanticVersion.TryParse(LatestRelease.tag_name, out var releaseVersion);

        return releaseVersion > localVersion;
    }

}
