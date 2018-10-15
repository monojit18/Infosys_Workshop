using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Subsystems.AzureTextAnalytics.External;
using TestNativeApp.Services;

namespace TestNativeApp.ViewModels
{
    public class AnalyzeViewModel
    {
        public AnalyzeViewModel()
        {
        }

        public async Task<List<Tuple<string, double>>> AnalyzeTextAsync(string analyzeTextString)
        {

            List<string> wordTokensList = null;
            var wordTokensArray = analyzeTextString.Split(new string[] { " " }, StringSplitOptions.None);
            if (wordTokensArray == null)
                wordTokensList = new List<string>() { analyzeTextString };
            else
                wordTokensList = wordTokensArray.ToList();

            var analyzeService = new AnalyzeService();
            var analyzedTextsList = await analyzeService.AnalyzeTextAsync(wordTokensList);
            return analyzedTextsList;

        }
    }
}
