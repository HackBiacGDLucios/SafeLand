using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Horus.Models;
using Horus.Repositories.Interface;

using Warehouse.Implementation;
using Warehouse.Interfaces;

namespace Horus.Repositories.Implementation
{
    public class AlertRepository : IAlertRepository
    {
        private IWarehouse<Alert> _alertWarehouse;
        public AlertRepository()
        {
            _alertWarehouse = new Warehouse<Alert>("https://safeland.documents.azure.com:443/", "JzgMnt0oCpQTDaO1fmFHnn3LVUtaImB8yaJRheixCiQzTOtkEAh10iZfyRVMAz6PGIrfitZICeMRFLWKFZwh1w==");
            _alertWarehouse.Initialize("Alerts", "alerts");
        }
        
        public async Task<string> Create(Alert alert)
        {
            var id = await _alertWarehouse.Store(alert, alert.id);
            if(id != null)
            {
                return id;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Delete(string Id)
        {
            try
            {
                await _alertWarehouse.Delete(Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<List<Alert>> GetAll()
        {
            try
            {
                var alerts = _alertWarehouse.GetAll();
                return Task.FromResult(alerts);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Alert>> alertsOfUser(string Id)
        {
            try
            {
                var alerts = await _alertWarehouse.GetAll(alert => alert.UserId == Id);
                return alerts;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Alert> Get(string Id)
        {
            var alert = await _alertWarehouse.Get(Id);
            if(alert != null)
            {
                return alert;
            }
            return null;
        }

        public Task<bool> Update(Alert alert)
        {
            try
            {
                _alertWarehouse.Update(alert, alert.id).Start();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}
