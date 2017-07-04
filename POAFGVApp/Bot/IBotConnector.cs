using System;
using System.Threading.Tasks;

namespace POAFGVApp
{
    public interface IBotConnector
    {
        Task<BotMessage> SendMessage(string username, string messageText);
        Task<bool> Setup();
    }
}