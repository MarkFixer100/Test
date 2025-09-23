using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IReposotory
{
  
        public interface ICacheService
        {
            Task<string> GetAsync<T>(string key) where T : class;
            Task SetAsync<T>(string key, T value, TimeSpan expiration) where T : class;
            Task RemoveAsync(string key);
        }
    
}
