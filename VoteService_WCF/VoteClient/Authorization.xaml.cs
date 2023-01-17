using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel.Channels;
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
using VoteClient.ServiceVote; 

namespace VoteClient
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        bool passwordIsVisible = false;

        public Authorization()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ShowInsertedPassword()
        {
            tbInsertPassword.Text = pbPassword.Password;

            pbPassword.Visibility = Visibility.Hidden; 

            tbInsertPassword.Visibility= Visibility.Visible;

            btImage.Source = BitmapFrame.Create(new Uri("../../Icons/eye.png", UriKind.Relative));    

            passwordIsVisible = !passwordIsVisible;
        }

        private void HideInsertedPassword()
        {
            tbInsertPassword.Visibility = Visibility.Hidden;

            pbPassword.Visibility = Visibility.Visible;

            btImage.Source =  BitmapFrame.Create(new Uri(@"../../Icons/hide.png", UriKind.Relative));

            passwordIsVisible = !passwordIsVisible;
        }

        private void bShowHide_Click(object sender, RoutedEventArgs e)
        {
            if (!passwordIsVisible)
                ShowInsertedPassword();
            else
                HideInsertedPassword();
        } 

        private void bConnect_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(ConfigurationManager.AppSettings["password"].ToString());
            
            if (pbPassword.Password.Trim() == ConfigurationManager.AppSettings["password"].ToString())
            {
                MessageBox.Show("Everything is ok!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow mainWindow = new MainWindow();

                //ServiceVoteClient client = new ServiceVoteClient();

                //Debug.WriteLine($"pbPassword: {pbPassword.Password}");

                //int clientId = client.Connect();

                //// Присвоние полям главного окна значений: 
                //mainWindow.Client = client;
                //mainWindow.ClientId = clientId;

                // Закрытие данного окна:
                this.Close();

                // Открытие главного окна:
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Inccorect password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void VoteCallBack(string message)
        {
            throw new NotImplementedException();
        }
    }
}
