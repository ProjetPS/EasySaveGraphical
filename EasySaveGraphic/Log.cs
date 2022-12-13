using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;

namespace EasySaveGraphic
{
    class LogType
    {
        public static void JSONType(string name, string sourcePath, string targetPath, long transfertTime, int size)
        {
            var Log = new LogInfo()  //JSON informations
            {
                BackupName = name,
                SourceFilePath = sourcePath,
                TargetFilePath = targetPath,
                FileTransfertTime = transfertTime,
                FileSize = size,
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,  //Transform to UTF-8
            };
            string jsonString = JsonSerializer.Serialize(Log, options);
            string path = "C:/temp/log.json";

            try
            {
                if (!File.Exists(path))
                {
                    //Create the file, or overwrite if the file exists.
                    using (FileStream fs = File.Create(path))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
                        //Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                }

                //Add new JSON informations at the end of the file
                string jsonAddString = jsonString + Environment.NewLine;
                File.AppendAllText(path, jsonAddString + ",");

                //Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void XMLType(string name, string sourcePath, string targetPath, long transfertTime, int size)
        {
            var Log = new LogInfo()  //JSON informations
            {
                BackupName = name,
                SourceFilePath = sourcePath,
                TargetFilePath = targetPath,
                FileTransfertTime = transfertTime,
                FileSize = size,
            };
            XDocument document = new XDocument
    (
        new XDeclaration("1.0", "utf-8", "yes"),
        new XComment("XML for Log"),

        new XElement("log",
            new XElement("data",
                new XElement("backupname", Log.BackupName),
                new XElement("sourcefilepath", Log.SourceFilePath),
                new XElement("targetfilepath", Log.TargetFilePath),
                new XElement("destpath", Log.DestPath),
                new XElement("time", Log.Time),
                new XElement("filesize", Log.FileSize.ToString()),
                new XElement("transferttime", Log.FileTransfertTime.ToString())))
    );
            string path = "C:/temp/log.xml";
            document.Save(path);
        }

        public static void CallType(string name, string sourcePath, string targetPath, long transfertTime, int size)
        {
            string settingsFile = @"C:/temp/settings.json";
            string jsonString = File.ReadAllText(settingsFile);
            Config stateInfo = JsonSerializer.Deserialize<Config>(jsonString)!;
            string choiceType = stateInfo.LogType;
            if (choiceType == "JSON")
            {
                JSONType(name, sourcePath, targetPath, transfertTime, size);
            }
            else if (choiceType == "XML")
            {
                XMLType(name, sourcePath, targetPath, transfertTime, size);
            }
        }
    }

    public class LogInfo
    {
        public string BackupName { get; set; }
        public string SourceFilePath { get; set; }
        public string TargetFilePath { get; set; }
        public string DestPath = "";
        public string Time
        {
            get
            {
                DateTime date = DateTime.Now;   //Get the Date + Time with the format dd/MM/yyyy HH:mm:ss
                return date.ToString();
            }
        }
        public int FileSize { get; set; } //In octets

        public long FileTransfertTime { get; set; }
    }
}

