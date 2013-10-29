using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using WackenDataAndUtilities.Utilities;
using WackenOpenAir.Core;
using WackenOpenAir.Utilities;

namespace WackenOpenAir.Services
{
    public class NewsService
    {
        private const string NewsFeedUrl = "http://www.wacken.com/{0}/wacken2012/main-news/rss/rss.xml";
        private const string GermanCultureName = "de-DE";
        private const string GermanUrl = "de";
        private const string EnglishUrl = "en";

        public async Task<Option<IEnumerable<NewsFeedItem>>> LoadNewsAsync()
        {
            try
            {
                var newsFeedUrl = ConstructCultureSpeficicNewsFeedUri();
                var client = new HttpClient();
                var newsFeed = await client.GetStringAsync(newsFeedUrl).ConfigureAwait(false);
                return Option<IEnumerable<NewsFeedItem>>.Some(ParseNewsFeed(newsFeed));
            }
            catch (Exception)
            {
                return Option<IEnumerable<NewsFeedItem>>.None();
            }
        }

        private string ConstructCultureSpeficicNewsFeedUri()
        {
            var cultureInfo = Thread.CurrentThread.CurrentUICulture;
            return 
                cultureInfo.Name == GermanCultureName 
                ? string.Format(NewsFeedUrl, GermanUrl) 
                : string.Format(NewsFeedUrl, EnglishUrl);
        }

        private IEnumerable<NewsFeedItem> ParseNewsFeed(string newsFeed)
        {
            XmlReader xmlReader = XmlReader.Create(new StringReader(newsFeed));
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

            return feed.Items.Select(sItem => new NewsFeedItem
                {
                    Title = sItem.IfNotNull(item => item.Title.Text), 
                    PublishDate = sItem.IfNotNull(item => item.PublishDate.Date), 
                    Details = sItem.IfNotNull(item => item.Summary.Text), 
                    Link = sItem.IfNotNull(item => item.Links[0].Uri.ToString())
                });
        }
    }
}