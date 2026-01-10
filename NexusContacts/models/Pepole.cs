using System.ComponentModel;

namespace NexusContacts.models
{
    internal class Pepole: INotifyPropertyChanged
    {
        // הוספתי internal set כדי שתוכל למלא את ה-ID כשאתה מושך נתונים מה-SQL
        public int ContID { get; internal set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public byte? Age { get; set; }

        // כאן ה-internal set מאפשר לקוד ה-SQL שלך להכניס את המידע
        public byte[] ImageRaw { get; internal set; }
        public bool? IsFavorite { get; internal set; }

        public Pepole(int ID, string Fname, string Lname, string phonNum,byte age,bool favo, byte[] getimag)
        {
            this.ContID = ID;
            this.FirstName = Fname;

        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                throw new System.NotImplementedException();
            }

            remove
            {
                throw new System.NotImplementedException();
            }
        }
    }
}