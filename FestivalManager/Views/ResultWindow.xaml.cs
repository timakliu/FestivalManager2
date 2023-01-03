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
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        List<Participant> participants;
        public ResultWindow(List<Participant> participantsList)
        {
            InitializeComponent();
            this.participants = participantsList.OrderBy(x => x.Score).Reverse().ToList();

            if (participants[0].Score > 0) { NamePlace1.Text = participants[0].FirstName + " " + participants[0].LastName; ScorePlace1.Text = participants[0].Score.ToString(); }
            else NamePlace1.Text = "Результат еще не определен";
            if (participants[1].Score > 0) { NamePlace2.Text = participants[1].FirstName + " " + participants[1].LastName; ScorePlace2.Text = participants[1].Score.ToString(); }
            else NamePlace2.Text = "Результат еще не определен";
            if (participants[2].Score > 0) { NamePlace3.Text = participants[2].FirstName + " " + participants[2].LastName; ScorePlace3.Text = participants[2].Score.ToString(); }
            else NamePlace3.Text = "Результат еще не определен";





        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
