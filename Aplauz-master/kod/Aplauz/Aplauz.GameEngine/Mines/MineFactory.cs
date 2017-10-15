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

        public List<Mine> GetAllMines()
        {
            List<Mine> resultList = new List<Mine>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();

                        Mine mine = new Mine(Int32.Parse(fields[0]),fields[1], Int32.Parse(fields[2]), fields[3] + fields[4] + fields[5] + fields[6] + fields[7]);
                        resultList.Add(mine);
                    
                }
            }
            return resultList;
        }
    }
}
