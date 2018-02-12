using DailyGoals.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DailyGoals.Droid;

namespace DailyGoals
{
	public partial class MainPage : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<GoalEntry> _goalEntries;
		public MainPage()
		{
			InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
		}

        protected override async void OnAppearing()
        {
            await _connection.CreateTableAsync<Goal>();
            await _connection.CreateTableAsync<GoalEntry>();
            await _connection.CreateTableAsync<Category>();

            var goalEntries = await _connection.Table<GoalEntry>().Where(g => g.Date == DateTime.Now.Date).ToListAsync();

            _goalEntries = new ObservableCollection<GoalEntry>(goalEntries);
            goalEntriesListView.ItemsSource = _goalEntries;

            base.OnAppearing();
        }

        async void OnAdd(object sender, System.EventArgs e)
        {
            var goalEntry = new GoalEntry
            {
                GoalId = 2,
                Completed = true,
                Details = "Drink Water" + DateTime.Now.Date.Ticks,
                Date = DateTime.Now.Date
            };
            await _connection.InsertAsync(goalEntry);

            _goalEntries.Add(goalEntry);
        }

        async void OnUpdate(object sender, System.EventArgs e)
        {
            var goalEntry = _goalEntries[0];
            goalEntry.Details += " UPDATED";

            await _connection.UpdateAsync(goalEntry);
        }

        async void OnDelete(object sender, System.EventArgs e)
        {
            var goalEntry = _goalEntries[0];

            await _connection.DeleteAsync(goalEntry);

            _goalEntries.Remove(goalEntry);
        }
    }
}
