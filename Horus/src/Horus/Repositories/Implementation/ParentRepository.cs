using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Horus.Repositories.Interface;
using Horus.Models;

using Warehouse.Implementation;
using Warehouse.Interfaces;

namespace Horus.Repositories.Implementation
{
    public class ParentRepository : IParentRepository
    {
        private readonly IWarehouse<Parent> parentWarehouse;
        
        public ParentRepository()
        {
            parentWarehouse = new Warehouse<Parent>("https://safeland.documents.azure.com:443/", "JzgMnt0oCpQTDaO1fmFHnn3LVUtaImB8yaJRheixCiQzTOtkEAh10iZfyRVMAz6PGIrfitZICeMRFLWKFZwh1w==");
            
            parentWarehouse.Initialize("Users","Parents");
        }
        public async Task<bool> Create(Parent parent)
        {
            var id = await parentWarehouse.Store(parent, parent.id);
            if (id != null)
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
                await parentWarehouse.Delete(Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public async Task<Parent> Get(string Id)
        {
            var parent = await parentWarehouse.Get(Id);
            if(parent != null)
            {
                return parent;
            }
            return null;
        }

        public async Task<bool> Update(Parent parent)
        {
            try
            {
                await parentWarehouse.Update(parent, parent.id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
