using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repository
{
    public interface IServiceReprository<t>
    {
        Task<t> FindAsync(int id);

        Task<t> AddAsync(t model);

        Task<List<t>> ListAsync();

        Task<t> UpdateAsync(t model);

        Task<bool> AddRangeAsync(List<t> model);

        Task<bool> RemoveRangeAsync(List<t> model);

        Task<int> RemoveAsync(t model);
    }
}
