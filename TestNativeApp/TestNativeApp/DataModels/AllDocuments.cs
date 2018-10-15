using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TestNativeApp.DataModels
{

    public class AllDocuments
    {

        [JsonProperty("documents")]
        public List<Document> Documents { get; set; }

    }

    public class Document
    {

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
