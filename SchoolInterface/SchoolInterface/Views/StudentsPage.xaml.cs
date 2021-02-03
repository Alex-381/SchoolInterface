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
    public sealed partial class StudentsPage : Page
    {
        public ObservableCollection<Student> Source { get; } = new ObservableCollection<Student>();

        private Student selectedStudent
        {
            get { return _selectedStudent; } set
            {
                _selectedStudent = value;
                if (_selectedStudent != null)
                {
                    StudentSelected();
                }
                else
                {
                    NoStudentSelected();
                }
            }
        }

        private Student _selectedStudent;

        public StudentsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            GetData();
        }

        private void GetData()
        {
            Source.Clear();
            var data = new StudentService().Get();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        private void StudentSelected()
        {
            btnDelete.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnEditSubjects.IsEnabled = true;
        }

        private void NoStudentSelected()
        {
            btnDelete.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnEditSubjects.IsEnabled = false;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                selectedStudent = (Student)e.AddedItems[0];
            }
            else
            {
                selectedStudent = null;
            }
        }

        private void btnEdit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StudentDetailPage), selectedStudent);
        }

        private void btnDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var result = new StudentService().Delete(selectedStudent.StudentID);

            if (result.IsSuccess)
            {
                GetData();
            }
        }

        private void btnAddNew_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StudentDetailPage));
        }

        private void btnEditSubjects_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StudentSubjectsPage), selectedStudent);
        }
    }
}
