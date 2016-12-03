using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenhaFacil
{
    class ConnectionFactory
    {
        private static ConexaoDB conn = new ConexaoDB();

        public static ConexaoDB conexao { get { return conn; } }
    }
}
