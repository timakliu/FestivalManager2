using FestivalManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FestivalManager.Views
{
    /// <summary>
    /// Interaction logic for EditFestivalPage.xaml
    /// </summary>
    public partial class EditFestivalPage : Page
    {
        MainWindow _MainWindow;
        DetailFestPage detailFestPage;
        FestivalManagerPage festivalManagerPage;
        public List<Judge> judges = new List<Judge>();
        public List<Participant> participants = new List<Participant>();
        Fest fest;

        public EditFestivalPage(MainWindow mainWindow, DetailFestPage detailFestPage)
        {
            InitializeComponent();
            judges = detailFestPage.fest.Judges.ToList();
            participants = detailFestPage.fest.Participants.ToList();
            this.detailFestPage = detailFestPage;
            this._MainWindow = mainWindow;
            this.fest = detailFestPage.fest;
            DataGridParticipants.ItemsSource = participants;


            JudgeItems.ItemsSource = judges;
            NameTextBox.Text = fest.Name;
            TimeStartTimePicker.Text = fest.StartTime.ToString("HH:mm");
            TimeEndTimePicker.Text = fest.EndTime.ToString("HH:mm");
            FestDateDatePicker.Text = fest.FestDate.ToString("dd.MM.yyyy");
        }
        public EditFestivalPage(MainWindow mainWindow, FestivalManagerPage festivalManagerPage)
        {

            InitializeComponent();
            this._MainWindow = mainWindow;
            this.festivalManagerPage = festivalManagerPage;

            this.fest = new Fest() { Participants = new List<Participant>(), Judges = new List<Judge>(), FestDate = DateTime.Now };
            DataGridParticipants.ItemsSource = participants;
            JudgeItems.ItemsSource = judges;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void RefreshPage()
        {

            DataGridParticipants.Items.Refresh();
            JudgeItems.Items.Refresh();
            _MainWindow.context.SaveChanges();
            Debug.WriteLine(JudgeItems.Items.Count);
            if (festivalManagerPage != null)
            {
                festivalManagerPage.RefreshPage();
            }
            else { detailFestPage.RefreshPage(); }



        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            new AddParticipantWindow(this).Show();

        }

        private void TrashButton_Click(object sender, RoutedEventArgs e)
        {
            participants.Remove(participants.Where((p) => p.Number == (DataGridParticipants.SelectedItem as Participant).Number).First());
            RefreshPage();
        }
        private void AddJudgeButton_Click(object sender, RoutedEventArgs e)
        {
            new AddJudgeWindow(this).Show();
        }

        private void RemoveJudgeButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = ((Button)sender).Parent as StackPanel;

            judges.Remove(judges.Last());

            RefreshPage();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

            _MainWindow.MainFrame.NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            fest.Name = NameTextBox.Text;
            fest.Description = "";
            fest.StartTime = DateTime.Parse(TimeStartTimePicker.Text);
            fest.EndTime = DateTime.Parse(TimeEndTimePicker.Text);
            fest.FestDate = DateTime.Parse(FestDateDatePicker.Text);

            fest.Judges.Clear();
            fest.Judges.AddRange(judges);
            fest.Participants.Clear();
            fest.Participants.AddRange(participants);

            if (festivalManagerPage != null)
            {
                _MainWindow.context.Fests.Add(fest);
                _MainWindow.context.SaveChanges();
            }
            RefreshPage();
            _MainWindow.MainFrame.NavigationService.GoBack();
        }
    }
}
