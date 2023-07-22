using LiteDB;
using RestSharp;
using RestSharp.Extensions;
using QFamilyForum.Data;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol;
using System.Security.Policy;

namespace QFamilyForum.Services
{
    public class BaiduService
    {
        private ILiteCollection<APIInfo> collection;
        private string AppID = "36475265";
        private string APIKey = "McNEFeSe4cqQGYAidOhhOSsB";
        private string Secret = "jiGZ2t3vOEiPWgmAjvFqfwEal7FrEjyW";
        public BaiduService()
        {
            collection = Constant.APIInfoCollection;
        }
        public string GetValidAPIToken()
        {
            var result = collection.FindAll().FirstOrDefault();
            if((result is not null) || (result.InsertedAt + 3600 * 30 < DateTime.Now.Ticks))
                return result.AccessToken;
            return UpdateTokenAndReturn();
        }
        private string UpdateTokenAndReturn()
        {
            var client = new RestClient($"https://aip.baidubce.com/oauth/2.0/token?client_id={APIKey}&client_secret={Secret}&grant_type=client_credentials");
            
            var request = new RestRequest();
            request.Timeout = -1;
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            var result = client.Get<Token>(request);

            collection.Insert(new APIInfo {
                APPId = AppID,
                AccessToken = result.AccessToken,
                InsertedAt = DateTime.UtcNow.Ticks
            });

            return result.AccessToken;
        }
        // 
    }
}