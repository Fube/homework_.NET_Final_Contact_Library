using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactLibrary
{
    public sealed class Contact
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string PhoneNumber { get; }
        public long? ID { get; }

        public Contact(string fName, string lName, string phone=null)
        {
            ID = null;
            FirstName = fName;
            LastName = lName;
            PhoneNumber = phone;
        }

        public Contact(long? id, string fName, string lName, string phone = null) : this(fName, lName, phone)
        {
            ID = id;
        }



        public override string ToString() => $"ID: {ID}, Name: {FirstName} {LastName}\nPhone Number: {PhoneNumber}";

        public void Deconstruct(out long? id, out string fName, out string lName, out string phone)
        {
            fName = FirstName;
            lName = LastName;
            phone = PhoneNumber;
            id = ID;
        }
    }
}
