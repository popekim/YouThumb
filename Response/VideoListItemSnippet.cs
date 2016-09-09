using System;
using System.Collections.Generic;

namespace YouThumb.Response
{
    public class VideoListItemSnippet
    {
        public DateTimeOffset publishedAt;
        public string channelId;
        public string title;
        public string description;

        public Dictionary<string, Thumbnail> thumbnails;

        public string channelTitle;
        public List<string> tags;
        public string categoryId;
        public string liveBroadcastContent;
        public string defaultLanguage;

        public LocalizedTitleDescription localized;
        public string defaultAudioLanguage;
    }
}
