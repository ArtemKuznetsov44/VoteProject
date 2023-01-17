using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
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
using VoteClient.ServiceVote;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;

namespace VoteClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    // Имплементируем интерфейс, который содержит в себе лишь один метод,
    // но при этом, он отвечает за возможность сервера отсылать сообщения нашим клиентам:
    public partial class MainWindow : Window
    {
        // Нам нужно создать объект типа нашего хоста в нашем клиенте, чтобы мы могли взаимодейстовать 
        // с его методами: 
        private ServiceVoteClient _client;
        private int _clientId;
        //public ServiceVoteClient Client { get { return _client; } set { _client = value; } }

        //public int ClientId { get { return _clientId; } set { _clientId = value; } }

        private List<CheckBox> checkBoxes = new List<CheckBox>();

        private bool _answerIsReady = false;

        private string _clientAnswer; 

        public MainWindow()
        {
            InitializeComponent();

        }

        private void GenerateMessageBox()
        {
            MessageBox.Show("Вы еще не проголосовали!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DisconnectClient()
        {
            if (_answerIsReady)
            {

                _client.SendAnswerMessage(_clientAnswer, _clientId);
                
                _client.Disconnect(_clientId); 
               
                _client.InnerChannel.Close();
                _client.Close();
                _client = null;
               //  _client.Disconnect(_clientId);
                this.Close();
            }
            else
                GenerateMessageBox();
                
        }
        
        private void bDisconnect_Click(object sender, RoutedEventArgs e) { DisconnectClient(); }
      
        public void CreatedCheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            checkBox.IsChecked = true;
            _clientAnswer = checkBox.Content.ToString().Trim();
            
            // _client.Close();

            _answerIsReady = !_answerIsReady;

            foreach (var bt in checkBoxes)
            {
                if (bt != (CheckBox)sender)
                    bt.IsEnabled = false;
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _client = new ServiceVoteClient();
            _clientId = _client.Connect(); 

            string question;
            string[] arrVariants;

            using (StreamReader reader = new StreamReader(@"../../QuestionAndVariants.txt"))
            {
                string res = reader.ReadToEnd();
                question = res.Substring(0, res.IndexOf("-"));
                arrVariants = res.Substring(res.IndexOf("-") + 1).Split('-');
            }

            lbQuestion.Content = question;


            foreach (string item in arrVariants)
            {
                CheckBox checkBox = new CheckBox() { Content = item, FontSize = 16, Background = Brushes.White, BorderThickness = new Thickness(0, 0, 0, 0), VerticalContentAlignment = VerticalAlignment.Center, Height = 24, IsChecked = false };

                checkBoxes.Add(checkBox);

                // Добавление события на созданный обеъкт checkBox:
                checkBox.Click += new RoutedEventHandler(CreatedCheckBoxClick);

                // Добавление элемента в StackPanel
                spVariants.Children.Add(checkBox);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!_answerIsReady)
            {
                e.Cancel = true;
                GenerateMessageBox();
            } 
            else if (_answerIsReady && sender.GetType().Equals(typeof(Button)))
            {
               DisconnectClient();
            }
        }
    }
}
