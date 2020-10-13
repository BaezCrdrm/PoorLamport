using PoorLamportTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PoorLamportTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<PoorLamportTest.Models.Message> _messageItems1 = new ObservableCollection<Message>();
        private ObservableCollection<PoorLamportTest.Models.Message> _messageItems2 = new ObservableCollection<Message>();
        private ObservableCollection<PoorLamportTest.Models.Message> _messageItems3 = new ObservableCollection<Message>();

        public ObservableCollection<PoorLamportTest.Models.Message> MessageItems1
        {
            get { return _messageItems1; }
        }

        public ObservableCollection<PoorLamportTest.Models.Message> MessageItems2
        {
            get { return _messageItems2; }
        }

        public ObservableCollection<PoorLamportTest.Models.Message> MessageItems3
        {
            get { return _messageItems3; }
        }

        List<Process> processes;
        public MainPage()
        {
            this.InitializeComponent();
            processes = new List<Process>
            {
                new Process(1),
                new Process(2),
                new Process(3)
            };

            //LoadData();
        }

        private void LoadData()
        {
            _messageItems1.Add(new Message
            {
                Parent = processes.ElementAt(0),
                Target = processes.ElementAt(1),
                Text = "Mensaje de prueba 1"
            });
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            int target = Int32.Parse(((ComboBoxItem)cbSendTo1.SelectedItem).Tag.ToString());
            string message = txtMessage1.Text;
            if(target == 2)
                SendMessage(target, 1, message, _messageItems2);
            else if(target == 3)
                SendMessage(target, 1, message, _messageItems3);
        }

        private void btnSendMessage2_Click(object sender, RoutedEventArgs e)
        {
            int target = Int32.Parse(((ComboBoxItem)cbSendTo2.SelectedItem).Tag.ToString());
            string message = txtMessage2.Text;
            if (target == 1)
                SendMessage(target, 2, message, _messageItems1);
            else if (target == 3)
                SendMessage(target, 2, message, _messageItems3);
        }

        private void btnSendMessage3_Click(object sender, RoutedEventArgs e)
        {
            int target = Int32.Parse(((ComboBoxItem)cbSendTo3.SelectedItem).Tag.ToString());
            string message = txtMessage3.Text;
            if (target == 1)
                SendMessage(target, 3, message, _messageItems1);
            else if (target == 2)
                SendMessage(target, 3, message, _messageItems2);
        }

        public void SendMessage(int target, int origin, string message, ObservableCollection<Message> messages)
        {
            messages.Add(new Message
            {
                Parent = processes.ElementAt(origin),
                Target = processes.ElementAt(target),
                Text = message
            });
        }
    }
}
