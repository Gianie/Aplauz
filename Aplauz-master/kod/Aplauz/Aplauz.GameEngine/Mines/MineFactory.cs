using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.VisualBasic.FileIO;

namespace Aplauz.GameEngine.Mines
{
    //TODO
    class MineFactory
    {
        private string filePath = ConfigurationManager.AppSettings["MinesFilePath"];

        public MineFactory()
        {
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        //TODO: Process field
                    }
                }
            }
        }

        //public List<Mine> GetAllMines()
        //{
        //    using (TextFieldParser parser = new TextFieldParser(filePath))
        //    {
        //        parser.TextFieldType = FieldType.Delimited;
        //        parser.SetDelimiters(";");
        //        while (!parser.EndOfData)
        //        {
        //            //Processing row
        //            string[] fields = parser.ReadFields();
        //            foreach (string field in fields)
        //            {
        //                //TODO: Process field
        //            }
        //        }
        //    }
        //}
    }
}
