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
        private EFGenericRepository<Concil> ConcilRepository;
        private EmployeeControl employeeControl;

        public Concil Concil { get; set; }

        public ConcilWindowVM(ConcilWindowView concilWindowView)
        {
            ConcilRepository = new EFGenericRepository<Concil>(new MeetUpContext());
            this.concilWindowView = concilWindowView;
            this.Concil = new Concil();

            employeeControl = new EmployeeControl(new ObservableCollection<Employee>(this.Concil.Employees));
            concilWindowView.ConcilInfoPanel.Children.Add(employeeControl);
        }

        public ConcilWindowVM(ConcilWindowView concilWindowView, Concil concil)
        {
            ConcilRepository = new EFGenericRepository<Concil>(new MeetUpContext());
            this.concilWindowView = concilWindowView;
            this.Concil = new Concil(concil);

            this.Concil.Employees = ConcilRepository.Get("Employees").FirstOrDefault((c) => { return c.Id == this.Concil.Id; }).Employees;
            employeeControl = new EmployeeControl(new ObservableCollection<Employee>(this.Concil.Employees));
            concilWindowView.ConcilInfoPanel.Children.Add(employeeControl);
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
