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
        public Employee SelectedEmployee { get; set; }

        public SelectEmployeeWindowVM(SelectEmployeeWindowView selectEmployeeWindowView)
        {
            this.selectEmployeeWindowView = selectEmployeeWindowView;
            EmployeeRepository = new EmployeeRepository();
            Employees = new ObservableCollection<Employee>(EmployeeRepository.GetAll());
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
                }, (e) => { return this.SelectedEmployee != null; }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
