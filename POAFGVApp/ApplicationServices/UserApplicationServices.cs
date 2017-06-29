using System;
using System.Collections.Generic;
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

        public Task<int> InsertAsync(User TEntity)
        {
            throw new NotImplementedException();
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

        public Task<User> GetWithChildrenByPredicateAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllWithChildrenByPredicateAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}