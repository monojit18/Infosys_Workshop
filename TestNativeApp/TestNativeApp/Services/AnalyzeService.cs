using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Subsystems.AzureTextAnalytics.External;
using TestNativeApp.DataModels;

namespace TestNativeApp.Services
{
    public class AnalyzeService
    {

        private const string KAnalyticsKeyString = "<Text Analytics_Key>";
        private const string KAnalyticsRegionString = "eastus";

        public async Task<List<Tuple<string, double>>> AnalyzeTextAsync(List<string> wordTokensList)
        {

            var analysisInfoList = new List<Tuple<string, double>>();

            await Task.Run(() =>
            {

                var analyzeTaskArray = wordTokensList.Select(async (string wordTokenString) =>
                {

                    var document = new Document()
                    {

                        Language = "en",
                        Id = "1",
                        Text = wordTokenString

                    };

                    var allDocs = new AllDocuments()
                    {

                        Documents = new List<Document>() { document }

                    };

                    var bodyString = JsonConvert.SerializeObject(allDocs);
                    var textAnalyticsProxy = new CMPTextAnalyticsProxy(KAnalyticsRegionString, KAnalyticsKeyString);
                    var sentimentModelsList = await textAnalyticsProxy.AnalyzeSentimentAsync(bodyString);
                    if ((sentimentModelsList == null) || (sentimentModelsList.Count == 0))
                        return;

                    var sentimentModel = sentimentModelsList[0];
                    var score = Double.Parse(sentimentModel.ScoreString);
                    analysisInfoList.Add(new Tuple<string, double>(wordTokenString, score));

                }).ToArray();

                Task.WaitAll(analyzeTaskArray);

            });

            return analysisInfoList;

        }
    }
}
