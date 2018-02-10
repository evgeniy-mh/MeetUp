using MeetUp.DB;
using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using MeetUp.SelectEmployeeWindow;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace MeetUp.ConcilWindow
{
    class ConcilWindowVM : INotifyPropertyChanged
    {
        private ConcilWindowView concilWindowView;
        private EmployeeRepository EmployeeRepository;
        private ConcilRepository ConcilRepository;
        private bool IsCreatingNewConcil;

        public Concil Concil { get; set; }
        public ObservableCollection<Employee> ConcilEmployees
        {
            get
            {
                return new ObservableCollection<Employee>(Concil.Employees);
            }
            set
            {
                Concil.Employees = value;
                OnPropertyChanged("ConcilEmployees");
            }
        }

        private ConcilWindowVM()
        {
            EmployeeRepository = new EmployeeRepository(new MeetUpContext());
            ConcilRepository = new ConcilRepository(new MeetUpContext());
        }

        public ConcilWindowVM(ConcilWindowView concilWindowView) : this()
        {
            this.concilWindowView = concilWindowView;
            IsCreatingNewConcil = true;
            this.Concil = new Concil();
        }

        public ConcilWindowVM(ConcilWindowView concilWindowView, Concil concil) : this()
        {
            this.concilWindowView = concilWindowView;
            IsCreatingNewConcil = false;
            this.Concil = new Concil(concil);
            ConcilEmployees = new ObservableCollection<Employee>(EmployeeRepository.GetEmployeesForConcil(this.Concil).ToList());
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    var freeEmployees = EmployeeRepository.GetAllFreeEmployeesForConcil(this.Concil);
                    SelectEmployeeWindowView window = new SelectEmployeeWindowView(freeEmployees);
                    if (window.ShowDialog() == true)
                    {
                        if (IsCreatingNewConcil)
                        {
                            Concil.Employees.Add(window.SelectedEmployee);
                            OnPropertyChanged("ConcilEmployees");
                        }
                        else
                        {
                            ConcilRepository.AddEmployeeToConcil(this.Concil, window.SelectedEmployee);
                            ConcilEmployees = new ObservableCollection<Employee>(EmployeeRepository.GetEmployeesForConcil(this.Concil).ToList());
                        }
                    }
                }));
            }
        }

        private RelayCommand accept_Click;
        public RelayCommand Accept_Click
        {
            get
            {
                return accept_Click ?? (accept_Click = new RelayCommand(obj =>
                {
                    concilWindowView.DialogResult = true;
                }, (obj) => { return IsAllFieldsValid(concilWindowView.ConcilInfoPanel); }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private bool IsAllFieldsValid(DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                TextBox element = child as TextBox;
                if (element == null) continue;

                if (Validation.GetHasError(element))
                {
                    return false;
                }

                IsAllFieldsValid(element);
            }
            return true;
        }
    }
}
