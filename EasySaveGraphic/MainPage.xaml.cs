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

using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EasySaveGraphic
{
    /// <summary>
    /// Logique d'interaction pour MainPage.xaml
    /// </summary>
    
    public partial class MainPage : Page
    {
        private readonly double opacity = 0.25;
        private bool isLangFR = false;
        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(bool isLangFR)
        {
            InitializeComponent();
            saveList_btn.Content = "Mes travaux de sauvegarde";
            createWS_btn.Content = "Créer un nouveau travail de sauvegarde";
            executeWS_btn.Content = "Éxecuter un travail de sauvegarde";
            settings_btn.ToolTip = "Paramètres";
            exit_btn.Content = "Quitter";
            clientRemote_btn.ToolTip = "Accès client à distance";
            FR_btn.Opacity = opacity;
            EN_btn.Opacity = 1;
            this.isLangFR = true;
        }

        //Change langage on FR
        private void FR_Lang(object sender, MouseButtonEventArgs e)
        {
            saveList_btn.Content = "Mes travaux de sauvegarde";
            createWS_btn.Content = "Créer un nouveau travail de sauvegarde";
            executeWS_btn.Content = "Éxecuter un travail de sauvegarde";
            settings_btn.ToolTip = "Paramètres";
            Application.Current.MainWindow.Title = "EasySave - Menu principal";
            exit_btn.Content = "Quitter";
            clientRemote_btn.ToolTip = "Accès client à distance";
            FR_btn.Opacity = opacity;
            EN_btn.Opacity = 1;
            this.isLangFR = true;
        }

        //Change langage on EN
        private void EN_Lang(object sender, MouseButtonEventArgs e)
        {
            saveList_btn.Content = "My worksave list";
            createWS_btn.Content = "Create a new worksave";
            executeWS_btn.Content = "Execute a worksave";
            settings_btn.ToolTip = "Settings";
            Application.Current.MainWindow.Title = "EasySave - Main menu";
            exit_btn.Content = "Exit";
            clientRemote_btn.ToolTip = "Client remote access";
            EN_btn.Opacity = opacity;
            FR_btn.Opacity = 1;
            this.isLangFR = false;
        }

        private void WorkSaveList(object sender, RoutedEventArgs e)
        {
            //Go to worksave list page
            var window = (Mainwindow)Application.Current.MainWindow;

            WorkSaveList goToWSL = new WorkSaveList(isLangFR);

            // Change window title in appropriate language
            if (this.isLangFR)
            {
                window.Title = "EasySave - Liste des travaux de sauvegarde";
            }
            else
            {
                window.Title = "EasySave - Worksave list";
            }
            window.Content = goToWSL;
        }

        private void CreateWorkSave(object sender, RoutedEventArgs e)
        {
            //Go to backup job creation
            var window = (Mainwindow)Application.Current.MainWindow;
            Create goToCreate = new Create(isLangFR);

            // Change window title in appropriate language
            if (this.isLangFR)
            {
                window.Title = "EasySave - Création";
            }
            else
            {
                window.Title = "EasySave - Create";
            }

            window.Content = goToCreate;
        }

        private void ExecuteWS_Click(object sender, RoutedEventArgs e)
        {
            //Go to backup job execution
            var window = (Mainwindow)Application.Current.MainWindow;
            Execute goToCreate = new Execute(isLangFR);

            // Change window title in appropriate language
            if (isLangFR)
            {
                window.Title = "EasySave - Éxecution";
            }
            else
            {
                window.Title = "EasySave - Execution";
            }

            window.Content = goToCreate;
        }

        private void Parameters_Click(object sender, RoutedEventArgs e)
        {
            //Go to backup job execution
            var window = (Mainwindow)Application.Current.MainWindow;
            settings goToSettings = new settings(isLangFR);

            // Change window title in appropriate language
            if (this.isLangFR)
            {
                window.Title = "EasySave - Paramètres";
            }
            else
            {
                window.Title = "EasySave - Settings";
            }

            window.Content = goToSettings;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // If language is FR
            if (isLangFR)
            {
                if (MessageBox.Show("Voulez-vous vraiment quitter EasySave ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // If Yes pressed, close EasySave
                    System.Windows.Application.Current.Shutdown();
                }
            }
            else
            {
                if (MessageBox.Show("Do you really want to close EasySave ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // If Yes pressed, close EasySave
                    System.Windows.Application.Current.Shutdown();
                }
            }
        }

        private void ClientRemote_Click(object sender, RoutedEventArgs e)
        {
            StartServerSocketAsync();
            Process client = new Process();
            client.StartInfo.FileName = @"..\..\..\..\EasySave_Client\bin\Release\netcoreapp3.1\EasySave_Client.exe";
            client.Start();
            
        }

        private static async Task StartServerSocketAsync()
        {
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11_000);

            using Socket listener = new Socket(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            listener.Bind(ipEndPoint);
            listener.Listen(100);

            var handler = await listener.AcceptAsync();
            while (true)
            {
                // Receive message.
                var buffer = new byte[1_024];
                var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);

                var eom = "<|EOM|>";
                if (response.IndexOf(eom) > -1 /* is end of message */)
                {
                    Console.WriteLine(
                        $"Socket server received message: \"{response.Replace(eom, "")}\"");

                    var ackMessage = "<|ACK|>"; // "<|ACK|>"
                    var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
                    await handler.SendAsync(echoBytes, 0);
                    break;
                }
            }
        }
    }
}
