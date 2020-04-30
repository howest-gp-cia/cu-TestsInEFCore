namespace EFBlog.Domain.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public string ShortenUrl(int charactercount)
        {
            if (charactercount < Url.Length)
                return Url.Substring(0, charactercount);
            else
                return Url;
        }

        public string ShortenUrl()
        {
            return ShortenUrl(10);
        }

        public string RemoveUrlProtocol()
        {
            if(Url.StartsWith("http://"))
                return Url.Substring(7);
            else if (Url.StartsWith("https://"))
                return Url.Substring(8);
            return Url;
        }
    }
}
