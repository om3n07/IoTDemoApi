namespace IoTDemoApi.DTO
{
    public class SparkEnvironmentDataRaw
    {
        public string DeviceId { get; set; }
        public string Location { get; set; }
        public double Celsius { get; set; }
        public double Fahrenheit { get; set; }
        public double RelativeHumidity { get; set; }
        public string Pressure { get; set; }
        public double Feet { get; set; }
        public double Meters { get; set; }
    }
}