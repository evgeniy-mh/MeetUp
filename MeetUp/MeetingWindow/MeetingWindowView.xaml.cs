using MeetUp.DBEntityModels;
using System.Windows;

namespace MeetUp.MeetingWindow
{
    public partial class MeetingWindowView : Window
    {
        private MeetingWindowVM meetingWindowVM;

        public MeetingWindowView()
        {
            InitializeComponent();
            meetingWindowVM = new MeetingWindowVM(this);
            DataContext = meetingWindowVM;
        }

        public MeetingWindowView(Meeting meeting)
        {
            InitializeComponent();
            meetingWindowVM = new MeetingWindowVM(this, meeting);
            DataContext = meetingWindowVM;
        }
    }
}
