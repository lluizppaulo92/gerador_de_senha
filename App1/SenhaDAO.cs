using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;

namespace App1
{
    class SenhaDAO 
    {
        SQLiteAsyncConnection conn;

        public SenhaDAO(ConexaoDB conexaoDB)
        {
            conn = conexaoDB.GetAsyncConnection();
        }

        public async Task InsertSenhaAsync(Senha senha)
        {
            await conn.InsertOrReplaceWithChildrenAsync(senha);
        }

        public async Task UpdateSenhaAsync(Senha senha)
        {
            await conn.UpdateWithChildrenAsync(senha);
        }

        public async Task DeleteSenhaAsync(Senha senha)
        {
            await conn.DeleteAsync(senha);
        }

        public async Task<List<Senha>> SelectAllSenhasAsync()
        {
            return await conn.GetAllWithChildrenAsync<Senha>();
        }

        public async Task<List<Senha>> SelectSenhasAsync(string query)
        {
            return await conn.QueryAsync<Senha>(query);
        }

       
    }
}
