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
    /// Interaction logic for AddParticipantWindow.xaml
    /// </summary>
    public partial class AddParticipantWindow : Window
    {
        EditFestivalPage editFestival;
        public AddParticipantWindow(EditFestivalPage editFestival)
        {
            InitializeComponent();
            this.editFestival = editFestival;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SavePersonButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int number = Int32.Parse(PersonNumber.Text);

                editFestival.participants.Add(new Participant { FirstName = PersonFirstName.Text, LastName = PersonLastName.Text, Number = Convert.ToInt32(PersonNumber.Text), TimePerformance = DateTime.Parse("12.01.1999 " + PersonTime.Text) });
                editFestival.RefreshPage();
                this.Close();
            }
            catch (FormatException)
            {
                PersonNumber.Background = new SolidColorBrush(Color.FromArgb(255, 242, 184, 184));
                MessageBox.Show("Номер должен быть числовым");
            }
            catch (OverflowException)
            {
                PersonNumber.Background = new SolidColorBrush(Color.FromArgb(255, 242, 184, 184));
                MessageBox.Show("Слишком большой номер");
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
