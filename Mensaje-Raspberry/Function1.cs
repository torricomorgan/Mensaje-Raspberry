using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Devices;
using System.Text;
using System.Threading.Tasks;

namespace Mensaje_Raspberry
{

    public static class Function1
    {
        static ServiceClient serviceClient;
        static string connectionString = "HostName=iotmatias.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=J1YWc06eK/7vsAuw1on2AyLm7KRBYt1XeE1W2TpmA2o=";
        static string targetDevice = "raspberryemumatias";

        [FunctionName("Function1")]
        public static async Task RunAsync([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            var commandMessage = new Message(Encoding.ASCII.GetBytes("Cloud to device message."));
            await serviceClient.SendAsync(targetDevice, commandMessage);

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
