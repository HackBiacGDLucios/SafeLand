using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Horus.Repositories.Implementation;
using Horus.Repositories.Interface;
using Horus.ViewModels.Alerts;
using Horus.Models;

namespace Horus.Controllers
{
    public class AlertController : Controller
    {
        private readonly IAlertRepository _alertMethod;

        public AlertController(
            IAlertRepository alertMethod)
        {
            _alertMethod = alertMethod;
        }

        [HttpPost]
        public async Task<IActionResult> PushAlert([FromBody]AlertVM alert)
        {
            var id = await _alertMethod.Create(new Alert
            {
                Id = Guid.NewGuid().ToString(),
                IsChild = alert.IsChild,
                Location = alert.Location,
                Message = alert.Message,
                Type = alert.Type,
                UserId = alert.UserId
            });
            if(id != null)
            {
                return Json(id);
            }
            return Json(new { Message = "Error" });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAlerts([FromRoute]string Id)
        {
            var alerts = await _alertMethod.alertsOfUser(Id);
            if(alerts != null)
            {
                return Json(alerts);
            }
            return Json(new { Message = "Error" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAlert([FromRoute]string Id)
        {
            var alert = await _alertMethod.Get(Id);
            if(alert != null)
            {
                return Json(alert);
            }
            return Json(new { Message = "Error" });
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAlerts()
        {
            var alerts = await _alertMethod.GetAll();
            return Json(alerts);
        }
    }
}
