using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenhaFacil
{
    class SenhaViewModel
    {
        private ObservableCollection<Senha> senhas = new ObservableCollection<Senha>();
        public ObservableCollection<Senha> Senhas { get { return this.senhas; } }
        public SenhaViewModel()
        {
            popularSenhas();
        }

        public async void popularSenhas()
        {
            SenhaDAO senhaDAO = new SenhaDAO(ConnectionFactory.conexao);
            try
            {
                List<Senha> listSenha = await senhaDAO.SelectAllSenhasAsync();
                foreach (Senha senhaEach in listSenha)
                {
                    this.senhas.Add(senhaEach);
                }

            }
            catch (SQLite.Net.SQLiteException e)
            {
                //Não carrega listagem
            }
        }

        public void atualizar(Senha senha)
        {
            foreach (Senha s in Senhas)
            {
                if (s.senhaId == senha.senhaId)
                {
                    Senhas[senhas.IndexOf(s)] = senha;
                }
            }
        }
    }
}
