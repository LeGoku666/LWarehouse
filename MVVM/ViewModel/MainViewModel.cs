using LWarehouse.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LWarehouse.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand SearchViewCommand { get; set; }
        public RelayCommand AddViewCommand { get; set; }
        public RelayCommand EditViewCommand { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }

        public SearchViewModel SearchVM { get; set; }
        public AddViewModel AddVM { get; set; }
        public EditViewModel EditVM { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public AboutViewModel AboutVM { get; set; }

        private object _currenView;

        public object CurrentView
        {
            get => _currenView;
            set
            {
                _currenView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            SearchVM = new SearchViewModel();
            AddVM = new AddViewModel();
            EditVM = new EditViewModel();
            HomeVM = new HomeViewModel();
            AboutVM = new AboutViewModel();

            CurrentView = SearchVM;

            SearchViewCommand = new RelayCommand(o =>
            {
                CurrentView = SearchVM;
            });

            AddViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddVM;
            });

            EditViewCommand = new RelayCommand(o =>
            {
                CurrentView = EditVM;
            });

            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = HomeVM;
            });

            AboutViewCommand = new RelayCommand(o =>
            {
                CurrentView = AboutVM;
            });
        }
    }
}
