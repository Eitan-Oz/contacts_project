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

        public string CityName
        {
             get => _city?.CityNameHe;
            set { _city = App.allCities.Find(c => c.CityNameHe == value); OnPropertyChanged(); }
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
            set { _isFavorite = value; OnPropertyChanged(); }
        }

        public Pepole()
        {

        }

        // הוספתי את cityId לרשימת הפרמטרים
        public Pepole(int ID, string Fname, string Lname, string phonNum, int cityId, byte? age, bool? favo)
        {
            this.ContID = ID;
            this._firstName = Fname;
            this._lastName = Lname;
            this._phoneNumber = phonNum;
            this._age = age;
            this._isFavorite = favo;
            this._city = App.allCities.Find(c => c.CityID == cityId); 
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}