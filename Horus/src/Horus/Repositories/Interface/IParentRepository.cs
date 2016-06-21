using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Horus.Models;

namespace Horus.Repositories.Interface
{
    public interface IParentRepository
    {
        //Main CRUD
        Task<bool> Create(Parent parent);
        Task<bool> Delete(string Id);
        Task<bool> Update(Parent parent);
        Task<Parent> Get(string Id);
    }
}
