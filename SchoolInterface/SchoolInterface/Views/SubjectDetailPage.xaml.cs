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
    public sealed partial class SubjectDetailPage : Page, INotifyPropertyChanged
    {
        private Subject subject = new Subject();

        public SubjectDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                subject = (Subject)e.Parameter;
            }
            base.OnNavigatedTo(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void btnSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                var subjectService = new SubjectService();
                var response = (MessageResult)subjectService.Save(subject);

                var dialog = new MessageDialog(response.IsSuccess ? "Save Successful!" : response.ErrorMessage);
                await dialog.ShowAsync();

                if (response.IsSuccess)
                {
                    this.Frame.Navigate(typeof(SubjectsPage));
                }
            }
        }

        private bool ValidateForm()
        {
            bool validName = !(subject.SubjectName == null || subject.SubjectName.Length <= 0);

            reqName.Visibility = validName ? Visibility.Collapsed : Visibility.Visible;

            return validName;
        }
    }
}
