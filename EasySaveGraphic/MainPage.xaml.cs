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
        private readonly double opacity = 0.2;
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
            EN_btn.Opacity = opacity;
            FR_btn.Opacity = 1;
            this.isLangFR = false;
        }

        private void WorkSaveList(object sender, RoutedEventArgs e)
        {
            //Go to worksave list page
            var window = (Mainwindow)Application.Current.MainWindow;

            WorkSaveList goToWSL = new WorkSaveList(isLangFR);
            window.Content = goToWSL;
        }

        private void CreateWorkSave(object sender, RoutedEventArgs e)
        {
            //TODO: Goto worksave creation page
        }

        private void ExecuteWS_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Goto worksave execution page
        }
    }
}
