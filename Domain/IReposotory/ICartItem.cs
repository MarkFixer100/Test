using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IReposotory
{
    public interface ICartItem 
    {
        public Task Add(CartItem item);

        public Task remove(CartItem item);
    }
}
