using System;
using System.Threading.Tasks;

using Horus.Models;
using Horus.Repositories.Interface;

using Warehouse.Interfaces;
using Warehouse.Implementation;

namespace Horus.Repositories.Implementation
{
    public class ChildRepository : IChildRepository
    {
        private readonly IWarehouse<Child> _childWarehouse;

        public ChildRepository()
        {
            _childWarehouse = new Warehouse<Child>("https://safeland.documents.azure.com:443/", "JzgMnt0oCpQTDaO1fmFHnn3LVUtaImB8yaJRheixCiQzTOtkEAh10iZfyRVMAz6PGIrfitZICeMRFLWKFZwh1w==");

            _childWarehouse.Initialize("Users", "Children");
        }

        public async Task<bool> Create(Child child)
        {
            var id = await _childWarehouse.Store(child, child.id);
            if(id != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> Delete(string Id)
        {
            try
            {
                await _childWarehouse.Delete(Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Child> Get(string Id)
        {
            var child = await _childWarehouse.Get(Id);
            if (child != null)
            {
                return child;
            }
            return null;
        }

        public Task<bool> Update(Child child)
        {
            try
            {
                _childWarehouse.Update(child, child.id).Start();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            
        }
    }
}
