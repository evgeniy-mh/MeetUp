using MeetUp.DBEntityModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MeetUp.RecordWindow
{
    class RecordWindowVM
    {
        private RecordWindowView recordWindowView;
        //private bool IsCreatingNewRecord;

        public Record Record { get; set; }

        public RecordWindowVM(RecordWindowView recordWindowView)
        {
            this.recordWindowView = recordWindowView;
            //IsCreatingNewRecord = true;
            Record = new Record();
        }

        public RecordWindowVM(RecordWindowView recordWindowView, Record record)
        {
            this.recordWindowView = recordWindowView;
            //IsCreatingNewRecord = false;
            Record = new Record(record);

            if (Record.Content != null)
            {
                recordWindowView.recordContentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(Record.Content)));
            }
        }

        private RelayCommand selectRecordCommand;
        public RelayCommand SelectRecordCommand
        {
            get
            {
                return selectRecordCommand ?? (selectRecordCommand = new RelayCommand(obj =>
                {
                    /*SelectConcilWindowView window = new SelectConcilWindowView();
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
                    }*/

                    //if (IsCreatingNewRecord)
                    //{
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Title = "Выберите документ протокола заседания";
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string text = File.ReadAllText(openFileDialog.FileName);
                        Record.Content = text;

                        MemoryStream stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(Record.Content));
                        recordWindowView.recordContentRichTextBox.Selection.Load(stream, DataFormats.Text);
                    }
                    //}


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
                    recordWindowView.DialogResult = true;
                }, (obj) => { return IsAllFieldsValid(recordWindowView.RecordInfoPanel); }));
            }
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
