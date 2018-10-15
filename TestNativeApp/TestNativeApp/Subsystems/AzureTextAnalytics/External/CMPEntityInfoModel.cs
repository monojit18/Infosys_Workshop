using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Subsystems.AzureTextAnalytics.External
{

    public class CMPEntityInfoModel
    {

        [JsonProperty("id")]
        public string IdString { get; set; }

        [JsonProperty("entities")]
        public List<CMPEntityModel> Entities { get; set; }

    }

    public class CMPEntityModel
    {

        [JsonProperty("name")]
        public string NameString { get; set; }

        [JsonProperty("wikipediaLanguage")]
        public string WikipediaLanguageString { get; set; }

        [JsonProperty("wikipediaId")]
        public string WikipediaIdString { get; set; }

        [JsonProperty("wikipediaUrl")]
        public string WikipediaUrlString { get; set; }

        [JsonProperty("bingId")]
        public string BingIdString { get; set; }

        [JsonProperty("matches")]
        public List<CMPMatchModel> Matches { get; set; }

    }

    public class CMPMatchModel
    {

        [JsonProperty("text")]
        public string TextString { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

    }
}
