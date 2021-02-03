using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using SchoolInterface.Core.Models;
using SchoolInterface.Core.Services;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
namespace SchoolInterface.Views
{
    public sealed partial class SubjectsPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<Subject> Source { get; } = new ObservableCollection<Subject>();

        private Subject selectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                _selectedSubject = value;
                if (_selectedSubject != null)
                {
                    SubjectSelected();
                }
                else
                {
                    NoSubjectSelected();
                }
            }
        }

        private Subject _selectedSubject;

        public SubjectsPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            GetData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void GetData()
        {
            Source.Clear();
            var data = new SubjectService().Get();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        private void SubjectSelected()
        {
            btnDelete.IsEnabled = true;
            btnEdit.IsEnabled = true;
        }

        private void NoSubjectSelected()
        {
            btnDelete.IsEnabled = false;
            btnEdit.IsEnabled = false;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                selectedSubject = (Subject)e.AddedItems[0];
            }
            else
            {
                selectedSubject = null;
            }
        }

        private void btnEdit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SubjectDetailPage), selectedSubject);
        }

        private void btnDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var result = new SubjectService().Delete(selectedSubject.SubjectID);

            if (result.IsSuccess)
            {
                GetData();
            }
        }

        private void btnAddNew_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SubjectDetailPage));
        }
    }
}
