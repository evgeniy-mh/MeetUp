using MeetUp.DB;
using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MeetUp.ConcilsControll
{
    class ConcilsControllVM : INotifyPropertyChanged
    {
        public Concil SelectedConcil { get; set; }
        private EFGenericRepository<Concil> ConcilRepository;
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
            ConcilRepository = new EFGenericRepository<Concil>(new MeetUpContext());
            Concils = new ObservableCollection<Concil>(ConcilRepository.Get());
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    /*EmployeeWindowView window = new EmployeeWindowView();
                    if (window.ShowDialog() == true)
                    {
                        EmployeeRepository.Create(window.Employee);
                        Employees = new ObservableCollection<Employee>(EmployeeRepository.Get());
                    }*/
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
                    /*EmployeeWindowView window = new EmployeeWindowView(SelectedEmployee);
                    if (window.ShowDialog() == true)
                    {
                        EmployeeRepository.Update(window.Employee);
                        Employees = new ObservableCollection<Employee>(EmployeeRepository.Get());
                    }*/
                    MessageBox.Show("ChangeCommand");
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
                    /*MessageBoxButton button = MessageBoxButton.OKCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result = MessageBox.Show(
                        String.Format("Вы действительно хотите удалить сотрудника {0} {1} {2} ?",
                        SelectedEmployee.MiddleName,
                        SelectedEmployee.FirstName,
                        SelectedEmployee.LastName),
                        "Удалить сотрудника?", button, icon);

                    if (result == MessageBoxResult.OK)
                    {
                        EmployeeRepository.Remove(SelectedEmployee);
                        Employees = new ObservableCollection<Employee>(EmployeeRepository.Get());
                    }*/
                    MessageBox.Show("RemoveCommand");
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
                    MessageBox.Show("sadasdas");
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
