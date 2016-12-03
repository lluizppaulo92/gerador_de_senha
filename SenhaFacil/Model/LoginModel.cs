using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenhaFacil.Model
{
    class LoginModel
    {
        [PrimaryKey, AutoIncrement]
        public int loginId { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
    }
}
