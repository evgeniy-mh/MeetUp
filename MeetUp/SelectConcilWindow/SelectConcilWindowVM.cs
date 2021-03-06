﻿using MeetUp.DB;
using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeetUp.SelectConcilWindow
{
    class SelectConcilWindowVM : INotifyPropertyChanged
    {
        private SelectConcilWindowView selectConcilWindowView;
        private UnitOfWork unitOfWork;

        public ObservableCollection<Concil> Concils { get; set; }
        public Concil SelectedConcil { get; set; }

        public SelectConcilWindowVM(SelectConcilWindowView selectConcilWindowView)
        {
            this.selectConcilWindowView = selectConcilWindowView;
            unitOfWork = new UnitOfWork();
            Concils = new ObservableCollection<Concil>(unitOfWork.ConcilRepository.GetAll());
        }

        private RelayCommand accept_Click;
        public RelayCommand Accept_Click
        {
            get
            {
                return accept_Click ?? (accept_Click = new RelayCommand(obj =>
                {
                    selectConcilWindowView.DialogResult = true;
                }, (e) => { return this.SelectedConcil != null; }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            unitOfWork.Dispose();
        }
    }
}
