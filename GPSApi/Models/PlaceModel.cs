using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPSApi.Models
{
    public class PlaceModel
    {
        public int PlaceId { get; set; }
        public string Place { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Isdefault { get; set; }
        public int NoofPlaces { get; set; }

       public List<SubPlaceModel> SubPlace { get; set; }
    }
    public class SubPlaceModel
    {
        public int PlaceId { get; set; }
        public int SubPlaceId { get; set; }
        public string SubPlace { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Image { get; set; }
        public int MarkeePoint { get; set; }
        public string Description { get; set; }
    }
}