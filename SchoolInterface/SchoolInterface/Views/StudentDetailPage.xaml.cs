using SchoolInterface.Core.Models;
using SchoolInterface.Core.Results;
using SchoolInterface.Core.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SchoolInterface.Views
{
    public sealed partial class StudentDetailPage : Page, INotifyPropertyChanged
    {
        private Student student = new Student();
        private DateTimeOffset dpDateTime = DateTime.Now;

        public event PropertyChangedEventHandler PropertyChanged;

        public StudentDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                student = (Student)e.Parameter;
                dpDateTime = student.DateOfBirth;
            }
            else
            {
                student.Address = new Address();
                student.Guardian = new Guardian();
            }
            base.OnNavigatedTo(e);
        }

        private async void btnSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            student.DateOfBirth = dpDateTime.UtcDateTime;

            if (ValidateForm())
            {
                var studentService = new StudentService();
                var response = (MessageResult)studentService.Save(student);

                var dialog = new MessageDialog(response.IsSuccess ? "Save Successful!" : response.ErrorMessage);
                await dialog.ShowAsync();

                if (response.IsSuccess)
                {
                    this.Frame.Navigate(typeof(StudentsPage));
                }
            }
        }

        private bool ValidateForm()
        {
            bool validFirstName = !(student.FirstName == null || student.FirstName.Length <= 0);
            bool validLastName = !(student.LastName == null || student.LastName.Length <= 0);
            bool validGuardianName = !(student.Guardian.FirstName == null || student.Guardian.FirstName.Length <= 0);

            reqFirstName.Visibility = validFirstName ? Visibility.Collapsed : Visibility.Visible;
            reqLastName.Visibility = validLastName ? Visibility.Collapsed : Visibility.Visible;
            reqGuardianName.Visibility = validGuardianName ? Visibility.Collapsed : Visibility.Visible;

            return validFirstName & validLastName & validGuardianName;
        }
    }
}
