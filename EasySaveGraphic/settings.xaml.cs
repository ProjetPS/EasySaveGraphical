using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace EasySaveGraphic
{
    /// <summary>
    /// Logique d'interaction pour settings.xaml
    /// </summary>
    public partial class settings : Page
    {
        private bool isLangFR = false;
        public settings(bool isFR)
        {
            InitializeComponent();
            if (isFR)
            {
                ChangetoFR();
                this.isLangFR = true;
            }
        }

        public static string path = @"C:/temp/settings.json";
        public static List<Config> configList = new List<Config>();

        public List<string> Extensions
        {
            get
            {
                List<string> boxesList = new List<string>();
                var children = LogicalTreeHelper.GetChildren(ListExt);
                //Loop through each child
                foreach (var item in children)
                {
                    var ext = item as CheckBox;
                    //Check if the CheckBox object is checked
                    if (ext.IsChecked == true)
                    {
                        boxesList.Add((string)ext.Content);
                    }
                }
                return boxesList;
            }
        }

        public string JobSoftware
        {
            get
            {
                string filepath = ShowDialog.Text;
                return filepath;
            }
        }


        public string LogType
        {
            get
            {
                string logType = "";
                var children = LogicalTreeHelper.GetChildren(TypeLog);
                foreach (var item in children)
                {
                    var type = item as ComboBoxItem;
                    if (type.IsSelected == true)
                    {
                        logType = (string)type.Content;
                    }
                }
                return logType;
            }
        }

        public void Button_Software(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
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
                ShowDialog.Text = filename;
            }
        }

        public void Button_Save(object sender, RoutedEventArgs e)
        {
            var settingSave = new Config()
            {
                LogType = LogType,
                Extensions = Extensions,
                JobSoftware = JobSoftware,
            };
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,  //Transform to UTF-8
            };
            string jsonString = JsonSerializer.Serialize(settingSave, options);
            //Create the file, or overwrite if the file exists.
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
                //Add some information to the file.
                fs.Write(info, 0, info.Length);
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

        private void ChangetoFR()
        {
            NameJS.Content = "Logiciel Métier :";
            NameLT.Content = "Type de Log :";
            SaveButton.Content = "Sauvegarder";
        }
    }
    public class Config
    {
        public string LogType { get; set; }
        public List<string> Extensions { get; set; }
        public string JobSoftware { get; set; }
    }
}