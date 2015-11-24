using System;
using System.Web.Http;
using System.Net.Http;
using IoTDemoApi.DataAccess;
using System.Net;
using IoTDemoApi.DTO;
using IoTDemoApi.Models;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using IoTDemoApi.IFTTT;
using System.Linq;

namespace IoTDemoApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class SparkEnvironmentDataController : ApiController
    {
        private SparkEnvironmentDbContext _sparkEnvironmentDbContext = new SparkEnvironmentDbContext();

        public SparkEnvironmentDataController()
        {
        }

        [HttpGet]
        [Route("environmentData/{numResults}")]
        public HttpResponseMessage SaveSparkEnvironmentData([FromUri] int numResults)
        {
            var envData = _sparkEnvironmentDbContext.EnvironmentData.AsEnumerable().OrderByDescending(e => e.DateCreated).Take(numResults);
            return Request.CreateResponse(HttpStatusCode.OK, envData);
        }

        //[HttpGet]
        //[Route("test/{temp}")]
        //public async Task<HttpResponseMessage> Test([FromUri]double temp)
        //{
        //    var response = await IfTttProxy.TriggerGarageGettingColdEvent(temp);
        //    return Request.CreateResponse(HttpStatusCode.OK, response);
        //}

        [HttpGet]
        [Route("environmentData")]
        public HttpResponseMessage SaveSparkEnvironmentData()
        {
            var envData = _sparkEnvironmentDbContext.EnvironmentData;
            return Request.CreateResponse(HttpStatusCode.OK, envData);
        }

        [HttpGet]
        [Route("IfTttEvents")]
        public HttpResponseMessage GetIfTttEvents()
        {
            var envData = _sparkEnvironmentDbContext.IfTttEvents;
            return Request.CreateResponse(HttpStatusCode.OK, envData);
        }

        [HttpPost]
        [Route("environmentData")]
        public async Task<HttpResponseMessage> SaveSparkEnvironmentData([FromBody] SparkEnvironmentDataRaw sparkEnvDataRaw)
        {
            var savedData = _sparkEnvironmentDbContext.EnvironmentData.Add(SparkEnvironmentDataMapper.Map(sparkEnvDataRaw));
            var lastWarningEvent = _sparkEnvironmentDbContext.IfTttEvents.AsEnumerable().LastOrDefault();
            if ((lastWarningEvent == null || lastWarningEvent.DateCreated.Date <= DateTime.Today) && sparkEnvDataRaw.Celsius <= 7)
            {
                await Notifications.SendLowTemperatureWarningSms(sparkEnvDataRaw.Celsius);
                _sparkEnvironmentDbContext.IfTttEvents.Add(new IfTttEventTriggered { DateCreated = DateTime.Now, EventTriggered = Enum.GetName(typeof(IfTttEventTypes), IfTttEventTypes.garage_getting_cold) });
            }

            _sparkEnvironmentDbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, savedData.Id);
        }
    }

    public static class SparkEnvironmentDataMapper
    {
        public static SparkEnvironmentData Map(SparkEnvironmentDataRaw sparkEnvDataRaw)
        {
            double outDouble = 0;
            double.TryParse(sparkEnvDataRaw.Pressure, out outDouble);

            var sparkEnvinronmentData = new SparkEnvironmentData
            {
                DeviceId = sparkEnvDataRaw.DeviceId,
                LocationName = sparkEnvDataRaw.Location,
                ElevationMeters = sparkEnvDataRaw.Meters,
                Pressure = outDouble,
                TempC = sparkEnvDataRaw.Celsius,
                RelativeHumidity = sparkEnvDataRaw.RelativeHumidity,
                DateCreated = DateTime.Now
            };

            return sparkEnvinronmentData;
        }
    }
}