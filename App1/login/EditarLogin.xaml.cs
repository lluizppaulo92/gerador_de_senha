using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using App1.DAO;
using App1.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1.login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditarLogin : Page
    {
        LoginModel loginModel = new LoginModel();
        LoginDAO loginDao;
        List<LoginModel> listLogin;
        ConexaoDB conn = ConnectionFactory.conexao;

        public EditarLogin()
        {
            this.InitializeComponent();
            loginDao = new LoginDAO(conn);
            recuperarLogin();
        }
        
        private async void alterarLogin(object sender, RoutedEventArgs e)
        {
           
            if (this.textBoxLogin.Text != "" && passwordBoxSenha.Password != "" && textBoxEmail.Text != "")
            {
                preencherLogin();
                await loginDao.UpdateLoginAsync(this.loginModel);
                menssage("Alteração de usuário!","Usuário Alterado!");
            }
            else
            {
               menssage("Preenchimento de campos obrigatórios!","Todos os campos são de preenchimento obrigatórios");
            }
        }

        public async void recuperarLogin()
        {
            try
            {
                listLogin = await loginDao.SelectAllLoginAsync();
                
            }
            catch (NullReferenceException loginNull) { }
        }
                
        void preencherLogin()
        {
            loginModel.login = textBoxLogin.Text.ToString();
            loginModel.senha = passwordBoxSenha.Password;
            loginModel.email = textBoxEmail.Text.ToString();
        }

        private void voltar(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void preencherView(object sender, RoutedEventArgs e)
        {
            loginModel = listLogin[0];
            this.textBoxLogin.Text = loginModel.login;
            this.passwordBoxSenha.Password = loginModel.senha;
            this.textBoxEmail.Text = loginModel.email;
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
    }
}
