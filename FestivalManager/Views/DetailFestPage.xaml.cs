using FestivalManager.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for DetailFestPage.xaml
    /// </summary>
    public partial class DetailFestPage : Page
    {
        MainWindow mainWindow;
        FestivalManagerPage festivalManagerPage;
        public Fest fest;
        private IQueryable<Fest> fests;

        public DetailFestPage(FestivalManagerPage festivalManagerPage, Fest fest)
        {
            InitializeComponent();
            this.fest = fest;
            this.DataContext = fest;
            this.festivalManagerPage = festivalManagerPage;
            this.mainWindow = festivalManagerPage.mainWindow;
            DataGridParticipants.ItemsSource = fest?.Participants;
            JudgeItems.ItemsSource = fest?.Judges;
        }

        public DetailFestPage(FestivalManagerPage festivalManagerPage, IQueryable<Fest> fests)
        {
            this.festivalManagerPage = festivalManagerPage;
            this.fests = fests;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            festivalManagerPage.FestList.UnselectAll();
            mainWindow.MainFrame.NavigationService.GoBack();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MainFrame.Navigate(new EditFestivalPage(mainWindow, this));
        }

        public void RefreshPage()
        {
            FestName.Text = fest.Name;
            RunEndTime.Text = fest.EndTime.ToString("HH:mm");
            RunStartTime.Text = fest.StartTime.ToString("HH:mm");
            RunDate.Text = fest.FestDate.ToString("dd.MM.yyyy");
            DataGridParticipants.Items.Refresh();
            JudgeItems.Items.Refresh();
            festivalManagerPage.RefreshPage();
        }



        private void TrashButton_Click(object sender, RoutedEventArgs e)
        {
            Participant participant = fest.Participants.Where((p) => p.Number == (DataGridParticipants.SelectedItem as Participant).Number).First();
            AddRatingWindow addRatingWindow = new AddRatingWindow(this, fest.Judges, participant);
            addRatingWindow.Show();

            //(fest.Participants as List<Participant>).Remove(fest.Participants.Where((p) => p.Number == (DataGridParticipants.SelectedItem as Participant).Number).First());
            //DataGridParticipants.Items.Refresh();
        }

        public class P
        {
            public int Number { get; set; }
        }
        private void XMLButton_Click(object sender, RoutedEventArgs e)
        {


            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(fest.GetType());
            x.Serialize(Console.Out, fest);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.ShowDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save text Files";
            saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "xml";
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            FileStream fileStream = new FileStream(saveFileDialog1.FileName + ".xml", FileMode.Create);
            x.Serialize(fileStream, fest);
            fileStream.Close();


        }

        private void GetResultsButton_Click(object sender, RoutedEventArgs e)
        {
            new ResultWindow(fest.Participants).Show();
        }
    }
}
