using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DailyGoals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Tomorrow : ContentPage
	{
		public Tomorrow ()
		{
			InitializeComponent ();

            BindingContext = this;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public string CurrentDate
        {
            get
            {
                return DateTime.Now.AddDays(1).ToShortDateString();
            }
        }
    }
}