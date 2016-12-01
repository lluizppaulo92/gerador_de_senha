using App1.DAO;
using App1.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageLogin : Page
    {
        LoginModel loginModel = new LoginModel();
        LoginDAO loginDao;
        ConexaoDB conn = ConnectionFactory.conexao;
        List<LoginModel> listLogin;

        public PageLogin()
        {
            this.InitializeComponent();
            loginDao = new LoginDAO(conn);
            recuperaLogin();

        }

        private void validarUsuarioSenha(object sender, RoutedEventArgs e)
        {
            if (listLogin != null) { 
                loginModel = listLogin[0];
                LoginModel loginInformado = new LoginModel();
                loginInformado.login = this.textBoxLogin.Text.ToString();
                loginInformado.senha = this.passwordBoxSenha.Password;
                if (loginInformado.login == "" || loginInformado.senha == "")
                {
                    textBlockMensagem.Text = "É necessário informar login e senha!";
                }
                else{
                    if (loginModel.login.ToString() == loginInformado.login.ToString() && loginModel.senha.ToString() == loginInformado.senha.ToString())
                    {
                        this.Frame.Navigate(typeof(MainPage));
                    }
                    else
                    {
                        textBlockMensagem.Text = "Usuário ou senha inválidos!";
                    }
                }
            }else
                {
                    textBlockMensagem.Text = "Nenhum login cadastrado! Favor acessar a opção Cadastrar-se!";
                }
        }

        private async void recuperaLogin()
        {
            try
            {
                //listLocal = await loginDao.SelectLoginAsync("Select * from LoginModel where login='"+ loginModel.login+"'");
                listLogin = await loginDao.SelectAllLoginAsync();
            } catch (NullReferenceException refEx) { } 
        }

       private void cadLogin(object sender, PointerRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CadLogin));
        }

        private void limparDados(object sender, PointerRoutedEventArgs e)
        {
            conn.deleteAll();
            textBlockMensagem.Text = "Dados deletados com sucesso!";
        }
    }
}
