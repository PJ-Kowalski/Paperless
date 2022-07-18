//using QSIDocumentLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDatabase.Data;
using ConnectionWatcher.Lotus;

namespace ConnectionWatcher.Lotus
{
    public class LotusService 
    {
        private DocumentInfo info;
        private List<DocumentRevisionData> myDoc;

        List<(string,string)> DatabaseNames = new List<(string,string)> {("QSI\\QSIDDPF\\QS4RELDC.NSF","DDPF" ),
                                                      ( "QSI\\QSIDPF\\QS4RELDC.NSF","DPF" ),
                                                      ( "QSI\\QSICD\\QS4RELDC.NSF","CD" ),
                                                      ( "QSI\\QSIGEN\\QS4RELDC.NSF","GEN" ),
                                                      ( "QSI\\QSINOX\\QS4RELDC.NSF","NOX" ) };

        public LotusService()
        {

        }

        public List<DocumentRevisionData> UpdateDocumentRevision()
        {
            List<DocumentRevisionData> documents = new List<DocumentRevisionData>(); ;
            info = new DocumentInfo();
            if (info.CheckIfQsiProdUsereExist("qsiprod.id", "1234"))
            {
                info.Username = "qsiprod.id";
                info.QSI_USER = true;
                foreach (var item in DatabaseNames)
                {
                    info.InitializeNotesSession(@"acp001/acp", item.Item1, "1234");
                    if (GetFCDDocument(item.Item1, item.Item2)!=null)
                    {
                        documents.AddRange(GetFCDDocument(item.Item1, item.Item2));
                    }

                }
            }
            return documents;
        }

        public List<DocumentRevisionData> GetFCDDocument(string databaseName, string location)
        {
            myDoc = info.GetDocuments(databaseName, location);
            return myDoc;
        }



        


    }
}
