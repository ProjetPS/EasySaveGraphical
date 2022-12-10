﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace EasySaveGraphic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Create : Page // Create Page
    {

        //private bool isLangFR = false;
        public Create() //bool isFR
        {
            InitializeComponent();

           /* if (isFR)
            {
                ChangetoFR();
                this.isLangFR = true;
            }*/
        }

        private void CreateBackupJob(object sender, RoutedEventArgs e)
        {
            string name = backupJobName.Text;
            string fileSource = backupJobSource.Text;
            string fileTarget = backupJobTarget.Text;
            string type = backupJobType.SelectedItem.ToString();

            backupJob save = new backupJob(name, fileSource, fileTarget, type);
            backupJob.Open(backupJob.filePath);
            backupJob.backupList.Add(save);
            backupJob.Save(backupJob.backupList, backupJob.filePath);
        }


        private void Selected(object sender, RoutedEventArgs e)
        {

        }

        private void findSourceFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
                backupJobSource.Text = filename;
            }
        }

        private void findTargetFile(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
                backupJobTarget.Text = filename;
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
          /*  // Go back to main page
            var window = (Mainwindow)Application.Current.MainWindow;

            if (this.isLangFR)
            {
                //Go back in FR language
                MainPage goBack = new MainPage(true);
                window.Title = "EasySave - Menu principal";
                window.Content = goBack;

            }
            else
            {
                //Go back in EN language
                MainPage goBack = new MainPage();
                window.Title = "EasySave - Main menu";
                window.Content = goBack;
            }*/


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}