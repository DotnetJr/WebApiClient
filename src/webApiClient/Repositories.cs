using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace WebApiClient
{
    [DataContractAttribute(Name = "repo")]
    public class Repositories
    {
        [DataMemberAttribute(Name = "name")]
        public string Name { get; set; }

        [DataMemberAttribute(Name = "description")]
        public string Description { get; set; }

        [DataMemberAttribute(Name = "html_url")]
        public Uri GitHubHomeUrl { get; set; }

        [DataMemberAttribute(Name = "homepage")]
        public Uri Homepage { get; set; }

        [DataMemberAttribute(Name = "watchers")]
        public int Watchers { get; set; }

        [DataMemberAttribute(Name = "pushed_at")]
        private string JsonDate { get; set; }

        [IgnoreDataMemberAttribute]
        public DateTime LastPush
        {
            get
            {
                return DateTime.ParseExact(JsonDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            }
        }
    }
}
