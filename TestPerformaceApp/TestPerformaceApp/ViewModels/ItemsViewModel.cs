using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

using TestPerformaceApp.Models;
using TestPerformaceApp.Views;

namespace TestPerformaceApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
    
        private SemaphoreSlim _poolSemaphore;

        private async Task DownloadImageAsync(string segmentString)
        {

            using (var httpClient = new HttpClient())
            {

                httpClient.BaseAddress = new Uri("https://placeholder.com/");
                var httpResponse = await httpClient.GetAsync(segmentString);
                var imageBytes = await httpResponse.Content.ReadAsByteArrayAsync();
                _poolSemaphore.Release();
                Console.WriteLine($"{segmentString}: " + imageBytes.Length.ToString());

            }

        }

        private async Task LoadImagesAsync()
        {

            var imagesList = new List<Task>();
            for (int i = 10, j = 10; i < 5000; ++i)
            {

                await _poolSemaphore.WaitAsync();
                var task = DownloadImageAsync($"{i}x{j}");
                imagesList.Add(task);

            }

            await Task.WhenAll(imagesList);
            Console.WriteLine(imagesList.Count.ToString());

        }

        private async Task PoolImagesAsync()
        {

            var imagesList = new List<string>();
            for (int i = 50, j = 10; i < 5000; ++i)
                imagesList.Add($"{i}x{j}");

            await Task.Run(async () =>
            {

                var tasksArray = imagesList.Select(async (string val) =>
                {

                    await _poolSemaphore.WaitAsync();
                    await DownloadImageAsync(val);

                }).ToArray(); ;

                await Task.WhenAll(tasksArray);

            });


            Console.WriteLine(imagesList.Count.ToString());

        }

        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            _poolSemaphore = new SemaphoreSlim(5);

#if ENABLE_OPTIMIZATION

            PoolImagesAsync();
#else
            LoadImagesAsync();
#endif



        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}