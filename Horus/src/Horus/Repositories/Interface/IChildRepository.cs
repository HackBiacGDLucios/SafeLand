using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Horus.Models;

namespace Horus.Repositories.Interface
{
    public interface IChildRepository
    {
        Task<bool> Create(Child child);
        Task<bool> Delete(string Id);
        Task<bool> Update(Child child);
        Task<Child> Get(string Id); 
    }
}
