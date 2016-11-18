using SQLite;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class ConexaoDB
    {

        string dbPath;
        SQLiteAsyncConnection conn;

        public ConexaoDB()
        {
            dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "GeradorSenha.sqlite");

            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: false)));
            conn = new SQLiteAsyncConnection(connectionFactory);
        }

        public async Task InitializeDatabase()
        {
            await conn.CreateTableAsync<Senha>();
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return conn;
        }
    }
}
