using MeetUp.DB;
using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using MeetUp.EmployeesControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MeetUp.ConcilWindow
{
    class ConcilWindowVM : INotifyPropertyChanged
    {
        private ConcilWindowView concilWindowView;
        private ConcilRepository ConcilRepository;

        public Concil Concil { get; set; }

        public ConcilWindowVM(ConcilWindowView concilWindowView)
        {
            ConcilRepository = new ConcilRepository();
            this.concilWindowView = concilWindowView;
            this.Concil = new Concil();
        }

        public ConcilWindowVM(ConcilWindowView concilWindowView, Concil concil)
        {
            ConcilRepository = new ConcilRepository();
            this.concilWindowView = concilWindowView;
            this.Concil = new Concil(concil);

            this.Concil.Employees = ConcilRepository.GetEmployees(this.Concil);
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
