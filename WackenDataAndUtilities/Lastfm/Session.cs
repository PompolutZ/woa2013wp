using System;

namespace WackenDataAndUtilities.Lastfm
{
    public class Session
    {
        private const string LastfmApiKey = "afff1b8c2da298e99b65065d1c76bc5b";
        private const string LastfmRootUrl = @"http://ws.audioscrobbler.com/2.0/?";

        public static void LoadArtist(string artistName, Action<Artist> artist)
        {
            var artistSearchUrl = GetArtistUrl(artistName);
            AsyncRequester.GetArtist(artistSearchUrl, artist);
        }

        private static string GetArtistUrl(string artistName)
        {
            return string.Format("{0}method=artist.getinfo&artist={1}&api_key={2}&format=json", LastfmRootUrl, artistName, LastfmApiKey);
        }
    }
}