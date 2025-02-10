using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IReposotory
{
    public interface IReposotoryPerfume
    {
        Task<List<Perfume>> GetAll(Expression<Func<Perfume , bool>> filter = null);

        Task<Perfume> Get(Expression<Func<Perfume , bool>> filter = null , bool tracked = true );
        Task Create(Perfume entity);

        Task Remove(Perfume entity);

        Task Update(Perfume entity);

        Task Save();
    }
}
