using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Horus.Models;

namespace Horus.Repositories.Interface
{
    public interface IAlertRepository
    {
        Task<List<Alert>> GetAll();
        Task<List<Alert>> alertsOfUser(string Id);
        Task<string> Create(Alert alert);
        Task<bool> Delete(string Id);
        Task<Alert> Get(string Id);
        Task<bool> Update(Alert alert);
    }
}
