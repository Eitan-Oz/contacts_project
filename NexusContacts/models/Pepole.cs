using System;
using System.ComponentModel;
using System.Runtime.CompilerServices; 

namespace NexusContacts.models
{
    public class Pepole : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private byte? _age;
        private bool? _isFavorite;

        public int ContID { get; internal set; }

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        public byte? Age
        {
            get => _age;
            set { _age = value; OnPropertyChanged(); }
        }

        public bool? IsFavorite
        {
            get => _isFavorite;
            internal set { _isFavorite = value; OnPropertyChanged(); }
        }

        public Pepole()
        {

        }

        public Pepole(int ID, string Fname, string Lname, string phonNum, byte? age, bool? favo/*, byte[] getimag*/)
        {
            this.ContID = ID;
            this.FirstName = Fname;
            this.LastName = Lname;
            this.PhoneNumber = phonNum;
            this.Age = age;
            this.IsFavorite = favo;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}