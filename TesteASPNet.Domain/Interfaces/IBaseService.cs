using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Canducci.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TesteASPNet.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
                where TValidator : AbstractValidator<TEntity>
                where TInputModel : class
                where TOutputModel : class;

        Task<TOutputModel> Add<TInputModel, TOutputModel>(TInputModel inputModel)
                where TInputModel : class
                where TOutputModel : class;

        Task<bool> Add<TInputModel>(TEntity inputModel);

        Task Delete(int id);

        Task<IEnumerable<TOutputModel>> Get<TOutputModel>() where TOutputModel : class;


        Task<TOutputModel> GetById<TOutputModel>(int id) where TOutputModel : class;

        Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;

        Task<bool> Update<TInputModel>(TEntity inputModel);

        Task<TOutputModel> Update<TInputModel, TOutputModel>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class;

        Task<PaginatedRest<TEntity>> GetWithPagination(int page, Expression<Func<TEntity, bool>> where);
        Task<PaginatedRest<TEntity>> GetWithPagination(int page);


    }
}
