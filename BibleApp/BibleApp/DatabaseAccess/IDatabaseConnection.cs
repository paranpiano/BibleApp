using System;
using System.Collections.Generic;
using System.Text;

namespace BibleApp.DatabaseAccess
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
