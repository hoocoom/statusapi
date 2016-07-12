using System;
using System.Text;
using Status.DemoApiClient.StatusService.Contracts;
using Status.DemoApiClient.StatusService.Implementation;

namespace Status.DemoApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new DefaultStatusService(Properties.Settings.Default.StatusApiUrl);
            Console.WriteLine("Press enter to get service status");
            Console.ReadLine();
            while (true)
            {
                GetStatus(service);
                Console.WriteLine("Press 'Esc' key for exit, other for getting status again");

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                    break;
            }
        }

        private static void GetStatus(IStatusService service)
        {
            var status = service.GetStatus();
            var result = new StringBuilder("Current status is {0}");
            if (status.MaintenancePlannedDate.HasValue)
                result.AppendLine(", maintenance planned for {1}");

            Console.WriteLine(result.ToString(), status.State.ToString("G"), status.MaintenancePlannedDate);
        }
    }
}
