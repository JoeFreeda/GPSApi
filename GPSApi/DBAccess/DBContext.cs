using GPSApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GPSApi.DBAccess
{
    public class DBContext
    {
        public static int SaveDetails(PlaceModel model)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GPSConnectionString"].ToString());
            string query = "INSERT INTO PlaceDetails(Place, Latitude, Longitude, Isdefault, NoofPlaces) values ('" + model.Place + "','" + model.Latitude + "','" + model.Longitude + "'," + model.Isdefault + "," + model.NoofPlaces + "); SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return i;
        }
        public static void SaveSubPlaceDetails(PlaceModel model, int PlaceId)
        {
            foreach (var val in model.SubPlace)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GPSConnectionString"].ToString());
                string query = "INSERT INTO SubPlace_Details(PlaceId, SubPlace, Latitude, Longitude,Image, MarkeePoint, Description) values (" + PlaceId + ",'" + val.SubPlace + "','" + val.Latitude + "','" + val.Longitude + "','" + val.Image + "'," + val.MarkeePoint + ",'" + val.Description + "'); SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();                
            }
        }

        public static List<PlaceModel> GetAll()
        {
            string queryString =
            "select * from PlaceDetails;";
            List<PlaceModel> lst = new List<PlaceModel>();
            PlaceModel ls = new PlaceModel();
            using (SqlConnection connection =
                       new SqlConnection(ConfigurationManager.ConnectionStrings["GPSConnectionString"].ToString()))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    ls = new PlaceModel();
                    ls.PlaceId = Convert.ToInt32(reader["PlaceId"]);
                    ls.Place = reader["Place"].ToString();
                    ls.Latitude = reader["Latitude"].ToString();
                    ls.Longitude = reader["Longitude"].ToString();
                    ls.Isdefault = Convert.ToInt32(reader["Isdefault"]);
                    ls.NoofPlaces = Convert.ToInt32(reader["NoofPlaces"]);
                    lst.Add(ls);
                }

                // Call Close when done reading.
                reader.Close();
            }
            return lst;
        }

        public static List<PlaceModel> GetByPlace(string Place)
        {
            string queryString =
            "select * from PlaceDetails where Place='"+ Place + "';";
            List<PlaceModel> lst = new List<PlaceModel>();
            PlaceModel ls = new PlaceModel();
            using (SqlConnection connection =
                       new SqlConnection(ConfigurationManager.ConnectionStrings["GPSConnectionString"].ToString()))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    ls = new PlaceModel();
                    ls.PlaceId = Convert.ToInt32(reader["PlaceId"]);
                    ls.Place = reader["Place"].ToString();
                    ls.Latitude = reader["Latitude"].ToString();
                    ls.Longitude = reader["Longitude"].ToString();
                    ls.Isdefault = Convert.ToInt32(reader["Isdefault"]);
                    ls.NoofPlaces = Convert.ToInt32(reader["NoofPlaces"]);
                    lst.Add(ls);
                }

                // Call Close when done reading.
                reader.Close();
            }
            return lst;
        }


        public static void GetSubPlace(List<PlaceModel> ObjPlace)
        {
            foreach(var val in ObjPlace)
            {
                val.SubPlace = new  List<SubPlaceModel>(); 
                string queryString =
            "select * from SubPlace_Details where PlaceId = "+val.PlaceId+";";
               
                using (SqlConnection connection =
                           new SqlConnection(ConfigurationManager.ConnectionStrings["GPSConnectionString"].ToString()))
                {
                    SqlCommand command =
                        new SqlCommand(queryString, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    SubPlaceModel SP = new SubPlaceModel();
                    // Call Read before accessing data.
                    while (reader.Read())
                    {
                        SP = new SubPlaceModel();
                        SP.PlaceId = Convert.ToInt32(reader["PlaceId"]);
                        SP.SubPlaceId = Convert.ToInt32(reader["SubPlaceId"]);
                        SP.SubPlace = reader["SubPlace"].ToString();
                        SP.Latitude = reader["Latitude"].ToString();
                        SP.Longitude = reader["Longitude"].ToString();
                        SP.Image = reader["Image"].ToString();
                        SP.MarkeePoint = Convert.ToInt32(reader["MarkeePoint"]);
                        SP.Description = reader["Description"].ToString();
                        val.SubPlace.Add(SP);
                    }

                    // Call Close when done reading.
                    reader.Close();
                }
            }
            
        }
    }
}