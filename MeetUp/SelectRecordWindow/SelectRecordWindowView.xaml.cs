using MeetUp.DBEntityModels;
using System.Collections.Generic;
using System.Windows;

namespace MeetUp.SelectRecordWindow
{
    public partial class SelectRecordWindowView : Window
    {
        private SelectRecordWindowVM selectRecordWindowVM;

        public Record SelectedRecord
        {
            get
            {
                return selectRecordWindowVM.SelectedRecord;
            }
        }

        public SelectRecordWindowView(IEnumerable<Record> records)
        {
            InitializeComponent();
            selectRecordWindowVM = new SelectRecordWindowVM(this, records);
            DataContext = selectRecordWindowVM;
        }


    }
}
