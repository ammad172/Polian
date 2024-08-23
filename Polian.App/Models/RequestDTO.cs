using Polian.App.Utility;
using System.Net.Mime;
using static Polian.App.Utility.Utils;

namespace Polian.App.Models
{
    public class RequestDTO
    {
        public AppiType ApiType { get; set; } = AppiType.Get;

        public string Url { get; set; }
        public object? Data { get; set; }
        public string? AccessToken { get; set; }
        public Contenttype? ContentType { get; set; } = Contenttype.Json;
    }

}