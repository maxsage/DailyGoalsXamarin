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
	public partial class Yesterday : ContentPage
	{
		public Yesterday ()
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
                return DateTime.Now.AddDays(-1).ToShortDateString();
            }
        }

        //Bindable Properties - what are these used for?
        //public static readonly BindableProperty CurrentDateProperty =
        //    BindableProperty.Create("CurrentDate", typeof(string), typeof(string));

        //public string CurrentDate
        //{

        //    get { return (string)GetValue(CurrentDateProperty); }
        //    set { SetValue(CurrentDateProperty, value); }
        //}

    }
}