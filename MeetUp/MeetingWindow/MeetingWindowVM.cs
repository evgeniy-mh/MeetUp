using MeetUp.DB;
using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using MeetUp.SelectConcilWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MeetUp.MeetingWindow
{
    class MeetingWindowVM : INotifyPropertyChanged
    {
        private MeetingWindowView meetingWindowView;
        private MeetingRepository MeetingRepository;
        private bool IsCreatingNewMeeting;

        public Meeting Meeting { get; set; }

        public MeetingWindowVM(MeetingWindowView meetingWindowView)
        {
            this.meetingWindowView = meetingWindowView;
            MeetingRepository = new MeetingRepository(new MeetUpContext());
            IsCreatingNewMeeting = true;
            Meeting = new Meeting();
        }

        public MeetingWindowVM(MeetingWindowView meetingWindowView, Meeting meeting)
        {
            this.meetingWindowView = meetingWindowView;
            MeetingRepository = new MeetingRepository(new MeetUpContext());
            IsCreatingNewMeeting = false;
            Meeting = new Meeting(meeting);
        }

        private RelayCommand selectConcilCommand;
        public RelayCommand SelectConcilCommand
        {
            get
            {
                return selectConcilCommand ?? (selectConcilCommand = new RelayCommand(obj =>
                  {
                      SelectConcilWindowView window = new SelectConcilWindowView();
                      if (window.ShowDialog() == true)
                      {
                          if (IsCreatingNewMeeting)
                          {
                              Meeting.Concil = window.SelectedConcil;
                          }
                          else
                          {
                              MeetingRepository.SetConcilForMeeting(Meeting, window.SelectedConcil);
                              Meeting.Concil = window.SelectedConcil;
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
                    meetingWindowView.DialogResult = true;
                }, (obj) => { return IsAllFieldsValid(meetingWindowView.MeetingInfoPanel); }));
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
