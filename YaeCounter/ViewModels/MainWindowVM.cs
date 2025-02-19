using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;


namespace YaeCounter.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private int _current = 0;
        public int current
        {
            get { return _current; }
            set 
            {
                _current = value;
                copytext = $"【やえデスカウンター】本日:{current}回 通算:{total}回";
                OnPropertyChanged();
            }
        }

        private int _total = 0;
        public int total 
        {
            get { return _total; }
            set
            {
                _total = value;
                copytext = $"【やえデスカウンター】本日:{current}回 通算:{total}回";
                OnPropertyChanged();
            }
        }

        private string _copytext = "";
        public string copytext
        {
            get 
            {
                return _copytext; 
            }
            set
            {
                _copytext = value;
                OnPropertyChanged();
            }
        }
        public MainWindowVM()
        {

        }
    }
}
