using System;

namespace WackenDataAndUtilities.Lastfm
{
    public class Artist
    {
        public string Name { get; set; }

        public string LastFmName { get; set; }

        public string Id { get; set; }
        
        public string Url { get; set; }
        
        public string ImageUrl { get; set; }

        public string StageName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}