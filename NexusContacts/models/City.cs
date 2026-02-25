using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusContacts.models
{
    public class City
    {
        private int _cityID;
        private string _cityNameEn;
        private string _cityNameHe;


        public City()
        {

        }
        
        public City(int cityID, string cityNameEn, string cityNameHe)
        {
            _cityID = cityID;
            _cityNameEn = cityNameEn;
            _cityNameHe = cityNameHe;
        }
        public int CityID
        {
            get => _cityID;
            set { _cityID = value; }
        }

        public string CityNameEn
        {
            get => _cityNameEn;
            set { _cityNameEn = value; }
        }

        public string CityNameHe
        {
            get => _cityNameHe;
            set { _cityNameHe = value;  }
        }
    }
}
