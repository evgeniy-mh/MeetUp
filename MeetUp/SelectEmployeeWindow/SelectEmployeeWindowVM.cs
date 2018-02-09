using MeetUp.DB;
using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.SelectEmployeeWindow
{
    class SelectEmployeeWindowVM : INotifyPropertyChanged
    {
        private SelectEmployeeWindowView selectEmployeeWindowView;
        private EmployeeRepository EmployeeRepository;

        public ObservableCollection<Employee> Employees { get; set; }
        public Employee SelectedEmployee;

        public SelectEmployeeWindowVM(SelectEmployeeWindowView selectEmployeeWindowView)
        {
            this.selectEmployeeWindowView = selectEmployeeWindowView;
            this.EmployeeRepository = new EmployeeRepository(new MeetUpContext());
            this.Employees = new ObservableCollection<Employee>(EmployeeRepository.GetAll());
        }

        public SelectEmployeeWindowVM(SelectEmployeeWindowView selectEmployeeWindowView, IEnumerable<Employee> employees)
        {
            this.selectEmployeeWindowView = selectEmployeeWindowView;
            this.Employees = new ObservableCollection<Employee>(employees);
        }

        private RelayCommand accept_Click;
        public RelayCommand Accept_Click
        {
            get
            {
                return accept_Click ?? (accept_Click = new RelayCommand(obj =>
                {
                    selectEmployeeWindowView.DialogResult = true;
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
