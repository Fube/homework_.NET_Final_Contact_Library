using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace ContactLibrary
{
    public sealed class CSVUtils
    {
        private CSVUtils(){}

        private static readonly Lazy<CSVUtils> LazyInstance = new Lazy<CSVUtils>(() => new CSVUtils());

        public static CSVUtils Instance => LazyInstance.Value;

        public List<Contact> ImportFromFile(Stream stream)
        {

            List<Contact> imported = new List<Contact>();
            using (var parser = new TextFieldParser(stream))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    string fName = fields[0];
                    string lName = fields[1];
                    string phoneNumber = fields.Length < 3 ? null : fields[2];

                    imported.Add(new Contact(fName, lName, phoneNumber));
                }
            }

            return imported;
        }

        public void ExportToFile(string path)
        {
            using (var sw = new StreamWriter(path))
            {
                foreach (var contact in ContactManager.Instance.Contacts)
                {
                    sw.WriteLine($"{contact.FirstName},{contact.LastName}{ ( string.IsNullOrEmpty(contact.PhoneNumber) ? "" : $",{contact.PhoneNumber}" )}");
                    sw.Flush();
                }
            }
        }
    }
}
