using System.Collections.Generic;

namespace YouThumb.Response
{
    public class ContentDetailsResponse
    {
        public string kind;
        public string etag;
        public PageInfo pageInfo;

        public List<ContentDetailsItem> items;
    }
}
