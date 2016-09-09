using System.Collections.Generic;

namespace YouThumb.Response
{
    public class PlaylistItemsResponse
    {
        public string kind;
        public string etag;
        public string nextPageToken;

        public PageInfo pageInfo;
        public List<PlaylistItem> items;
    }
}
