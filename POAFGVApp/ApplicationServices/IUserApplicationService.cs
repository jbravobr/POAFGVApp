using System;
using System.Threading.Tasks;

namespace POAFGVApp
{
    public interface IUserApplicationService
    {
        Task<User> DoRemoteLogin(string user, string password);
        Task<User> GetRemoteUserById(int id);
    }
}