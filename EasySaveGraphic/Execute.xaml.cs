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
using EasySaveGraphic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace EasySaveGraphic
{
    /// <summary>
    /// Logique d'interaction pour Execute.xaml
    /// </summary>
    public partial class Execute : Page
    {

        public static string CellValue; // Recover datagridValue
        private bool isLangFR = false;

        public Execute(bool isFR) 
        {
            InitializeComponent();

             if (isFR) // VF
             {
                 ChangetoFR();
                 this.isLangFR = true;
             }
        }
        public class Backup // Backups list to display
        {
            public string BackupName { get; set; }
            public string BackupSource { get; set; }
            public string BackupTarget { get; set; }
            public string BackupType { get; set; }
        }

        public static ObservableCollection<Backup> backCollection; //Backup Collection

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)  //Display saves into datagrid
        {
            //Print the backupJob List
            backupJob.Open(backupJob.filePath);
            //Console.WriteLine(i + " - " + backupJob.backupList[i].name);

            backCollection = new ObservableCollection<Backup> { }; //Filled collection with list elements
            for (int i = 0; i < backupJob.backupList.Count; i++)
            {
                backCollection.Add(new Backup { BackupName = backupJob.backupList[i].name, BackupSource = backupJob.backupList[i].fileSource, BackupTarget = backupJob.backupList[i].fileTarget, BackupType = backupJob.backupList[i].type });
            }



            DataContext =
                (from i in backCollection
                 select new { i.BackupName, i.BackupSource, i.BackupTarget, i.BackupType }).ToList();

        }

        private void ExecuteSave(object sender, RoutedEventArgs e)
        {
            bool canExecute = false;
            Process[] processes = Process.GetProcessesByName("notepad"); // Is jobSoftware open ?

            if (processes.Length == 0)
            {
                canExecute = true;
            }
            else if (processes.Length == 1)
            {
                canExecute = false;
            }

            // If jobSoftware close, then we can execute our save
            if (canExecute == true)
            {
                Thread[] moves = new Thread[backupJob.backupIndex.Count];
                for (int i = 0; i < moves.Length; i++)
                {
                    string sourceFile = backupJob.backupList[backupJob.Index].fileSource;
                    string targetFile = backupJob.backupList[backupJob.Index].fileTarget;
                    string saveType = backupJob.backupList[backupJob.Index].type;
                    LogType.CallType();
                    StateLogType.CallType();
                    //backupJob.MoveFileDirectory(sourceFile, targetFile, saveType);

                    Thread move = new Thread(new ThreadStart(() => backupJob.MoveFileDirectory(sourceFile, targetFile, saveType)));
                    move.Name = i.ToString();
                    move.Start();
                    //Thread.Sleep(6000);
                }
                backupJob.backupIndex.Clear(); //Clear the selected rows array at the end
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

              }
              else
              {
                  //Go back in EN language
                  MainPage goBack = new MainPage();
                  window.Title = "EasySave - Main Menu";
                  window.Content = goBack;
              }

        }

        private void BackupGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Recover Rows indexes selected
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            CellValue = RowColumn.Content.ToString();
            backupJob.Index = dataGrid.SelectedIndex;

        }

        private void ChangetoFR() // VF
        {
            ExecuteTitle.Content = "Executer un travail de sauvegarde";
            ExecuteButton.Content = "Executer";
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //initialisation de la barre de progression avec le pourcentage de progression
            progressBar.Value = e.ProgressPercentage;

            //Affichage de la progression sur un label
            percentage.Content = progressBar.Value.ToString() + "%";



        }
    }
}
