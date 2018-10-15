using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using Contacts;

namespace TestThreadsNative
{
    public class TestThreads
    {

        private  CNContactStore _contactStore;
        private SemaphoreSlim _blockingSemaphore;
        private SemaphoreSlim _poolSemaphore;
        private List<string> _urlsList;

        public async Task<bool> RequestAccessAsync()
        {

            NSError accessError = null;

            await Task.Run(() =>
            {

                _contactStore.RequestAccess(CNEntityType.Contacts, (bool granted, NSError error) =>
                {

                    if (error != null)
                    {

                        accessError = error;
                        _blockingSemaphore.Release();
                        return;

                    }

                    if (granted == false)
                    {
                    
                        _blockingSemaphore.Release();
                        return;

                    }

                    _blockingSemaphore.Release();

                });
            });

            await _blockingSemaphore.WaitAsync();
            return true;

        }

        public async Task CallPool1Async()
        {

            var baseUrl = "https://via.placeholder.com/";

            for (int i = 10; i < 500;i+=50)
            {

                var url = $"{baseUrl}{i}x150";
                _urlsList.Add(url);

            }

            var imageList = new List<string>();
            var cl = new HttpClient();

            await Task.Run(async () =>
            {

                foreach (var urlString in _urlsList)
                {


                    await _poolSemaphore.WaitAsync();
                    var res = await cl.GetAsync(urlString);                   
                    _poolSemaphore.Release();
                    imageList.Add(urlString);

                }

                _blockingSemaphore.Release();


            });

            await _blockingSemaphore.WaitAsync();
            Console.WriteLine(imageList.Count);

        }

        public async Task CallPool2Async()
        {

            var baseUrl = "https://via.placeholder.com/";

            for (int i = 10; i < 500; i += 50)
            {

                var url = $"{baseUrl}{i}x150";
                _urlsList.Add(url);

            }

            var imageList = new List<string>();
            var cl = new HttpClient();
            var urlTasks = _urlsList.Select(async (string urlString) => 
            {
            
                await cl.GetAsync(urlString);
                _poolSemaphore.Release();
                imageList.Add(urlString);


            }).ToArray();

            await Task.WhenAll(urlTasks);
            Console.WriteLine(imageList.Count);

        }

        public async Task CallParallel()
        {

            var baseUrl = "https://via.placeholder.com/";

            for (int i = 0; i < 50000;++i)
            {

                var url = $"{baseUrl}{i}x150";
                _urlsList.Add(url);

            }

            await Task.Run(() =>
            {

                Parallel.For(0, 49999, (int obj) =>
                {

                    try
                    {

                        var urlString = _urlsList[obj];
                        var uri = new Uri(urlString);
                        var host = uri.AbsolutePath;
                        Console.WriteLine(host);

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);

                    }


                });

            });





        }


        public TestThreads()
        {

            _contactStore = new CNContactStore();
            _blockingSemaphore = new SemaphoreSlim(0);
            _poolSemaphore = new SemaphoreSlim(2);
            _urlsList = new List<string>();


        }
    }
}
