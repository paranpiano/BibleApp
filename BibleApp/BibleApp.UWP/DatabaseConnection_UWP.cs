using BibleApp.UWP;
using BibleApp.DatabaseAccess;
using SQLite;
using System.IO;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_UWP))]
namespace BibleApp.UWP
{
    public class DatabaseConnection_UWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "CustomersDb.db3";
            var path = Path.Combine(ApplicationData.
                Current.LocalFolder.Path , dbName);
            return new SQLiteConnection(path);
        }
    }
}
