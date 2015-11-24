using IoTDemoApi.Common;
using System.Threading.Tasks;

namespace IoTDemoApi
{
    public static class Notifications
    {
        public static async Task<bool> SendLowTemperatureWarningSms(double temp)
        {
            await IfTttProxy.TriggerGarageGettingColdEvent(ConvertTemp.ConvertCelsiusToFahrenheit(temp));
            return false;
        }
    }
}