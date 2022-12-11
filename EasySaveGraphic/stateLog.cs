using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text;
using System.Xml.Linq;

namespace EasySaveGraphic
{
    public class LogType
    {
        public static void JSONType()
        {
            var stateLog = new StateInfo()  //JSON informations
            {
                NbFilesLeftToDo = 0,
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,  //Transform to UTF-8
            };
            string jsonString = JsonSerializer.Serialize(stateLog, options);
            string path = "C:/temp/statelog.json";

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

        public static void XMLType()
        {
            var stateLog = new StateInfo()  //JSON informations
            {
                NbFilesLeftToDo = 0,
            };
            XDocument document = new XDocument
    (
        new XDeclaration("1.0", "utf-8", "yes"),
        new XComment("XML for stateLog"),

        new XElement("statelog",
            new XElement("data",
                new XElement("backupname", stateLog.BackupName),
                new XElement("sourcefilepath", stateLog.SourceFilePath),
                new XElement("targetfilepath", stateLog.TargetFilePath),
                new XElement("time", stateLog.Time),
                new XElement("state", stateLog.State),
                new XElement("totalfilestocopy", stateLog.TotalFilesToCopy.ToString()),
                new XElement("totalfilesize", stateLog.TotalFileSize.ToString()),
                new XElement("nbfileslefttodo", stateLog.NbFilesLeftToDo.ToString()),
                new XElement("progression", stateLog.Progression.ToString())))
    );
            string path = "C:/temp/statelog.xml";
            document.Save(path);
        }

        public static void CallType()
        {
            string settingsFile = @"C:/temp/settings.json";
            string jsonString = File.ReadAllText(settingsFile);
            Config stateInfo = JsonSerializer.Deserialize<Config>(jsonString)!;
            string choiceType = stateInfo.LogType;
            if (choiceType == "JSON")
            {
                JSONType();
            }
            else if (choiceType == "XML")
            {
                XMLType();
            }
        }

    }
    public class StateInfo
    {
        public string BackupName => backupJob.backupList[backupJob.Index].name;
        public string SourceFilePath => backupJob.backupList[backupJob.Index].fileSource;
        public string TargetFilePath => backupJob.backupList[backupJob.Index].fileTarget;
        public string Time
        {
            get
            {
                DateTime date = DateTime.Now;   //Get the Date + Time with the format dd/MM/yyyy HH:mm:ss
                return date.ToString();
            }
        }
        public string State => NbFilesLeftToDo == 0 ? "END" : "ACTIVE";   //Show END if no files are moved, else show ACTIVE
        public int TotalFilesToCopy
        {
            get
            {
                try
                {
                    int Size = Directory.GetFiles(SourceFilePath).Length;   //Count number of files in a directory
                    return Size;
                }
                catch
                {
                    return 1;   //Return 1 if a file is moved, and not a directory
                }
            }
        }
        public int TotalFileSize  //In octets
        {
            get
            {
                try
                {
                    int Size = (int)new FileInfo(SourceFilePath).Length;  //Return Size of the file
                    return Size;
                }
                catch
                {
                    return 0;   //Can't return an other size than a file
                }
            }
        }
        public int NbFilesLeftToDo { get; set; }
        public double Progression => (double)Decimal.Divide(NbFilesLeftToDo, TotalFilesToCopy); //Tends from 1 to 0, 0 corresponds to finished processing
    }
}
