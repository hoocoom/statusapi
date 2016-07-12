using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Status.DemoApiClient.StatusService.Contracts;

namespace Status.DemoApiClient.StatusService.Implementation
{
    public class DefaultStatusService : IStatusService
    {
        private readonly string _apiUrl;

        public DefaultStatusService(string apiUrl)
        {
            _apiUrl = apiUrl;
        }
        
        public ServiceStatus GetStatus()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var responseString = client.GetStringAsync(_apiUrl).Result;
                    var response = JsonConvert.DeserializeObject<ServiceStatus>(responseString);

                    return response;
                }
                catch (WebException e)
                {
                    //если не получилось запросить статус, считаем что на сервисе проводятся работы
                    return new ServiceStatus() {State = State.Maintenance};
                }
            }
        }
    }
}