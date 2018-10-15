using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Subsystems.HttpConnection.External;
using Subsystems.AzureTextAnalytics.External;
using Subsystems.AzureTextAnalytics.Internal;

namespace Subsystems.AzureTextAnalytics.Internal
{
    
    public class CMPTextAnalytics
    {

        private const string KBaseURLString = "https://{0}.api.cognitive.microsoft.com/text/analytics/v2.0/{1}";
        private const string KSubscriptionKeyString = "Ocp-Apim-Subscription-Key";
        private const string KSentimentKeyString = "sentiment";
        private const string KEntitiesKeyString = "entities";

        private CMPHttpConnectionProxy _httpConnectionProxy;
        private string _apiRegionString;
        private string _apiKeyString;

        private string PrepareURL(string actionString)
        {

            return (string.Format(KBaseURLString, _apiRegionString, actionString));

        }

        private async Task<string> PerformAnalyzeAsync(string analyzeString, string actionString)
        {
            
            if ((string.IsNullOrEmpty(analyzeString) == true) || (string.IsNullOrEmpty(actionString) == true))
                return null;
            
            var bodyBytesArray = Encoding.UTF8.GetBytes(analyzeString);
            if (bodyBytesArray == null)
                return null;

            var urlString = PrepareURL(actionString);
            if (string.IsNullOrEmpty(urlString) == true)
                return null;
            
            _httpConnectionProxy.URL(urlString)
                                .ByteArrayBody(bodyBytesArray)
                                .Build();
            
            var analyzeResponse = await _httpConnectionProxy.PostAsync();
            return (analyzeResponse?.ResponseString);

        }

        public CMPTextAnalytics(string apiRegionString, string apiKeyString)
        {

            _apiRegionString = string.Copy(apiRegionString);
            _apiKeyString = string.Copy(apiKeyString);
            _httpConnectionProxy = new CMPHttpConnectionProxy();
            _httpConnectionProxy.Headers(KSubscriptionKeyString, _apiKeyString);

        }

        public async Task<List<CMPSentimentModel>> AnalyzeSentimentAsync(string analyzeString)
        {

            if (string.IsNullOrEmpty(analyzeString) == true)
                return null;
            
            var analyzeResponseString = await PerformAnalyzeAsync(analyzeString, KSentimentKeyString);
            if (string.IsNullOrEmpty(analyzeResponseString) == true)
                return null;
            
            var sentimentDocumentsModel = JsonConvert.DeserializeObject<CMPSentimentDocumentsModel>(analyzeResponseString);
            return sentimentDocumentsModel.Documents;

        }

        public async Task<List<CMPEntityInfoModel>> AnalyzeEntitiesAsync(string analyzeString)
        {

            if (string.IsNullOrEmpty(analyzeString) == true)
                return null;

            var analyzeResponseString = await PerformAnalyzeAsync(analyzeString, KEntitiesKeyString);
            if (string.IsNullOrEmpty(analyzeResponseString) == true)
                return null;

            var entitiesDocumentsModel = JsonConvert.DeserializeObject<CMPEntityDocumentsModel>(analyzeResponseString);
            return entitiesDocumentsModel.Documents;

        }

    }
}
