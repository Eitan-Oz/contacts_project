using NexusContacts.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace NexusContacts
{
    public class BaseDal
    {
        protected string ConnectionString;
        protected SqlConnection conn;
        public BaseDal()
        {
            // נתיב מוחלט -לא מומלץ
            // ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\MyProject\\LayersExample2\\DAL\\Database1.mdf;Integrated Security=True";

            // נתיב יחסי למיקום קובץ הדטה בייס  
            string DBfileName = "DAL\\Database1.mdf";
            // צריך להוריד  2 תיקיות מהסוף
            // (bin → Debug ) שם ממוקם הדטה בייס ברירת מחדל
            string projectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
            ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                projectDir + DBfileName + ";Integrated Security=True";

            // יצירת האובייקט שמייצג את החיבור לדטה בייס
            conn = new SqlConnection(ConnectionString);
        }
        public List<Pepole> GetPepoleDataFromDataTableToList(DataTable dt)
        {
            List<Pepole> students = new List<Pepole>();

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    Pepole p = new Pepole();
                    p.ContID = row.Field<int>("ContID");
                    p.FirstName = row.Field<string>("FName");
                    p.LastName = row.Field<string>("LName");
                    p.Age = row.Field<byte>("Age");
                    p.PhoneNumber = row.Field<string>("PhoneNum");
                    p.IsFavorite = row.Field<bool?>("IsFavorite") ?? false;
                    p.City = row.Field<City>("CityID");//!!!!!
                    students.Add(p);
                }
                return students;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing contact data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine(ex);
                return students;
            }
        }
        public List<City> GetCityDataFromDataTableToList(DataTable dt)
        {
            List<City> cities = new List<City>();

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    City ci = new City();
                    ci.CityID = row.Field<int>("CityID");
                    ci.CityNameEn = row.Field<string>("city_name_en");
                    ci.CityNameHe = row.Field<string>("city_name_he");
                    cities.Add(ci);
                }
                return cities;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing contact data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine(ex);
                return cities;
            }
        }

        public int ExecuteInsertQuery(string sql)
        {
             return ExecuteQuery(sql);
        }
        public int ExecuteUpdateQuery(string sql)
        {
            return ExecuteQuery(sql);
        }
        public int ExecuteDeleteQuery(string sql)
        {
            return ExecuteQuery(sql);
        }

        /// <summary>
        /// מפעיל שאילתא שמחזירה טבלה
        /// למשל
        /// select * from Students where Gender='male'
        public DataTable ExecuteSelectAllQuery(string sql)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                HandleError(ex, sql);
                throw; // חשוב! לא לבלוע שגיאה
            }
            finally
            {
                conn.Close();
            }

            return table;
        }

        /// <summary>
        /// מפעיל שאילתה שמחזירה מחרוזת
        /// </summary>
        /// <param name="sql">The sql qurey to execute</param>        
        /// <returns>the scalar execution converted to string</returns>
        public string ExecuteSelectStringQuery(string sql)
        {
            object obj = ExecuteSelectOneData(sql);
            return Convert.ToString(obj);
        }

        /// <summary>
        /// מפעיל שאילתה שמחזירה ערך בוליאני
        /// </summary>
        /// <param name="sql">The sql qurey to execute</param>        
        /// <returns>the scalar execution converted to boolean</returns>
        public bool ExecuteSelectBoolQuery(string sql)
        {
            object obj = ExecuteSelectOneData(sql);
            return Convert.ToBoolean(obj);
        }

        /// <summary>
        /// מפעיל שאילתה שמחזירה ערך שלם
        /// </summary>
        /// <param name="sql">The sql qurey to execute</param>        
        /// <returns>the scalar execution converted to int</returns>
        public int ExecuteSelectIntQuery(string sql)
        {
            object obj = ExecuteSelectOneData(sql);
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// מפעיל שאילתה שמחזירה ערך יחיד
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private object ExecuteSelectOneData(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                HandleError(ex, sql);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// מפעיל שאילתא שלא מחזירה ערך (INSERT, UPDATE, DELETE)
        private int ExecuteQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HandleError(ex, sql);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public virtual void HandleError(Exception ex, string sql)
        {
            // כאן אפשר:
            // לכתוב לקובץ
            // לשמור ל-DB
            // לשלוח לוג

            Console.WriteLine("SQL Error:");
            Console.WriteLine(sql);
            Console.WriteLine(ex.Message);
        }
    }
}



