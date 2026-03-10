using NexusContacts.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NexusContacts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static BaseDal dal = new BaseDal();
        readonly  public static List<City> allCities = dal.GetCityDataFromDataTableToList(dal.ExecuteSelectAllQuery("SELECT * FROM Cities"));

    }
}
