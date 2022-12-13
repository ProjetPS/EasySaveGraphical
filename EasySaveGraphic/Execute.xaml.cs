using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.IO;

namespace EasySaveGraphic
{
    /// <summary>
    /// Logique d'interaction pour Execute.xaml
    /// </summary>
    public partial class Execute : Page
    {

        public static string CellValue; // Recover datagridValue

        private bool isLangFR = false;

        BackgroundWorker worker = new BackgroundWorker();


        public Execute(bool isFR)
        {
            InitializeComponent();

            //Progress Bar tools
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.DoWork += ExecuteSave; ;

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

        private void BackupGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Recover Rows indexes selected
            DataGrid dataGrid = sender as DataGrid;
            backupJob.backupIndex.Add(dataGrid.SelectedIndex);
        }
        private void ChangetoFR() // VF
        {
            ExecuteTitle.Content = "Executer une sauvegarde";
            ExecuteButton.Content = "Executer";
        }

        private void ExecuteSave(object sender, DoWorkEventArgs e)
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
                var watch = new Stopwatch();
                Thread[] moves = new Thread[backupJob.backupIndex.Count];
                for (int i = 0; i < moves.Length; i++)
                {
                    int Index = backupJob.backupIndex[i];
                    string name = backupJob.backupList[Index].name;
                    string sourceFile = backupJob.backupList[Index].fileSource;
                    string targetFile = backupJob.backupList[Index].fileTarget;
                    string saveType = backupJob.backupList[Index].type;
                    int size = (int)new FileInfo(sourceFile).Length;

                    //backupJob.MoveFileDirectory(sourceFile, targetFile, saveType);

                    Thread move = new Thread(new ThreadStart(() => MoveFileDirectory(sourceFile, targetFile, saveType)));
                    move.Name = i.ToString();
                    watch.Start();
                    move.Start();
                    LogType.CallType(name, sourceFile, targetFile, watch.ElapsedMilliseconds, size);
                    watch.Reset();
                    StateLogType.CallType(name, sourceFile, targetFile, size);
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
                window.Title = "EasySave - Main menu";
                window.Content = goBack;
            }


        }

        private void RunProgressbar(object sender, RoutedEventArgs e)
        {

            worker.RunWorkerAsync();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Init progressBar
            progressBar.Value = e.ProgressPercentage;

            //Display progression
            percentage.Content = progressBar.Value.ToString() + "%";

        }
        public void DataGrid_Loaded(object sender, RoutedEventArgs e) //Display saves into datagrid
        {
            //Print the backupJob List
            backupJob.Open(backupJob.filePath);

            backCollection = new ObservableCollection<Backup> { }; //Filled collection with list elements
            for (int i = 0; i < backupJob.backupList.Count; i++)
            {
                backCollection.Add(new Backup { BackupName = backupJob.backupList[i].name, BackupSource = backupJob.backupList[i].fileSource, BackupTarget = backupJob.backupList[i].fileTarget, BackupType = backupJob.backupList[i].type });
            }

            DataContext =
                (from i in backCollection
                 select new { i.BackupName, i.BackupSource, i.BackupTarget, i.BackupType }).ToList();

        }


        public void MoveFileDirectory(string sourceFile, string targetFile, string saveType) //Method that move a file/directory to the right place
        {
            if (saveType == "System.Windows.Controls.ComboBoxItem : File" || saveType == "System.Windows.Controls.ComboBoxItem : Fichier") //If user wants to move a file
            {
                FileStream fsout = new FileStream(targetFile, FileMode.Create);
                FileStream fsin = new FileStream(sourceFile, FileMode.Open);
                byte[] bt = new byte[1048756];
                int readByte;

                while ((readByte = fsin.Read(bt, 0, bt.Length)) > 0)  //Read progression
                {
                    fsout.Write(bt, 0, readByte);
                    worker.ReportProgress((int)(fsin.Position * fsin.Length));
                }
                fsin.Close();
                File.Delete(sourceFile); //Delete source file
                fsout.Close();
            }
            else if (saveType == "System.Windows.Controls.ComboBoxItem : Directory" || saveType == "System.Windows.Controls.ComboBoxItem : Répertoire") //If user wants to move a directory
            {
                System.IO.Directory.Move(sourceFile, targetFile);
            }
        }


    }
}
