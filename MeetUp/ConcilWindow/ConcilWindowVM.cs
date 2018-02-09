using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
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

        public Concil Concil { get; set; }

        public ConcilWindowVM(ConcilWindowView concilWindowView)
        {
            this.concilWindowView = concilWindowView;
            this.Concil = new Concil();
        }

        public ConcilWindowVM(ConcilWindowView concilWindowView, Concil concil)
        {
            this.concilWindowView = concilWindowView;
            this.Concil = new Concil(concil);
        }

        private RelayCommand accept_Click;
        public RelayCommand Accept_Click
        {
            get
            {
                return accept_Click ?? (accept_Click = new RelayCommand(obj =>
                {
                    concilWindowView.DialogResult = true;
                }, (obj) => { return /*IsAllFieldsValid(concilWindowView.EmployeeInfoPanel)*/ true; }));
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
