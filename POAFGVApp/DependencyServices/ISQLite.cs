using System;
using SQLite;


namespace POAFGVApp
{
    public interface ISQLite
    {
        SQLiteConnection GetConn();
    }
}