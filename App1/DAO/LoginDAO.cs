using App1.Model;
using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DAO
{
    class LoginDAO
    {
        SQLiteAsyncConnection conn;

        public LoginDAO(ConexaoDB conexaoDB)
        {
            conn = conexaoDB.GetAsyncConnection();
        }

        public async Task InsertLoginAsync(LoginModel login)
        {
            await conn.InsertOrReplaceWithChildrenAsync(login);
        }

        public async Task UpdateLoginAsync(LoginModel login)
        {
            await conn.UpdateWithChildrenAsync(login);
        }

        public async Task DeleteLoginAsync(LoginModel login)
        {
            await conn.DeleteAsync(login);
        }

        public async Task<List<LoginModel>> SelectAllLoginAsync()
        {
            return await conn.GetAllWithChildrenAsync<LoginModel>();
        }

        public async Task<List<LoginModel>> SelectLoginAsync(string query)
        {
            return await conn.QueryAsync<LoginModel>(query);
        }


    }
}
