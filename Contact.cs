using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ContactLibrary.Annotations;

namespace ContactLibrary
{
    public sealed class Contact : INotifyPropertyChanged, ICloneable
    {

        private String _firstName, _lastName, _phoneNumber;
        private long? _ID;

        public string FirstName
        {
            get => _firstName;
            internal set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            internal set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            internal set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public long? ID
        {
            get => _ID;
            internal set
            {
                _ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Contact(string fName, string lName, string phone=null)
        {
            _ID = null;
            _firstName = fName;
            _lastName = lName;
            _phoneNumber = phone;
        }

        public Contact(long? id, string fName, string lName, string phone = null) : this(fName, lName, phone)
        {
            _ID = id;
        }



        public override string ToString() => $"ID: {ID}, Name: {FirstName} {LastName}\nPhone Number: {PhoneNumber}";
        public object Clone() => MemberwiseClone();

        public void Deconstruct(out long? id, out string fName, out string lName, out string phone)
        {
            fName = FirstName;
            lName = LastName;
            phone = PhoneNumber;
            id = ID;
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
