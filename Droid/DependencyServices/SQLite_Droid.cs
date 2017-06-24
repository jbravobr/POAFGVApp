using System;
using System.IO;
using SQLite;

namespace POAFGVApp.Droid
{
    public class SQLite_Droid : ISQLite
    {
		public SQLiteConnection GetConn()
		{
			try
            {
                var sqliteFilename = "poafgvdb.db3";
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var path = Path.Combine(documentsPath, sqliteFilename);
                var conn = new SQLiteConnection(path);
                return conn;
			} catch (Exception ex) {
                throw ex;
			}
		}
    }
}
