using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

namespace Horus.Models
{
    public class Notifications
    {
        public static Notifications Instance = new Notifications();

        public NotificationHubClient Hub { get; set; }

        private Notifications()
        {
            Hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://hacknamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=5e88sfkneySins17GQvQqEf35hp0nGH0acCaAGSy+2o=", "hack");
        }
    }
}
