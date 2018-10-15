using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Subsystems.AzureTextAnalytics.External;

namespace Subsystems.AzureTextAnalytics.Internal
{
    public class CMPSentimentDocumentsModel
    {

        [JsonProperty("documents")]
        public List<CMPSentimentModel> Documents { get; set; }

    }
}
