using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace DailyGoals.Models
{
    public class GoalDetail : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int GoalEntryId { get; set; }

        public int GoalId { get; set; }

        public string GoalName { get; set; }

        public bool _completed;
        public bool Completed
        {
            get { return _completed; }
            set
            {
                if (_completed == value)
                    return;

                _completed = value;

                GoalColor = _completed ? Color.LightGreen : Color.LightSalmon;

                OnPropertyChanged(nameof(Completed));
            }
        }

        public string Details { get; set; }

        public DateTime Date { get; set; }

        public Color _goalColor;
        public Color GoalColor
        {
            get
            {
                _goalColor = Completed ? Color.LightGreen : Color.LightSalmon;

                return _goalColor;
            }
            set
            {
                if (_goalColor == value)
                    return;

                _goalColor = value;

                OnPropertyChanged(nameof(GoalColor));
            }

        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
