using MeetUp.ConcilWindow;
using MeetUp.DB;
using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MeetUp.ConcilsControll
{
    class ConcilsControllVM : INotifyPropertyChanged
    {
        public Concil SelectedConcil { get; set; }
        private ConcilRepository ConcilRepository;
        private ObservableCollection<Concil> _concils;
        public ObservableCollection<Concil> Concils
        {
            get
            {
                return _concils;
            }
            set
            {
                _concils = value;
                OnPropertyChanged("Concils");
            }
        }

        public ConcilsControllVM()
        {
            ConcilRepository = new ConcilRepository(new MeetUpContext());
            Concils = new ObservableCollection<Concil>(ConcilRepository.GetAll());
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    ConcilWindowView window = new ConcilWindowView();
                    if (window.ShowDialog() == true)
                    {
                        ConcilRepository.Create(window.Concil);
                        Concils = new ObservableCollection<Concil>(ConcilRepository.GetAll());
                    }
                }));
            }
        }

        private RelayCommand changeCommand;
        public RelayCommand ChangeCommand
        {
            get
            {
                return changeCommand ?? (changeCommand = new RelayCommand(obj =>
                {
                    ConcilWindowView window = new ConcilWindowView(SelectedConcil);
                    if (window.ShowDialog() == true)
                    {
                        ConcilRepository.Update(window.Concil);
                        Concils = new ObservableCollection<Concil>(ConcilRepository.GetAll());
                    }

                }, (obj) => { return SelectedConcil != null; }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ?? (removeCommand = new RelayCommand(obj =>
                {
                    MessageBoxButton button = MessageBoxButton.OKCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result = MessageBox.Show(
                        String.Format("Вы действительно хотите удалить совет {0} ?",
                        SelectedConcil.Name),
                        "Удалить совет?", button, icon);

                    if (result == MessageBoxResult.OK)
                    {
                        ConcilRepository.Remove(SelectedConcil);
                        Concils = new ObservableCollection<Concil>(ConcilRepository.GetAll());
                    }
                }, (obj) => { return SelectedConcil != null; }));
            }
        }

        private RelayCommand moreInfoCommand;
        public RelayCommand MoreInfoCommand
        {
            get
            {
                return moreInfoCommand ?? (moreInfoCommand = new RelayCommand(obj =>
                {
                    /*EmployeeWindowView window = new EmployeeWindowView();
                    if (window.ShowDialog() == true)
                    {
                        EmployeeRepository.Create(window.Employee);
                        Employees = new ObservableCollection<Employee>(EmployeeRepository.Get());
                    }*/
                    MessageBox.Show("asdasd");
                }, (obj) => { return SelectedConcil != null; }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
