using System;
using System.ComponentModel;
using System.Runtime.CompilerServices; // נדרש עבור [CallerMemberName]

// איתן עוז בכר י"א 4
namespace NexusContacts.models
{
    internal class Pepole : INotifyPropertyChanged
    {
        // שדות פרטיים כדי שנוכל להפעיל את הודעת השינוי למסך
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private byte? _age;
        private bool? _isFavorite;

        // הוספתי internal set כדי שתוכל למלא את ה-ID כשאתה מושך נתונים מה-SQL
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

        // כאן ה-internal set מאפשר לקוד ה-SQL שלך להכניס את המידע
        // public byte[] ImageRaw { get; internal set; }
        public bool? IsFavorite
        {
            get => _isFavorite;
            internal set { _isFavorite = value; OnPropertyChanged(); }
        }

        // הוספנו סימני שאלה לפרמטרים age ו-favo כדי שיתאימו ל-SQL
        public Pepole(int ID, string Fname, string Lname, string phonNum, byte? age, bool? favo/*, byte[] getimag*/)
        {
            this.ContID = ID;
            this.FirstName = Fname;
            this.LastName = Lname;
            this.PhoneNumber = phonNum;
            this.Age = age;
            this.IsFavorite = favo;
            //  this.ImageRaw = getimag;
        }

        // --- המימוש המתוקן ל-PropertyChanged (במקום ה-throw) ---
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}