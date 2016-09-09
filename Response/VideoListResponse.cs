using System.Collections.Generic;

namespace YouThumb.Response
{
    public class VideoListResponse
    {
        public string kind;
        public string etag;
        public PageInfo pageInfo;

        public List<VideoListItem> items;
    }
}
