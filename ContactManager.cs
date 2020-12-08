using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace ContactLibrary
{
    public sealed class ContactManager
    {
        private static readonly Lazy<ContactManager> LazyInstance = new Lazy<ContactManager>(() => new ContactManager());

        public static ContactManager Instance => LazyInstance.Value;

        public ObservableCollection<Contact> Contacts { get; }

        private ContactManager()
        {
            Contacts = new ObservableCollection<Contact>();
        }

        public void AddContact(Contact contact)
        {
            Contacts.Add(contact);
        }

        public Contact FindById(long? id) => Contacts.FirstOrDefault(n => n.ID == id);

        public void RemoveContact(long id) => Contacts.Remove(FindById(id));

        public void UpdateContact(Contact contact)
        {
            Contact toUpdate = FindById(contact.ID);
            toUpdate.FirstName = contact.FirstName;
            toUpdate.LastName = contact.LastName;
            toUpdate.PhoneNumber = contact.PhoneNumber;
        }

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
                foreach (var v in Contacts)
                {
                    sw.WriteLine($"{v.FirstName},{v.LastName},{ v.PhoneNumber ?? "null" }");
                    sw.Flush();
                }
            }
        }

    }
}
