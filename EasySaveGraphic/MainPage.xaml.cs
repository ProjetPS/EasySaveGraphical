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
            Application.Current.MainWindow.Title = "EasySave - Menu principal";
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
            Application.Current.MainWindow.Title = "EasySave - Main menu";
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
            //Goto worksave creation page
            var window = (Mainwindow)Application.Current.MainWindow;

            Create goToCWS = new Create(isLangFR);

            // Change window title in appropriate language
            if (this.isLangFR)
            {
                window.Title = "EasySave - Créer un travail de sauvegarde";
            }
            else
            {
                window.Title = "EasySave - Create a new worksave";
            }
            window.Content = goToCWS;
        }

        private void ExecuteWS_Click(object sender, RoutedEventArgs e)
        {
            //Goto worksave execution page
            var window = (Mainwindow)Application.Current.MainWindow;

            Execute goToEWS = new Execute(isLangFR);

            // Change window title in appropriate language
            if (this.isLangFR)
            {
                window.Title = "EasySave - Executer un travail de sauvegarde";
            }
            else
            {
                window.Title = "EasySave - Execute a worksave";
            }
            window.Content = goToEWS;
        }
    }
}
