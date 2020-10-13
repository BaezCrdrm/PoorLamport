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
        private ObservableCollection<Message> _messageItems1 = new ObservableCollection<Message>();
        private ObservableCollection<Message> _messageItems2 = new ObservableCollection<Message>();
        private ObservableCollection<Message> _messageItems3 = new ObservableCollection<Message>();
        // Int - Proceso
        // Mensaje
        private Dictionary<int, List<Message>> _colaEspera = new Dictionary<int, List<Message>>();

        public ObservableCollection<Message> MessageItems1
        {
            get { return _messageItems1; }
        }

        public ObservableCollection<Message> MessageItems2
        {
            get { return _messageItems2; }
        }

        public ObservableCollection<Message> MessageItems3
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

            for (int i = 0; i < 3; i++)
            {
                _colaEspera[i] = new List<Message>();
            }

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

        private void UpdateValues()
        {
            txtMessageNumber1.Text = processes[0].Clock.ToString();
            txtMessageNumber2.Text = processes[1].Clock.ToString();
            txtMessageNumber3.Text = processes[2].Clock.ToString();
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            int target = Int32.Parse(((ComboBoxItem)cbSendTo1.SelectedItem).Tag.ToString());
            string message = txtMessage1.Text;
            int sentIndex = Int32.Parse(txtMessageNumber1.Text) + 1;
            if (target == 2)
                SendMessage(target, 1, message, _messageItems2, sentIndex);
            else if(target == 3)
                SendMessage(target, 1, message, _messageItems3, sentIndex);

            UpdateValues();
        }

        private void btnSendMessage2_Click(object sender, RoutedEventArgs e)
        {
            int target = Int32.Parse(((ComboBoxItem)cbSendTo2.SelectedItem).Tag.ToString());
            string message = txtMessage2.Text;
            int sentIndex = Int32.Parse(txtMessageNumber2.Text) + 1;
            if (target == 1)
                SendMessage(target, 2, message, _messageItems1, sentIndex);
            else if (target == 3)
                SendMessage(target, 2, message, _messageItems3, sentIndex);

            UpdateValues();
        }

        private void btnSendMessage3_Click(object sender, RoutedEventArgs e)
        {
            int target = Int32.Parse(((ComboBoxItem)cbSendTo3.SelectedItem).Tag.ToString());
            string message = txtMessage3.Text;
            int sentIndex = Int32.Parse(txtMessageNumber3.Text) + 1;
            if (target == 1)
                SendMessage(target, 3, message, _messageItems1, sentIndex);
            else if (target == 2)
                SendMessage(target, 3, message, _messageItems2, sentIndex);

            UpdateValues();
        }

        public void SendMessage(int target, int origin, string message, ObservableCollection<Message> messages, int sendAgm)
        {
            target--;
            origin--;
            processes.ElementAt(origin).Clock = sendAgm;
            
            // Supone el envío del mensaje en Origin
            Message _message = new Message
            {
                Parent = processes.ElementAt(origin),
                Target = processes.ElementAt(target),
                Time = processes.ElementAt(origin).Clock,
                Text = message
            };

            // Supone recepción en Target
            bool finish = true;
            if(_message.Time <= processes.ElementAt(target).Clock + 1 || _message.Time == 1)
            {
                // Recibe mensaje
                Receive(target, _message, messages);
            } else
            {
                _colaEspera[target].Add(_message);
                finish = false;
            }

            if (finish)
            {
                if (_colaEspera[target].Count > 0)
                {
                    // Probable bucle infinito
                    do
                    {
                        foreach (Message esperando in _colaEspera[target])
                        {
                            if (esperando.Time <= processes.ElementAt(target).Clock + 1)
                            {
                                Receive(target, esperando, messages);
                                _colaEspera[target].Remove(esperando);
                                break;
                            }
                        }
                    } while (_colaEspera[target].Count > 0);
                }
            }
        }

        private void Receive(int target, Message _message, ObservableCollection<Message> messages)
        {
            int maxTime = Math.Max(_message.Time, processes.ElementAt(target).Clock);
            int clock = maxTime + 1;
            processes.ElementAt(target).Clock = maxTime;
            messages.Add(_message);
        }
    }
}
