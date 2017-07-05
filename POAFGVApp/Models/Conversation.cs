using System;
namespace POAFGVApp
{
    public class Conversation : BaseEntity
    {
        public string ConversationId { get; set; }
        public string Token { get; set; }
        public string ETag { get; set; }
    }
}