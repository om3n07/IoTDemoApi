using IoTDemoApi.IFTTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace IoTDemoApi
{
    public static class IfTttProxy
    {
        public static async Task<string> TriggerGarageGettingColdEvent(double temperatureCelcius)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"value1", temperatureCelcius.ToString() }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://maker.ifttt.com/trigger/" + Enum.GetName(typeof(IfTttEventTypes), IfTttEventTypes.garage_getting_cold) + "/with/key/dqUsqZ-yWQy0JJ6ONtpfBAi8zlQft8b8B54-bUZf-fZ", content);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}