using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TesteASPNet.Domain.Interfaces;
using FluentValidation;
using Canducci.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TesteASPNet.Architecture.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }


        public async Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
                where TValidator : AbstractValidator<TEntity>
                where TInputModel : class
                where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            Validate(entity, Activator.CreateInstance<TValidator>());
            await _baseRepository.Insert(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public async Task<TOutputModel> Add<TInputModel, TOutputModel>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            await _baseRepository.Insert(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;
        }

        public async Task<bool> Add<TInputModel>(TEntity inputModel)
        {
            try
            {
                await _baseRepository.Insert(inputModel);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task Delete(int id) => await _baseRepository.Delete(id);

        public async Task<IEnumerable<TOutputModel>> Get<TOutputModel>() where TOutputModel : class
        {
            var entities = await _baseRepository.Select();

            var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s));

            return outputModels;
        }

        public async Task<TOutputModel> GetById<TOutputModel>(int id) where TOutputModel : class
        {
            var entity = await _baseRepository.Select(id);
            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public async Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);


            Validate(entity, Activator.CreateInstance<TValidator>());

            await _baseRepository.Update(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public async Task<TOutputModel> Update<TInputModel, TOutputModel>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);
            await _baseRepository.Update(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public async Task<bool> Update<TInputModel>(TEntity inputModel)
        {
            try
            {
                await _baseRepository.Update(inputModel);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }

        public async Task<PaginatedRest<TEntity>> GetWithPagination(int page, Expression<Func<TEntity, bool>> where)
        {
            return await _baseRepository.SelectWithPagination(page, where);
        }

        public async Task<PaginatedRest<TEntity>> GetWithPagination(int page)
        {
            return await _baseRepository.SelectWithPagination(page);
        }

    }
}

