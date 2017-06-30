using SQLite;

namespace POAFGVApp
{
    public class BaseEntity
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
    }
}