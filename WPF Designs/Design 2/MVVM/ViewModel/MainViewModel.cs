using Design_2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_2.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand MicroserviceViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public MicroserviceViewModel MicroserviceVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            MicroserviceVM = new MicroserviceViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            MicroserviceViewCommand = new RelayCommand(o =>
            {
                CurrentView = MicroserviceVM;
            });

        }
    }
}
