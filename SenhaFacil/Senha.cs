using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace SenhaFacil
{
    class Senha
    {
        [PrimaryKey, AutoIncrement]
        public int senhaId { get; set; }
        public string descricao { get; set; }
        public string password { get; set; }
    }
}
