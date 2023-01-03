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
    /// Interaction logic for AddJudgeWindow.xaml
    /// </summary>
    public partial class AddJudgeWindow : Window
    {
        EditFestivalPage editFestival;
        public AddJudgeWindow(EditFestivalPage editFestival)
        {
            InitializeComponent();
            this.editFestival = editFestival;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SavePersonButton_Click(object sender, RoutedEventArgs e)
        {

            string Name = PersonName.Text;

            editFestival.judges.Add(new Models.Judge { FirstName = Name, LastName = Name });
            editFestival.RefreshPage();
            //editFestival.DataGridParticipants.Items.Refresh();

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
