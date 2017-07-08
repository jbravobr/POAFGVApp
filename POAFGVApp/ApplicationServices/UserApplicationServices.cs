using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace POAFGVApp
{
    public class UserApplicationServices : IUserApplicationService
    {
        IBaseRepository<User> _userRepository { get; }

        public UserApplicationServices(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> DoRemoteLogin(string user, string password)
        {
            //TODO: Implementar chamada ao webservice
            return await Task.Run(() => new User() { Id = 1, Login = "admin", Password = "admin", Name = "Rodrigo" });
        }

        public async Task<User> GetRemoteUserById(int id)
        {
            //TODO: Implementar chamada ao webservice
            return await Task.Run(() => new User() { Id = 1, Login = "admin", Password = "admin", Name = "Rodrigo" });
        }

        public async Task<int> InsertAsync(User TEntity)
        {
            User entity;

            try
            {
                await Task.Run(() => _userRepository.Insert(TEntity));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                entity = (await this.GetAllAsync()).Last();
            }

            return entity != null ? entity.Id : -1;
        }

        public Task DeleteAsync(User TEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User TEntity)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int pkId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetWithChildrenByPredicateAsync(Func<User, bool> predicate)
        {
            return await Task.Run(() => _userRepository.GetWithChildrenByPredicate(predicate));
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await Task.Run(() => _userRepository.GetAll());
        }

        public async Task<List<User>> GetAllWithChildrenByPredicateAsync(Expression<Func<User, bool>> predicate)
        {
            return await Task.Run(() => _userRepository.GetAllWithChildrenByPredicate(predicate));
        }

        public async Task<User> GetWithChildrenByPredicateAsync(int pk)
        {
            return await Task.Run(() => _userRepository.GetWithChildrenByPredicate(pk));
        }
    }
}