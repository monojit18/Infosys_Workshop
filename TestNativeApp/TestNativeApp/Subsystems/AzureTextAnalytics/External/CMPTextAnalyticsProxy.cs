using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Subsystems.AzureTextAnalytics.Internal;

namespace Subsystems.AzureTextAnalytics.External
{
    public class CMPTextAnalyticsProxy
    {

        private CMPTextAnalytics _textAnalytics;

        public CMPTextAnalyticsProxy(string apiRegionString, string apiKeyString)
        {

            _textAnalytics = new CMPTextAnalytics(apiRegionString, apiKeyString);

        }

        public async Task<List<CMPSentimentModel>> AnalyzeSentimentAsync(string analyzeString)
        {

            var sentimentModelsList = await _textAnalytics?.AnalyzeSentimentAsync(analyzeString);
            return sentimentModelsList;

        }

        public async Task<List<CMPEntityInfoModel>> AnalyzeEntitiesAsync(string analyzeString)
        {

            var entityModelsList = await _textAnalytics?.AnalyzeEntitiesAsync(analyzeString);
            return entityModelsList;

        }

    }
}
