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
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class WorkSaveList : Page
    {
        private bool isLangFR = false;
        public WorkSaveList(bool isFR)
        {
            InitializeComponent();

            // If language selected on main page is FR...
            if (isFR)
            {
                ChangetoFR();
                this.isLangFR = true;
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            // Go back to main page
            var window = (Mainwindow)Application.Current.MainWindow;

            if (this.isLangFR)
            {
                //Go back in FR language
                MainPage goBack = new MainPage(true);
                window.Title = "EasySave - Menu principal";
                window.Content = goBack;
                
            }else
            {
                //Go back in EN language
                MainPage goBack = new MainPage();
                window.Title = "EasySave - Main menu";
                window.Content = goBack;
            }
            
           
        }

        //Change all content displayed in FR
        private void ChangetoFR()
        {
            goback_btn.Content = "Retour";
        }
    }
}
