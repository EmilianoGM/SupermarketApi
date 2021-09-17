using SupermarketApi.Abstractions.Applications;
using SupermarketApi.Abstractions.Communication;
using SupermarketApi.Abstractions.Repositories;
using SupermarketApi.Application.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarketApi.Application
{
    public class Application<T> : IApplication<T>
    {
        protected readonly IRepository<T> _repository;
        public Application(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IResponseWrapper<IEnumerable<T>>> ListAsync()
        {
            var entities = await _repository.ListAsync();
            return new ResponseWrapper<IEnumerable<T>>(entities);
            /*
            try
            {
                var entities = await _repository.ListAsync();
                return new ResponseWrapper<IEnumerable<T>>(entities);
            } catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<T>>("An error ocurred while retrieving the entities.", ex.Message);
            }*/
        }

        public async Task<IResponseWrapper<T>> AddAsync(T entity)
        {
            try
            {
                await _repository.AddAsync(entity);
                return new ResponseWrapper<T>(entity);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<T>("An error ocurred while saving the entity.", ex.Message);
            }
        }

        public async Task<IResponseWrapper<T>> FindByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.FindByIdAsync(id);
                if(entity != null)
                {
                    return new ResponseWrapper<T>(entity);
                } else
                {
                    return new ResponseWrapper<T>("Entity not found.", "The entity was not found with the provided id.");
                }
            }
            catch (Exception ex)
            {

                return new ResponseWrapper<T>("An error ocurred while retrieving the entity.", ex.Message);
            }
        }

        public async Task<IResponseWrapper<T>> Remove(int id)
        {
            try
            {
                var entity = await _repository.FindByIdAsync(id);
                if (entity != null)
                {
                    _repository.Remove(entity);
                    return new ResponseWrapper<T>(entity);
                }
                else
                {
                    return new ResponseWrapper<T>("Entity not found.", "The entity was not found with the provided id.");
                }
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<T>("An error ocurred while deleting the entity.", ex.Message);
            }
        }

        public async Task<IResponseWrapper<T>> Update(T entity)
        {
            try
            {
                var updated = await _repository.Update(entity);
                return new ResponseWrapper<T>(entity);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<T>("An error ocurred while updating the entity.", ex.Message);
            }
        }


    }
}
