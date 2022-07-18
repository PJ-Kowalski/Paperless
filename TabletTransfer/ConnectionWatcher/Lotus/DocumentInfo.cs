using CommonDatabase.Data;
using Domino;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectionWatcher.Lotus
{
    public class DocumentInfo
    {
        public bool QSI_USER { get; set; }

        public string Username { get; set; }

        private NotesSession _ldSession;
        private NotesDatabase _ldDatabase;
        private NotesDocument _ldDocument;
        private string appDataFolder = "";

        bool QsiPrepareIni()
        {
            var filename = Path.Combine(appDataFolder, "notes.ini");

            if (!File.Exists(filename)) return false;

            string reader = "";
            using (StreamReader sr = new StreamReader(filename))
            {
                reader = sr.ReadToEnd();
                sr.Close();

                var tab = reader.Split('\n', '\r');
                var keyFile = tab.FirstOrDefault(t => t.StartsWith("KeyFileName="));

                var newKeyFile = "KeyFileName=" + Username;

                reader = reader.Replace(keyFile, newKeyFile);
            }

            using (StreamWriter sw = new StreamWriter(filename, false))
            {
                sw.WriteLine(reader);
                sw.Flush();
                sw.Close();
            }

            return true;
        }


        public bool CheckIfQsiProdUsereExist(string db_name, string password)
        {
            appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\\Local", @"IBM\\Notes", "Data");
            if (!Directory.Exists(appDataFolder))
            {
                appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\\Local", @"Lotus\\Notes", "Data");
                if (!Directory.Exists(appDataFolder)) return false;
            }
            string qsiprod_path = Path.Combine(appDataFolder, "qsiprod.id");
            if (File.Exists(qsiprod_path))
                return true;
            else
                return false;
        }

        public bool InitializeNotesSession(string server, string db_name, string password)
        {
            try
            {
                if (QSI_USER)
                {
                    QsiPrepareIni();
                    password = "1234";
                }
                _ldSession = new NotesSession();
                _ldSession.Initialize(password);
                Debug.WriteLine(_ldSession.UserName);
                _ldDatabase = _ldSession.GetDatabase(server, db_name, false);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        //get from lotus database
        public List<DocumentRevisionData> GetDocuments(string databaseName, string location) 
        {
            List<DocumentRevisionData> documents;
            try
            {
                if (_ldDatabase == null) return null;

                documents = new List<DocumentRevisionData>();

                var notesDocs = _ldDatabase.AllDocuments;
                var doc = notesDocs.GetFirstDocument();

                while (doc!=null)
                {
                    DocumentRevisionData document = new DocumentRevisionData
                    {

                        Location = location,
                        DocNum = doc.GetItemValue("DocNum")[0],
                        revisionValue = doc.GetItemValue("qsi_revision")[0].ToString()
                    };
                    if (document.DocNum!="")
                    {
                       documents.Add(document);
                    }
                    doc = notesDocs.GetNextDocument(doc);
                }


                return documents;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return null;

        }

    }
}