using System;
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

        private bool isLangFR = false;
        public Create(bool isFR)
        {
            InitializeComponent();

            if (isFR)
            {
                ChangetoFR();
                this.isLangFR = true;
            }
        }

        private void ChangetoFR()
        {
            CreateTitle.Content = "Créer une sauvegarde";
            name.Content = "Nom : ";
            sourceFile.Content = "Fichier source : ";
            targetFile.Content = "Fichier cible : ";
            type.Content = "Type : ";
            CreateButton.Content = "Créer";
        }

        private void CreateBackupJob(object sender, RoutedEventArgs e)
        {
            string name = backupJobName.Text;
            string fileSource = backupJobSource.Text;
            string fileTarget = backupJobTarget.Text;
            string type = backupJobType.SelectedItem.ToString();

            if (name.Length > 0 && fileSource.Length > 0 && fileTarget.Length > 0 && type.Length > 0)
            {
                backupJob save = new backupJob(name, fileSource, fileTarget, type);
                backupJob.Open(backupJob.filePath);
                backupJob.backupList.Add(save);
                backupJob.Save(backupJob.backupList, backupJob.filePath);
            }
            else
            {
                if (isLangFR)
                {
                    MessageBox.Show("Il manque des informations à remplir.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Information Missing.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
