using MeetUp.DB;
using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using MeetUp.MeetingWindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MeetUp.MeetingsControll
{
    class MeetingControllVM : INotifyPropertyChanged
    {
        public Meeting SelectedMeeting { get; set; }
        private MeetingRepository MeetingRepository;
        private ObservableCollection<Meeting> _meetings;
        public ObservableCollection<Meeting> Meetings
        {
            get
            {
                return _meetings;
            }
            set
            {
                _meetings = value;
                OnPropertyChanged("Meetings");
            }
        }

        public MeetingControllVM()
        {
            MeetingRepository = new MeetingRepository(new MeetUpContext());
            Meetings = new ObservableCollection<Meeting>(MeetingRepository.Get("Concil"));
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    MeetingWindowView window = new MeetingWindowView();
                    if (window.ShowDialog() == true)
                    {
                        MeetingRepository.Create(window.Meeting);
                        Meetings = new ObservableCollection<Meeting>(MeetingRepository.Get("Concil"));
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
                    MeetingWindowView window = new MeetingWindowView(SelectedMeeting);
                    if (window.ShowDialog() == true)
                    {

                        Meetings = new ObservableCollection<Meeting>(MeetingRepository.Get("Concil"));
                    }

                }, (obj) => { return SelectedMeeting != null; }));
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
                        String.Format("Вы действительно хотите удалить заседание {0} ?",
                        SelectedMeeting.Name),
                        "Удалить заседание?", button, icon);

                    if (result == MessageBoxResult.OK)
                    {
                        MeetingRepository.Remove(SelectedMeeting);
                        Meetings = new ObservableCollection<Meeting>(MeetingRepository.Get("Concil"));
                    }
                }, (obj) => { return SelectedMeeting != null; }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }        
    }
}
