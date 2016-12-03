using System.IO;
using System.Threading.Tasks;
using SQLite.Net.Async;
using System;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using SenhaFacil.login;
using SenhaFacil.Model;

namespace SenhaFacil
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
            InitializeDatabase();
        }

        public async Task InitializeDatabase()
        {
            await conn.CreateTableAsync<Senha>();
            await conn.CreateTableAsync<LoginModel>();
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return conn;
        }

        public void deleteAll()
        {
            conn.DeleteAllAsync<Senha>();
            conn.DeleteAllAsync<LoginModel>();
        }
    }
}
