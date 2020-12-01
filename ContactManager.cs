using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace ContactLibrary
{
    public sealed class ContactManager
    {

        private readonly List<Contact> _contacts;

        private static readonly Lazy<ContactManager> LazyInstance = new Lazy<ContactManager>(() => new ContactManager());

        public static ContactManager Instance => LazyInstance.Value;

        private ContactManager()
        {
            _contacts = new List<Contact>();
        }

        public void AddContact(Contact contact)
        {
            _contacts.Add(contact);
        }

        public Contact FindById(long? id) => _contacts.First(n => n.ID == id);

        public void RemoveContact(long id) => _contacts.RemoveAll(n => n.ID == id);

        public void ImportFromFile(Stream stream)
        {
            using (var parser = new TextFieldParser(stream))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields != null)
                        AddContact(new Contact(fields[0], fields[2], fields.Length < 3 ? null : fields[3]));
                }
            }
        }

        public void ExportToFile(string path)
        {
            using (var sw = new StreamWriter(path))
            {
                foreach (var v in _contacts)
                {
                    sw.WriteLine($"{v.FirstName},{v.LastName},{ v.PhoneNumber ?? "null" }");
                    sw.Flush();
                }
            }
        }

    }
}
