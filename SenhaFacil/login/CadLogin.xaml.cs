﻿using SenhaFacil.DAO;
using SenhaFacil.Model;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SenhaFacil.login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CadLogin : Page
    {
        LoginModel loginModel = new LoginModel();
        LoginDAO loginDao;
        ICollection<LoginModel> listLogin;
        ConexaoDB conn = ConnectionFactory.conexao;

        public CadLogin()
        {
            this.InitializeComponent();
            loginDao = new LoginDAO(conn);
        }

        private async void salvarLogin(object sender, RoutedEventArgs e){
            try{
                listLogin = await loginDao.SelectAllLoginAsync();
            }catch (NullReferenceException loginNull) { }


            if (this.textBoxLogin.Text != "" && passwordBoxSenha.Password != "" && textBoxEmail.Text != ""){
                if (listLogin.Count == 0){
                    preencherLogin();
                    await loginDao.InsertLoginAsync(this.loginModel);
                    menssage("Cadastro de Usuário", "Usuário cadastrado com sucesso!");
                    this.Frame.Navigate(typeof(PageLogin));
                }
                else{
                    menssage("Cadastro de Usuário", "Cadastro cancelado! Já possui um usuário cadastrado!");
                }
            }else{
                menssage("Validação de cadastro", "Todos os campos são de preenchimento obrigatórios");
            }
        }

        void preencherLogin()
        {
            loginModel.login = textBoxLogin.Text.ToString();
            loginModel.senha = passwordBoxSenha.Password;
            loginModel.email = textBoxEmail.Text.ToString();
        }
                
        private void voltar(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PageLogin));
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
