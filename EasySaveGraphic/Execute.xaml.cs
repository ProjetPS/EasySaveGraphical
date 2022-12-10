﻿using System;
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
using EasySaveGraphic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;

namespace EasySaveGraphic
{
    /// <summary>
    /// Logique d'interaction pour Execute.xaml
    /// </summary>
    public partial class Execute : Page
    {

        public static string CellValue;

        //private bool isLangFR = false;
        public Execute() //bool isFR
        {
            InitializeComponent();

            /* if (isFR)
             {
                 ChangetoFR();
                 this.isLangFR = true;
             }*/
        }
        public class Backup
        {
            public string BackupName { get; set; }
            public string BackupSource { get; set; }
            public string BackupTarget { get; set; }
            public string BackupType { get; set; }
        }

        public static ObservableCollection<Backup> backCollection;

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //Print the backupJob List
            backupJob.Open(backupJob.filePath);
            //Console.WriteLine(i + " - " + backupJob.backupList[i].name);

            backCollection = new ObservableCollection<Backup> { };
            for (int i = 0; i < backupJob.backupList.Count; i++)
            {
                backCollection.Add(new Backup { BackupName = backupJob.backupList[i].name, BackupSource = backupJob.backupList[i].fileSource, BackupTarget = backupJob.backupList[i].fileTarget, BackupType = backupJob.backupList[i].type });
            }



            DataContext =
                (from i in backCollection
                 select new { i.BackupName, i.BackupSource, i.BackupTarget, i.BackupType }).ToList();

        }

        private void SequentialExec_Click(object sender, RoutedEventArgs e)
        {
            bool canExecute = false;
            Process[] processes = Process.GetProcessesByName("notepad");

            if(processes.Length == 0)
            {
                canExecute = true;
            }
            else if (processes.Length == 1)
            {
                canExecute = false;
            }


            if (canExecute == true)
            {
                string sourceFile = backupJob.backupList[backupJob.Index].fileSource;
                string targetFile = backupJob.backupList[backupJob.Index].fileTarget;
                string saveType = backupJob.backupList[backupJob.Index].type;
                backupJob.MoveFileDirectory(sourceFile, targetFile, saveType);
            }

        }


        private void GoBack(object sender, RoutedEventArgs e)
        {
            // Go back to main page
            /*       var window = (Mainwindow)Application.Current.MainWindow;*/

            /*  if (this.isLangFR)
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

        private void backupGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            CellValue = RowColumn.Content.ToString();
            backupJob.Index = dataGrid.SelectedIndex;

        }
    }
}
