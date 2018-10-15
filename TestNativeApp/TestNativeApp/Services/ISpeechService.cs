using System;
using System.Threading.Tasks;

namespace TestNativeApp.Services
{
    public interface ISpeechService
    {

        Task<string> RecordSpeech();

    }
}
