﻿using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using MeetUp.SelectConcilWindow;
using MeetUp.SelectRecordWindow;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace MeetUp.MeetingWindow
{
    class MeetingWindowVM : INotifyPropertyChanged
    {
        private UnitOfWork unitOfWork;
        private MeetingWindowView meetingWindowView;
        private bool IsCreatingNewMeeting;

        public Meeting Meeting { get; set; }

        public MeetingWindowVM(MeetingWindowView meetingWindowView)
        {
            unitOfWork = new UnitOfWork();
            this.meetingWindowView = meetingWindowView;
            IsCreatingNewMeeting = true;
            Meeting = new Meeting();
        }

        public MeetingWindowVM(MeetingWindowView meetingWindowView, Meeting meeting)
        {
            unitOfWork = new UnitOfWork();
            this.meetingWindowView = meetingWindowView;
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
                            //unitOfWork.MeetingRepository.SetConcilForMeeting(Meeting, window.SelectedConcil);
                            Meeting.Concil = window.SelectedConcil;
                        }
                    }
                }));
            }
        }

        private RelayCommand addRecordCommand;
        public RelayCommand AddRecordCommand
        {
            get
            {
                return addRecordCommand ?? (addRecordCommand = new RelayCommand(obj =>
                {
                    var freeRecords = unitOfWork.RecordRepository.GetFreeRecords();
                    SelectRecordWindowView window = new SelectRecordWindowView(freeRecords);
                    if (window.ShowDialog() == true)
                    {
                        Meeting.Records.Add(window.SelectedRecord);
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

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            unitOfWork.Dispose();
        }
    }
}
