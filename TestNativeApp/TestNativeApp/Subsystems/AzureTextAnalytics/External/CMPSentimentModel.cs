using System;
using Newtonsoft.Json;

namespace Subsystems.AzureTextAnalytics.External
{
    public class CMPSentimentModel
    {

        [JsonProperty("id")]
        public string IdString { get; set; }

        [JsonProperty("score")]
        public string ScoreString { get; set; }

    }



}
