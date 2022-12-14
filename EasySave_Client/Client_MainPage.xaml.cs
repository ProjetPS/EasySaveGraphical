using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EasySave_Client
{
    /// <summary>
    /// Logique d'interaction pour Client_MainPage.xaml
    /// </summary>
    public partial class Client_MainPage : Page
    {
        private static string texte;
        public Client_MainPage()
        {
            InitializeComponent();
            ClientSocket_Read();
        }

        async void ClientSocket_Read()
        {
            // Initialize socket
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11_000);

            using Socket client = new Socket(
            ipEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

            await client.ConnectAsync(ipEndPoint);
            while (true)
            {
                // Send message.
                var message = "Client request <|EOM|>";
                var messageBytes = Encoding.UTF8.GetBytes(message);
                _ = await client.SendAsync(messageBytes, SocketFlags.None);

                // Receive ack.
                var buffer = new byte[1_024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);

                //Print message on WPF
                Client_TextRecieved.Text = response;
                Client_ProgressBar.Value = Convert.ToDouble(response);

            }
        }
    }
}
