using AutoMapper;
using CatSolution.CrossCutting.Logging.LoggerEvent;
using CatSolution.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CatSolution.Application.Core
{
    public class ManagementService<TEntity, TEntityDTO> : IManagementService<TEntity,TEntityDTO>
        where TEntity : class, new()
        where TEntityDTO : class, new()
    {
        Logger _log = null;
        IRepository<TEntity> _Repository = null;

        public ManagementService(IRepository<TEntity> repository)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TEntity, TEntityDTO>());
            _log = new Logger();
            _Repository = repository;
        }

        public TEntityDTO Add(TEntity entity)
        {
            IUnitOfWork unitOfWork = _Repository.UnitOfWork;
            TEntityDTO entityDTO = null;

            try
            {
                _Repository.Add(entity);
                unitOfWork.Commit();
                entityDTO = Mapper.Map<TEntityDTO>(entity);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return entityDTO;
        }

        public Task<TEntityDTO> AddAsync(TEntity entity)
        {
            return Task.Run(() => Add(entity));
        }

        public TEntityDTO Modify(TEntity entity)
        {
            IUnitOfWork unitOfWork = _Repository.UnitOfWork;
            TEntityDTO entityDTO = null;

            try
            {
                _Repository.Modify(entity);
                unitOfWork.Commit();
                entityDTO = Mapper.Map<TEntityDTO>(entity);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return entityDTO;
        }

        public Task<TEntityDTO> ModifyAsync(TEntity entity)
        {
            return Task.Run(() => Modify(entity));
        }

        public int Modify(ICollection<TEntity> items)
        {
            IUnitOfWork unitOfWork = _Repository.UnitOfWork;
            int result = 0;

            try
            {
                _Repository.Modify(items);
                result = unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return result;
        }

        public Task<int> ModifyAsync(ICollection<TEntity> items)
        {
            return Task.Run(() => Modify(items));
        }

        public int Remove(params object[] keys)
        {
            IUnitOfWork unitOfWork = _Repository.UnitOfWork;
            int result = 0;

            try
            {
                TEntity entity = _Repository.GetById(keys);

                _Repository.Remove(entity);
                result = unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }

            return result;
        }

        public Task<int> RemoveAsync(params object[] keys)
        {
            return Task.Run(() => Remove(keys));
        }

        public TEntityDTO GetById(params object[] keys)
        {
            TEntityDTO entityDTO = null;

            try
            {
                TEntity entity = _Repository.GetById(keys);

                entityDTO = Mapper.Map<TEntityDTO>(entity);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }
            return entityDTO;
        }

        public IEnumerable<TEntityDTO> GetAll()
        {
            IEnumerable<TEntityDTO> result = null;
            try
            {
                var list = _Repository.GetAll();

                result =  Mapper.Map<IEnumerable<TEntityDTO>>(list);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }
            return result;
        }

        public IEnumerable<TEntityDTO> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntityDTO> result = null;
            try
            {
                var list = _Repository.FindBy(predicate);

                result = Mapper.Map<IEnumerable<TEntityDTO>>(list);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex.Source, ex.StackTrace);
            }
            return result;
        }
    }
}
