using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IReposotory
{
    public interface IReposotoryPerfume:IRepository<Perfume>
    {

        Task<Perfume> UpdateAsync(Perfume entity);

    }
}
