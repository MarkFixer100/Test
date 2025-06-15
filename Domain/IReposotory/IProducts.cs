using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IReposotory
{
    public interface IProducts:IRepository<Product>
    {
        Task<Product> UpdateAsync(Product entity);

        Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId);
    }
}
