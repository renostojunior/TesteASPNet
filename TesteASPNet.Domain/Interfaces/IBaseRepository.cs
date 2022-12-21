using Canducci.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TesteASPNet.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task Insert(TEntity obj);
        Task Update(TEntity obj);

        Task Delete(int id);

        Task<IList<TEntity>> Select();

        Task<TEntity> Select(int id);
        Task<PaginatedRest<TEntity>> SelectWithPagination(int page, Expression<Func<TEntity, bool>> where);
        Task<PaginatedRest<TEntity>> SelectWithPagination(int page);
    }
}
