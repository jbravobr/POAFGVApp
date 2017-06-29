using System.Threading.Tasks;

namespace POAFGVApp
{
    public class UserApplicationServices : BaseApplicationService<User>, IUserApplicationService
    {
        IBaseRepository<User> _userRepository { get; }

        public UserApplicationServices(IBaseRepository<User> userRepository) : base(userRepository)
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
    }
}