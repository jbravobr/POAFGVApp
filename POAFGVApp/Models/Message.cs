using System;

namespace POAFGVApp
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }
        public bool IsIncoming { get; set; }
        public DateTimeOffset MessageDateTime { get; set; }
    }
}