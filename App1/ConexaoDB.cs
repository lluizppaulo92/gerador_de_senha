using System.IO;
using System.Threading.Tasks;
using SQLite.Net.Async;
using System;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;

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
