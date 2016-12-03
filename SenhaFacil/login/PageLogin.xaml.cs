using SenhaFacil.DAO;
using SenhaFacil.Model;
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

namespace SenhaFacil.login
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
            if (listLogin.Count != 0) { 
                loginModel = listLogin[0];
                LoginModel loginInformado = new LoginModel();
                loginInformado.login = this.textBoxLogin.Text.ToString();
                loginInformado.senha = this.passwordBoxSenha.Password;
                if (loginInformado.login == "" || loginInformado.senha == "")
                {
                    menssage("Login sistema!","É necessário informar login e senha!");
                }
                else{
                    if (loginModel.login.ToString() == loginInformado.login.ToString() && loginModel.senha.ToString() == loginInformado.senha.ToString())
                    {
                        this.Frame.Navigate(typeof(MainPage));
                    }
                    else
                    {
                        menssage("Validação Usuário","Usuário ou senha inválidos!");
                    }
                }
            }else
                {
                    menssage("Login sistema!","Nenhum login cadastrado! Favor acessar a opção Cadastrar-se!");
                }
        }

        private async void recuperaLogin()
        {
            try
            {
                listLogin = await loginDao.SelectAllLoginAsync();
            } catch (NullReferenceException refEx) { } 
        }

       private void cadLogin(object sender, PointerRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CadLogin));
        }

        private async void limparDados(object sender, PointerRoutedEventArgs e)
        {
            ContentDialog deleteFileDialog = new ContentDialog()
            {
                Title = "Limpar dados do aplicativo?",
                Content = "Esta ação apaga usuário de acesso e todas as senhas armazenadas, deseja continuar?",
                PrimaryButtonText = "Sim",
                SecondaryButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                conn.deleteAll();
                menssage("Apagar dados!", "Dados apagados com sucesso!");
            }
        }

        public async void menssage(String titulo, String menssagem)
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = titulo,
                Content = menssagem,
                PrimaryButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

        
        private void recursoNaoImplemetado(object sender, PointerRoutedEventArgs e)
        {
            menssage("Recurso não implementado", "Este recurso está previsto para a próxima versão!");
        }
    }
}
