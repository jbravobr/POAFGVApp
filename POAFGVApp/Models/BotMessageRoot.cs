using System;
using System.Collections.Generic;

namespace POAFGVApp
{
    public class BotMessageRoot
    {
        public List<BotMessage> Messages { get; set; }
        public string Watermark { get; set; }
    }
}