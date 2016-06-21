using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Horus.Models;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;

namespace Horus.Controllers
{
    public class NotificationController : Controller
    {
        private NotificationHubClient hub;

        public NotificationController()
        {
            hub = Notifications.Instance.Hub;
        }

        public class DeviceRegistration
        {
            public string Platform { get; set; }
            public string Handle { get; set; }
            public string[] Tags { get; set; }
        }
        
        public async Task<IActionResult> Post(string handle = null)
        {
            string newRegistrationId = null;
            
            if (handle != null)
            {
                var registrations = await hub.GetRegistrationsByChannelAsync(handle, 100);

                foreach (RegistrationDescription registration in registrations)
                {
                    if (newRegistrationId == null)
                    {
                        newRegistrationId = registration.RegistrationId;
                    }
                    else
                    {
                        await hub.DeleteRegistrationAsync(registration);
                    }
                }
            }

            if (newRegistrationId == null)
                newRegistrationId = await hub.CreateRegistrationIdAsync();

            return Json(new { NotificationId = newRegistrationId });
        }
        
        public async Task<IActionResult> Put(string id, DeviceRegistration deviceUpdate)
        {
            RegistrationDescription registration = null;
            switch (deviceUpdate.Platform)
            {
                case "mpns":
                    registration = new MpnsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "wns":
                    registration = new WindowsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "apns":
                    registration = new AppleRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "gcm":
                    registration = new GcmRegistrationDescription(deviceUpdate.Handle);
                    break;
                default:
                    throw new Exception("exception");
            }

            registration.RegistrationId = id;
            
            registration.Tags = new HashSet<string>(deviceUpdate.Tags);
            registration.Tags.Add(Request.Headers["AuthID"]);

            try
            {
                await hub.CreateOrUpdateRegistrationAsync(registration);
            }
            catch (MessagingException)
            {
                return Json(new { Status = "Error" });
            }

            return Json(new { Status = "OK" });
        }
        
        public async Task<IActionResult> Delete(string id)
        {
            await hub.DeleteRegistrationAsync(id);
            return Json(new { Status = "OK" });
        }

        public async Task<IActionResult> Send(string pns, [FromBody]string message, string to_tag)
        {

            Microsoft.Azure.NotificationHubs.NotificationOutcome outcome = null;

            var json = "{ \"data\" : {\"message\":\"\"Hey you are in a danger zone! Please take precautions...\"\"}\"}";
            outcome = await Notifications.Instance.Hub.SendGcmNativeNotificationAsync(json);

            if (outcome != null)
            {
                if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
                    (outcome.State == NotificationOutcomeState.Unknown)))
                {
                    return Json(new { StatusCode = "Error" });
                }

                return Json(new { StatusCode = "OK" });
            }

            return Json(new { StatusCode = "Error" });
        }
    }
}
