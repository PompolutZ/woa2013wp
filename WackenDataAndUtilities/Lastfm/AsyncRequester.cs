using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace WackenDataAndUtilities.Lastfm
{
    public class AsyncRequester
    {
        public static void GetArtist(string url, Action<Artist> callback)
        {
            MakeAsyncRequest(url)
                .ContinueWith(t => JsonObject.Parse(t.Result).Object("artist").ConvertTo(
                    x => new Artist
                    {
                        LastFmName = x.Get("name"),
                        Id = x.Get("mbid"),
                        Url = x.Get("url"),
                        ImageUrl = x.ArrayObjects("image").First(img => img.Get("size") == "large").Get("#text")
                    }))
               .ContinueWith(t => callback(t.Result), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private static Task<string> MakeAsyncRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "Get";

            Task<WebResponse> task = Task.Factory.FromAsync(
                request.BeginGetResponse, asyncResult => request.EndGetResponse(asyncResult), null);

            return task.ContinueWith(t => ReadStreamFromResponse(t.Result));
        }

        private static string ReadStreamFromResponse(WebResponse webResponse)
        {
            using (var responseStream = webResponse.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                var strContent = reader.ReadToEnd();
                return strContent;
            }
        }
    }
}