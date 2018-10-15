using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using TestPerformaceApp.Models;

namespace TestPerformaceApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {

        private HttpClient _httpClient;
        private SemaphoreSlim _poolSemaphore;

        private async Task UploadPostsAsync(Item uploadItem)
        {

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

            var uploadString = JsonConvert.SerializeObject(uploadItem);
            var content = new StringContent(uploadString, Encoding.UTF8, "application/json");

            var httpResponse = await _httpClient.PostAsync("posts", content);
            var responseString = await httpResponse.Content.ReadAsStringAsync();


            //var buffer = new byte[responseStream.Length];
            //int offset = 0;
            //int bytesRead = 0;

            //do
            //{

            //    bytesRead = await responseStream.ReadAsync(buffer, offset, buffer.Length);
            //    offset += bytesRead;

            //} while (bytesRead <= 0);

            _poolSemaphore.Release();
            Console.WriteLine(responseString);


        }

        private async Task PoolUploadPostsAsync()
        {

            var postsList = new List<Item>();
            for (int i = 0; i < 5000; ++i)
            {

                var uploadItem = new Item()
                {

                    ItemId = i.ToString(),
                    Text = $"Test{i}",
                    Description = $"Desc{i}"

                };

                postsList.Add(uploadItem);

            }

            await Task.Run(async () =>
            {

                var tasksArray = postsList.Select(async (Item uploadItem) =>
                {

                    await _poolSemaphore.WaitAsync();
                    await UploadPostsAsync(uploadItem);

                }).ToArray(); ;

                await Task.WhenAll(tasksArray);

            });


            Console.WriteLine(postsList.Count.ToString());

        }

        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));

            _poolSemaphore = new SemaphoreSlim(5);
            PoolUploadPostsAsync();

        }

        public ICommand OpenWebCommand { get; }
    }
}