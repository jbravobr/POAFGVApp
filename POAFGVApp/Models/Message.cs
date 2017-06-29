namespace POAFGVApp
{
    public class Message : BaseEntity
    {
        public string Text { get; set; }
        public bool IsIncoming { get; set; }
        public string MessageDateTime { get; set; }
    }
}