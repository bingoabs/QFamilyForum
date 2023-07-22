using System.Text.Json.Serialization;

namespace QFamilyForum.Data
{
    public class Token
    {
        [JsonPropertyName("access_token")]
        public string AccessToken {get;set;}

        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get;set;}

        [JsonPropertyName("session_key")]
        public string SessionKey { get; set; }

        [JsonPropertyName("session_secret")]
        public string SessionSecret { get;set;}
    }

    public class APIInfo
    {
        public String APPId { get;set;}
        public string AccessToken { get;set;}
        public long InsertedAt { get;set; }
    }
}
