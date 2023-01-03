using FestivalManager.Models;
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
using System.Windows.Shapes;

namespace FestivalManager.Views
{
    /// <summary>
    /// Interaction logic for AddRatingWindow.xaml
    /// </summary>
    public partial class AddRatingWindow : Window
    {
        DetailFestPage detailFestPage;
        Participant participant;
        public AddRatingWindow(DetailFestPage detailFestPage, List<Judge> judges, Participant participant)
        {
            InitializeComponent();

            this.detailFestPage = detailFestPage;
            this.participant = participant;
            JudgeList.ItemsSource = judges;

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            StackPanel stackPanel = ((Slider)sender).Parent as StackPanel;
            (VisualTreeHelper.GetChild(stackPanel, 1) as TextBlock).Text = ((Slider)sender).Value.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            double score = 0;
            for (int i = 0; i < JudgeList.Items.Count; i++)
            {
                UIElement uiElement =
                    (UIElement)JudgeList.ItemContainerGenerator.ContainerFromIndex(i);
                score += (VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(uiElement, 0), 1), 0) as Slider).Value;
            }
            score /= JudgeList.Items.Count;

            participant.Score = Math.Round(score, 2);
            detailFestPage.DataGridParticipants.Items.Refresh();
            this.Close();

        }
    }
}
