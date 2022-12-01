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
    /// Logique d'interaction pour Mainwindow.xaml
    /// </summary>
    public partial class Mainwindow : Window
    {
        public Mainwindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FR_Lang(object sender, MouseButtonEventArgs e)
        {
            saveList_btn.Content = "Mes travaux de sauvegarde";
            createWS_btn.Content = "Créer un nouveau travail de sauvegarde";
            executeWS_btn.Content = "Éxecuter un travail de sauvegarde";
        }

        private void EN_Lang(object sender, MouseButtonEventArgs e)
        {
            saveList_btn.Content = "My worksave list";
            createWS_btn.Content = "Create a new worksave";
            executeWS_btn.Content = "Execute a worksave";
        }

        private void WorkSaveList(object sender, RoutedEventArgs e)
        {

        }

        private void CreateWorkSave(object sender, RoutedEventArgs e)
        {

        }

        private void ExecuteWS_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
