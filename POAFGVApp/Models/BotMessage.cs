﻿using System;
namespace POAFGVApp
{
    public class BotMessage
    {
        public string Id { get; set; }
        public string ConversationId { get; set; }
        public DateTime Created { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
        public string ChannelData { get; set; }
        public string[] Images { get; set; }
        public Attachment[] Attachments { get; set; }
        public string ETag { get; set; }
        public bool IsIncoming { get; set; }
        public DateTime MessageDateTime { get; set; }
    }
}