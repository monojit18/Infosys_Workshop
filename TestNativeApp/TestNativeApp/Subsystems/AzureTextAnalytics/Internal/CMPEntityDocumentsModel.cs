using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Subsystems.AzureTextAnalytics.External;

namespace Subsystems.AzureTextAnalytics.Internal
{
    public class CMPEntityDocumentsModel
    {

        [JsonProperty("documents")]
        public List<CMPEntityInfoModel> Documents { get; set; }

    }
}
