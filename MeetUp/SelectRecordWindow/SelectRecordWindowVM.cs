using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.SelectRecordWindow
{
    class SelectRecordWindowVM
    {
        private SelectRecordWindowView selectRecordWindowView;

        public ObservableCollection<Record> Records { get; set; }
        public Record SelectedRecord { get; set; }

        public SelectRecordWindowVM(SelectRecordWindowView selectRecordWindowView, IEnumerable<Record> records)
        {
            this.selectRecordWindowView = selectRecordWindowView;
            Records = new ObservableCollection<Record>(records);
        }

        private RelayCommand accept_Click;
        public RelayCommand Accept_Click
        {
            get
            {
                return accept_Click ?? (accept_Click = new RelayCommand(obj =>
                {
                    selectRecordWindowView.DialogResult = true;
                }, (e) => { return this.SelectedRecord != null; }));
            }
        }
    }
}
