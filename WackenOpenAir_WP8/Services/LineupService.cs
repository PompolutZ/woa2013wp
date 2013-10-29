using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WackenOpenAir.Utilities;

namespace WackenOpenAir.Services
{
    public class LineupService
    {
        private const string WackenLineupFileSkyDriveAddress = "https://dl.dropboxusercontent.com/u/30119691/wacken.xml";

        public async Task LoadLineup()
        {
            await TryReadLineupFileVersionFromSkyDrive();
        }

        private async Task<Option<int>> TryReadLineupFileVersionFromSkyDrive()
        {
            var httpClient = new HttpClient();
            using (var reader = new StreamReader(await httpClient.GetStreamAsync(WackenLineupFileSkyDriveAddress)))
            {
                var line = await reader.ReadToEndAsync();
                var closeTagIndex = line.IndexOf("</ver>", System.StringComparison.Ordinal);
                var version = 0;
                if (int.TryParse(line.Substring(5, closeTagIndex - 5), out version))
                {
                    return Option<int>.Some(version);
                }
            }

            return Option<int>.None();
        }
    }
}