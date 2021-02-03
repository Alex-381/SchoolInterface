using SchoolInterface.Core.Models;
using SchoolInterface.Core.Requests;
using SchoolInterface.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SchoolInterface.Views
{
    public sealed partial class StudentSubjectsPage : Page
    {
        private Student student = new Student();
        public ObservableCollection<Subject> studentSubjects1 { get; } = new ObservableCollection<Subject>();
        public ObservableCollection<Subject> availableSubjects1 { get; } = new ObservableCollection<Subject>();

        private Subject selectedAvailableSubject
        {
            get { return _selectedAvailableSubject; }
            set
            {
                _selectedAvailableSubject = value;
                if (_selectedAvailableSubject != null)
                {
                    AvailableSubjectSelected();
                }
                else
                {
                    NoAvailableSubjectSelected();
                }
            }
        }

        private Subject _selectedAvailableSubject;

        private Subject selectedStudentSubject
        {
            get { return _selectedStudentSubject; }
            set
            {
                _selectedStudentSubject = value;
                if (_selectedStudentSubject != null)
                {
                    StudentSubjectSelected();
                }
                else
                {
                    NoStudentSubjectSelected();
                }
            }
        }

        private Subject _selectedStudentSubject;

        public StudentSubjectsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                student = (Student)e.Parameter;
                lblStudentName.Text = String.Format("{0} {1}'s Subjects", student.FirstName, student.LastName);
            }
            base.OnNavigatedTo(e);

            GetData();
        }

        private void GetData()
        {
            studentSubjects1.Clear();
            availableSubjects1.Clear();

            var studentService = new StudentService();
            var data1 = studentService.GetSubjects(student.StudentID);

            foreach(var item in data1)
            {
                studentSubjects1.Add(item);
            }

            var subjectService = new SubjectService();
            var subjects = subjectService.Get();

            var data2 = subjects
                .Where(p => studentSubjects1.All(p2 => p2.SubjectID != p.SubjectID))
                .ToList();

            foreach (var item in data2)
            {
                availableSubjects1.Add(item);
            }
        }

        private void AvailableSubjectSelected()
        {
            btnAdd.IsEnabled = true;
        }

        private void NoAvailableSubjectSelected()
        {
            btnAdd.IsEnabled = false;
        }

        private void StudentSubjectSelected()
        {
            btnDelete.IsEnabled = true;
        }

        private void NoStudentSubjectSelected()
        {
            btnDelete.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var request = new AddStudentSubjectRequest()
            {
                StudentID = student.StudentID,
                SubjectID = selectedAvailableSubject.SubjectID
            };

            var studentService = new StudentService();
            var result = studentService.AddSubject(request);

            GetData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var request = new DeleteStudentSubjectRequest()
            {
                StudentID = student.StudentID,
                SubjectID = selectedStudentSubject.SubjectID
            };

            var studentService = new StudentService();
            var result = studentService.DeleteSubject(request);

            GetData();
        }

        private void grdAvailableSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grdStudentSubjects.SelectedIndex = -1;
            if (e.AddedItems.Count > 0)
            {
                selectedAvailableSubject = (Subject)e.AddedItems[0];
            }
            else
            {
                selectedAvailableSubject = null;
            }
        }

        private void grdStudentSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grdAvailableSubjects.SelectedIndex = -1;

            if (e.AddedItems.Count > 0)
            {
                selectedStudentSubject = (Subject)e.AddedItems[0];
            }
            else
            {
                selectedStudentSubject = null;
            }
        }
    }
}
