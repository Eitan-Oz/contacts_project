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
        private City _city;



        public int ContID { get; internal set; }

        public City City
        {
             get => _city;
            set { _city = value; OnPropertyChanged(); }
        }

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

        // הוספתי את cityId לרשימת הפרמטרים
        public Pepole(int ID, string Fname, string Lname, string phonNum, int cityId, byte? age, bool? favo,City city)
        {
            this.ContID = ID;
            this.FirstName = Fname;
            this.LastName = Lname;
            this.PhoneNumber = phonNum;
            this.Age = age;
            this.IsFavorite = favo;
            this._city = city;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}