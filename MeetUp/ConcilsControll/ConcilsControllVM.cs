using MeetUp.ConcilWindow;
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
        private ConcilControll concilControllUserControll;
        private UnitOfWork unitOfWork;
        public Concil SelectedConcil { get; set; }
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

        private string searchString;
        public string SearchString
        {
            get
            {
                return searchString;
            }
            set
            {
                searchString = value;
                SearchCommand.Execute(searchString);
            }
        }

        public ConcilsControllVM(ConcilControll concilControllUserControll)
        {
            this.concilControllUserControll = concilControllUserControll;
            unitOfWork = new UnitOfWork();
            Concils = new ObservableCollection<Concil>(unitOfWork.ConcilRepository.GetAll());
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
                        unitOfWork.ConcilRepository.Create(window.Concil);
                        Concils = new ObservableCollection<Concil>(unitOfWork.ConcilRepository.GetAll());
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
                        unitOfWork.ConcilRepository.Update(window.Concil);
                        Concils = new ObservableCollection<Concil>(unitOfWork.ConcilRepository.GetAll());
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
                        unitOfWork.ConcilRepository.Remove(SelectedConcil);
                        Concils = new ObservableCollection<Concil>(unitOfWork.ConcilRepository.GetAll());
                    }
                }, (obj) => { return SelectedConcil != null; }));
            }
        }

        private RelayCommand searchCommand;
        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ?? (searchCommand = new RelayCommand(query =>
                {
                    if (query.ToString().Length == 0)
                    {
                        Concils = new ObservableCollection<Concil>(unitOfWork.ConcilRepository.GetAll());
                    }
                    else
                    {
                        Concils = new ObservableCollection<Concil>(unitOfWork.ConcilRepository.SearchConcils(query.ToString()));
                    }
                }));
            }
        }

        private RelayCommand clearSearchStringCommand;
        public RelayCommand ClearSearchStringCommand
        {
            get
            {
                return clearSearchStringCommand ?? (clearSearchStringCommand = new RelayCommand(query =>
                {
                    concilControllUserControll.SearchTextBox.Text = "";
                }, (obj) =>
                {
                    return concilControllUserControll.SearchTextBox.Text.Length > 0;
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
