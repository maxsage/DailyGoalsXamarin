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
        private List<GoalDetail> _todaysGoalDetails = new List<GoalDetail>();
        private ObservableCollection<GoalDetail> _goalDetails;
        bool hasBeenShown = false;

        public MainPage()
		{
			InitializeComponent();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            //await _connection.CreateTableAsync<Goal>();
            //await _connection.CreateTableAsync<GoalEntry>();
            //await _connection.CreateTableAsync<Category>();



            if (hasBeenShown == false)
            {
                // Get the current Goals
                var currentGoals = await _connection.Table<Goal>().Where(g => g.Current == true).ToListAsync();

                // Get todays GoalEntries
                // https://stackoverflow.com/questions/27260905/join-tables-in-sqlite-net-with-linq-on-xamarin-android-is-not-supported
                var todaysGoals = await _connection.QueryAsync<GoalDetail>(
                    "SELECT Goal.Id  AS GoalId, GoalEntry.Id AS GoalEntryId, Goal.Name AS GoalName, Completed, Details, Date FROM GoalEntry " +
                    "INNER JOIN Goal " +
                   "ON Goal.Id = GoalEntry.GoalId " +
                    "WHERE date(Date / 10000000 - 62135596800, 'unixepoch') = date('now')");

                foreach (Goal g in currentGoals)
                {
                    var goalDetail = todaysGoals.FirstOrDefault(tg => tg.GoalId == g.Id);
                    if (goalDetail != null)
                    {
                        // Get the existing GoalDetail record and add it to _todaysGoalDetails
                        _todaysGoalDetails.Add(goalDetail);
                    }
                    else
                    {
                        // We have a Goal that is Current but isn't in Todays GoalEntries. We should add it to the Database.
                        var goalEntry = new GoalEntry { GoalId = g.Id, Completed = false, Details = "", Date = DateTime.Now.Date };
                        await _connection.InsertAsync(goalEntry);

                        _todaysGoalDetails.Add(
                            new GoalDetail
                            {
                                GoalId = g.Id,
                                GoalName = g.Name,
                                Completed = false,
                                Details = "",
                                Date = DateTime.Now.Date,
                                GoalColor = Color.LightSalmon
                            });
                    }
                }

                _goalDetails = new ObservableCollection<GoalDetail>(_todaysGoalDetails);

                //var goalEntries = await _connection.Table<GoalEntry>().Where(g => g.Date == DateTime.Now.Date).ToListAsync();
                //_goalEntries = new ObservableCollection<GoalEntry>(goalEntries);

                goalEntriesListView.ItemsSource = _goalDetails;
                hasBeenShown = true;
            }

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            // Should probably save to database here.

            base.OnDisappearing();
        }

        //private void Completed_Toggled(object sender, ValueChangedEventArgs e)
        //{
        //    //DisplayAlert("Toggling", "Toggled", "Ok");
        //    var goal = sender;
        //}

        private async void goalEntriesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var goalDetail = e.Item as GoalDetail;
            goalDetail.Completed = !goalDetail.Completed;

            await _connection.QueryAsync<GoalEntry>("UPDATE GoalEntry SET Completed = " + (goalDetail.Completed ? 1 : 0) + " WHERE Id = " + goalDetail.GoalEntryId);

            //goalDetail.GoalColor = goalDetail.Completed ? Color.LightGreen : Color.LightSalmon;

            //DisplayAlert("Tapped", goalDetail.GoalName, "OK");
        }

        private void goalEntriesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            goalEntriesListView.SelectedItem = null;
            //var goalDetail = e.SelectedItem as GoalDetail;
            //DisplayAlert("Selected", goalDetail.GoalName, "OK");
        }


        public string CurrentDate
        {
            get
            {
                return DateTime.Now.ToShortDateString();
            }
        }

        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageGoals());
        }
    }
}
