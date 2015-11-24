using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTDemoApi.Models
{
    public class SparkEnvironmentData
    {

        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string LocationName { get; set; }
        public double ElevationMeters { get; set; }
        public double TempC { get; set; }
        public double RelativeHumidity { get; set; }
        public double Pressure { get; set; }
        public double Meters { get; set; }
        public DateTime DateCreated { get; set; }
    }

}