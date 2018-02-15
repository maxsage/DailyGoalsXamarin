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
	public partial class ManageGoals : ContentPage
	{
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Goal> _goals;

        public ManageGoals ()
		{
			InitializeComponent ();

            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            var goals = await _connection.Table<Goal>().ToListAsync();

            _goals = new ObservableCollection<Goal>(goals);

            goalsListView.ItemsSource = _goals;

            base.OnAppearing();
        }

        private void goalsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var goal = e.Item as Goal;
            //goalDetail.Completed = !goalDetail.Completed;
            //goalDetail.GoalColor = goalDetail.Completed ? Color.LightGreen : Color.LightSalmon;
            //DisplayAlert("Tapped", goalDetail.GoalName, "OK");
        }

        private void goalsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            goalsListView.SelectedItem = null;
            //var goalDetail = e.SelectedItem as GoalDetail;
            //DisplayAlert("Selected", goalDetail.GoalName, "OK");
        }
    }
}