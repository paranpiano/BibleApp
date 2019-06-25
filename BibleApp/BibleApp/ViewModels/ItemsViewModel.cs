using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using BibleApp.Models;
using BibleApp.Views;

namespace BibleApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }

        /// <summary>
        /// called when refreshed!
        /// </summary>
        public Command LoadItemsCommand { get; set; }

        /// <summary>
        /// DB Command
        /// </summary>
        public Command CreateBibleDataCommand { get; set; }
        public Command DeleteBibleDataCommand { get; set; }

        /// <summary>
        /// Contructor
        /// </summary>
        public ItemsViewModel()
        {
            RestWebService.SetDataContext(datacontext);

            Title = "BIBLE";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            CreateBibleDataCommand = new Command(async () => await ExecuteCreateBibleDataCommand());
            DeleteBibleDataCommand = new Command(async () => await ExecuteDeleteBibleDataCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

        }

        /// <summary>
        /// Command execute
        /// </summary>
        /// <returns></returns>
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
        async Task ExecuteCreateBibleDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (datacontext.GetDataRawCount() != 0)
                    return;

                foreach (var item in Items)
                {
                    await RestWebService.CreateBibleVerseData(item.Id);
                }

                datacontext.SaveAllBibleVerses();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", "FAILED TO DOWNLOAD BIBLE DATA!" + ex.Message, "CLOSE");
            }
            finally
            {
                IsBusy = false;
            }
        }
        async Task ExecuteDeleteBibleDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (datacontext.GetDataRawCount() != 0)
                    datacontext.DeleteAllVerses();
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