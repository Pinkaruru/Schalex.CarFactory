using System.Data.SQLite;

namespace CarFactory_Factory
{
    public interface IStorageProvider
    {
        public SQLiteConnection GetConnection();
    }
}
