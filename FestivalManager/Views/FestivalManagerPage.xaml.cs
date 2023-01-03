using FestivalManager.Data;
using FestivalManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for FestivalManagerPage.xaml
    /// </summary>
    public partial class FestivalManagerPage : Page
    {
        public List<Fest> fests = new List<Fest>();
        FestContext context;
        public MainWindow mainWindow;
        public FestivalManagerPage(MainWindow mainWindow)
        {
            
            InitializeComponent();
            this.context = mainWindow.context;
            this.mainWindow = mainWindow;
            this.fests = context.Fests.Include(x => x.Judges).Include(x => x.Participants).ToList();
            FestList.ItemsSource = fests.OrderBy(x => x.Name).ToList();

        }

        private void FestSelectedItem(object sender, SelectionChangedEventArgs e)
        {
           mainWindow.MainFrame.Navigate(new DetailFestPage(this, FestList.SelectedItem as Fest));
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SortFestItem();
            FestList.Items.Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortFestItem();

        }
        public void RefreshPage()
        {
            FestList.ItemsSource = context.Fests.Include(x => x.Judges).Include(x => x.Participants).Where(x => x.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            FestList.Items.Refresh();

        }
        private void SortFestItem()
        {

            if (fests.Count > 0)
            {
                FestList.ItemsSource = fests.Where(x => x.Name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
                switch (SortComboBox.SelectedIndex)
                {
                    case 0:
                        {

                            FestList.ItemsSource = (FestList.ItemsSource as List<Fest>).OrderBy(x => x.FestDate).ToList();
                            break;
                        }
                    case 1:
                        {
                            FestList.ItemsSource = (FestList.ItemsSource as List<Fest>).OrderBy(x => x.Name).ToList();
                            break;
                        }
                    case 2:
                        {
                            FestList.ItemsSource = (FestList.ItemsSource as List<Fest>).OrderBy(x => x.StartTime).ToList();
                            break;
                        }

                }
                RefreshPage();
            }

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

           mainWindow.MainFrame.Navigate(new EditFestivalPage(mainWindow, this));
        }
    }
}
