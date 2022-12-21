using TesteASPNet.Domain.Entity;
using TesteASPNet.Domain.Interfaces;
using TesteASPNet.Infra.Context;
using Canducci.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TesteASPNet.Infra.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly MySqlContext _mySqlContext;

        public BaseRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public async Task Insert(TEntity obj)
        {
            _mySqlContext.Set<TEntity>().Add(obj);
            await _mySqlContext.SaveChangesAsync();
        }

        public async Task Update(TEntity obj)
        {
            _mySqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _mySqlContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _mySqlContext.Set<TEntity>().Remove(await Select(id));
            await _mySqlContext.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> Select() =>
            await _mySqlContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity> Select(int id) =>
            await _mySqlContext.Set<TEntity>().FindAsync(id);

        public async Task<PaginatedRest<TEntity>> SelectWithPagination(int page, Expression<Func<TEntity, bool>> where)
        {
            return await _mySqlContext.Set<TEntity>()
                                      .AsNoTracking()
                                      .Where(where)
                                      .OrderBy(e => e.Id)
                                      .ToList()
                                      .ToPaginatedRestAsync<TEntity>(page, 10);
        }

        public async Task<PaginatedRest<TEntity>> SelectWithPagination(int page)
        {
            return await _mySqlContext.Set<TEntity>()
                                      .AsNoTracking()
                                      .OrderBy(e => e.Id)
                                      .ToList()
                                      .ToPaginatedRestAsync<TEntity>(page, 10);
        }

    }
}
