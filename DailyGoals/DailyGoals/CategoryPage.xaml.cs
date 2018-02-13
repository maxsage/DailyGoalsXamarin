using DailyGoals.Droid;
using DailyGoals.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyGoals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryPage : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Category> _categories;

        public CategoryPage ()
		{
			InitializeComponent ();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
		}

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Category>();

            var categories = await _connection.Table<Category>().ToListAsync();

            _categories = new ObservableCollection<Category>(categories);
            categoriesListView.ItemsSource = _categories;
            
        }




    }
}