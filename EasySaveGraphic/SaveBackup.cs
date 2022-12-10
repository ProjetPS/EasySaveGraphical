using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// ------------------------------ SaveBackup ------------------------------ ///
namespace EasySaveGraphic
{
    [System.Serializable] //Allow to serialyze an object 
    public class backupJob
    {
        /// --------------- Attributes --------------- ///
        public string name;
        public string fileSource;
        public string fileTarget;
        public string type;
        public backupJob(string name, string fileSource, string fileTarget, string type) //Constructor
        {
            this.name = name;
            this.fileSource = fileSource;
            this.fileTarget = fileTarget;
            this.type = type;
        }

        public static List<backupJob> backupList = new List<backupJob>(); //List of backups
        public static string directoryPath = @"C:\temp";
        public static string filePath = @"C:\temp\backupJobs.txt"; //Path of the list save file, if EasySave restarted, backup jobs are still usable.
        public static int Index;

        /// --------------- Methods --------------- ///
        public static void Save(List<backupJob> backup, string filePath) //Allow to save the list
        {

            //Create & open the directory
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            //Create & open the file
            FileStream fs = File.Create(filePath);
            //Serialyze object
            BinaryFormatter bf = new BinaryFormatter();
            //Push the serialyzed object in the file
            bf.Serialize(fs, backup);
            //Close file
            fs.Close();
        }

        public static List<backupJob> Open(string filePath) //Allow to open the list
        {
            //If file exist
            if (File.Exists(filePath))
            {
                FileStream fs = null;

                //If errors occurs
                try
                {
                    //Open file
                    fs = File.OpenRead(filePath);
                    //Unserialyze object
                    BinaryFormatter bf = new BinaryFormatter();
                    //Recover object
                    backupList = (List<backupJob>)bf.Deserialize(fs);
                }
                catch (Exception e)
                {
                    //Print error message
                    throw e;
                }
                finally
                {
                    //Close file
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }

            //Return our list
            return backupJob.backupList;
        }
        public static void MoveFileDirectory(string sourceFile, string targetFile, string saveType) //Method that move a file/directory to the right place
        {
            if (saveType == "System.Windows.Controls.ComboBoxItem : File" || saveType == "System.Windows.Controls.ComboBoxItem : Fichier") //If user wants to move a file
            {
                System.IO.File.Move(sourceFile, targetFile);
            }
            else if (saveType == "System.Windows.Controls.ComboBoxItem : Directory" || saveType == "System.Windows.Controls.ComboBoxItem : Répertoire") //If user wants to move a directory
            {
                System.IO.Directory.Move(sourceFile, targetFile);
            }
        }
    }
}