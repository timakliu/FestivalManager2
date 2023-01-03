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
using System.Windows.Shapes;

namespace FestivalManager.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        List<User> users;
        public LoginWindow()
        {
            InitializeComponent();
            Loaded += LoginWindow_Loaded;
            using (FestContext context = new FestContext())
            {
                users = context.Users.ToList();
            }
            ErrorMessage.Visibility = Visibility.Collapsed;

        }

        // при загрузке окна
        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            using(FestContext context = new FestContext())
            {
                context.Fests.RemoveRange(context.Fests);
                context.Judges.RemoveRange(context.Judges);
                context.Participants.RemoveRange(context.Participants);
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();
                // гарантируем, что база данных создана
                context.Database.EnsureCreated();

                // Заполняем первичными данными, если бд пуста
                if (!(context.Users.Count() > 0))
                {
                    context.Users.Add(new User() { Login = "admin", Password = "123" });
                    context.Users.Add(new User() { Login = "1", Password = "1" });
                    context.SaveChanges();

                }

                if (!(context.Fests.Count() > 0))
                {
                    context.Fests.Add(new Fest() { Name = "Голос", Description = "", StartTime = DateTime.Parse("10:11:00"), FestDate = DateTime.Parse("26.12.2022"), EndTime = DateTime.Parse("12:00:00") });
                    context.Fests.Add(new Fest() { Name = "Весенний фестиваль", Description = "", StartTime = DateTime.Parse("10:00:00"), FestDate = DateTime.Parse("15.12.2022"), EndTime = DateTime.Parse("14:11:00"), });
                    context.Fests.Add(new Fest() { Name = "Осенний фестиваль", Description = "", StartTime = DateTime.Parse("06:00:00"), FestDate = DateTime.Parse("22.11.2022"), EndTime = DateTime.Parse("18:30:00"), });
                    context.Fests.Add(new Fest() { Name = "Лунная ночь", Description = "", StartTime = DateTime.Parse("09:20:00"), FestDate = DateTime.Parse("03.01.2023"), EndTime = DateTime.Parse("15:00:00"), });

                    context.SaveChanges();
                    Fest fest = context.Fests.FirstOrDefault();

                    foreach (Fest buffFest in context.Fests.Include(x => x.Judges).Include(x => x.Participants).ToList())
                    {
                        buffFest.Participants.AddRange(new List<Participant>()
                        {
                            new Participant() { FirstName = "Тимофей", LastName = "Карпинский", Number = 1, TimePerformance = DateTime.Parse("10:30:00"),},
                            new Participant() { FirstName = "Алексей", LastName = "Усачев", Number = 2,  TimePerformance = DateTime.Parse("11:00:00") , },
                            new Participant() { FirstName = "Евгений", LastName = "Пянко", Number = 3,  TimePerformance = DateTime.Parse("11:30:00"), }
                        });
                        buffFest.Judges.AddRange(new List<Judge>() {
                         new Judge() { FirstName = "Артемий", LastName = "Лебедев", },
                          new Judge() { FirstName = "Амир", LastName = "Хамза" , },
                          new Judge() { FirstName = "Рудлольф", LastName = "Карамбеков", }
                        });

                    }
                    context.SaveChanges();
                }
                context.Users.Load();
            }
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (users.Where((user) => user.Login == inputLogin.Text).Where((user) => user.Password == inputPassword.Password).Count() > 0)
            {
                new MainWindow().Show();
                this.Close();
            }
            else
            {
                inputPassword.Password = "";
                ErrorMessage.Visibility = Visibility.Visible;

            }


        }
       
     
    }
}
